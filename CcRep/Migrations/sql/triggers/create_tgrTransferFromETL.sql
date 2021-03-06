﻿-- check new data and save it into report
CREATE TRIGGER [etl].[tgrTransferFromETL] ON [etl].[ETL_DWHS]  
FOR UPDATE AS  
    IF UPDATE(PARAM_VALUE)
	BEGIN
	     DECLARE @ID_DWH int
	     DECLARE @id_rep int
	     DECLARE @value char
		 DECLARE @res char
		 DECLARE @author nvarchar(20)

         SELECT @value = [PARAM_VALUE], @ID_DWH = [ID_KEY], @id_rep = [ID_REP], @author = [LAUNCH_AUTHOR] FROM inserted

		 IF (@value = '3')
		 BEGIN
		   UPDATE [etl].[ETL_DWHS] SET [START_LOAD] = GETDATE() FROM [etl].[ETL_DWHS] WHERE  [ID_KEY] =  @ID_DWH
		 END

		 IF (@value = '4')
		 BEGIN
         IF exists(select * from [dbo].[VK_HEADER_REP] hr where hr.ID_REP = @id_rep)
		 BEGIN
			DECLARE @DATE datetime

			SET @res = 'S'
			select @DATE = CAST(FLOOR(CAST(GETDATE() as float)) as datetime)

		    BEGIN TRY
		    DECLARE @IRB_PAYMENT_RK bigint
			DECLARE @IRB_PAYMENT_SK bigint
			DECLARE @DUPLICATE_STR varchar(9)
			DECLARE @LOADED_STR varchar(9)
			DECLARE @SUPDOC_FL char(1)
			DECLARE @NEW_STR varchar(9)
			DECLARE @LAST_ID bigint
			DECLARE @FIRST_ID bigint
			DECLARE @COUNT_BO_DWH bigint
			DECLARE @COUNT_BO_3130_406 bigint
			DECLARE @COUNT_BO_3132 bigint
			DECLARE @CURRENT_ID bigint
			DECLARE @ERROR_NUM INT 
			DECLARE @ERROR_MSG varchar(1000);
			DECLARE @ERROR_LINE varchar(4);
			
			SET @DUPLICATE_STR = 'DUPLICATE'
			SET @LOADED_STR = 'LOADED'
			SET @NEW_STR = 'NEW'
			SET @SUPDOC_FL = 'N'

			select @COUNT_BO_DWH = Count(*) from [etl].[ETL_BO_DWH]
			select @COUNT_BO_3130_406 = Count(*) from [etl].[ETL_BO_3130_406]
			select @COUNT_BO_3132 = Count(*) from [etl].[ETL_BO_3132]

			-- Fill VK_ADDINF_REP
			INSERT INTO [dbo].[VK_ADDINF_REP] 
			    ([FORM], [SECTION], [SOURCE_NAME], [IRB_PAYMENT_RK], [PAYMENT_RK], [SOURCE], SUPDOC_FL, ID_REP, 
				 DATE_CREATE, AUTHOR, [STATUS])
		    SELECT u.*,
			      @LOADED_STR, @SUPDOC_FL, @id_rep, @DATE, @author as AUTHOR, 
			     STAT = 
				case 
				when exists(select * from [dbo].[VK_ADDINF_REP] ar
				where ar.ID_REP = @id_rep AND ar.[IRB_PAYMENT_RK] = u.IRB_PAYMENT_RK AND 
				      ar.[STATUS] != @DUPLICATE_STR AND ar.SOURCE = @LOADED_STR AND ar.SUPDOC_FL =@SUPDOC_FL)
				then @DUPLICATE_STR else @NEW_STR
				end 
            FROM (
				SELECT FORM = '405', SECTION = 'NO',
				DWH_DATA_MART, IRB_PAYMENT_RK,[IRB_PAYMENT_SK]
				FROM [etl].[ETL_BO_DWH]
				UNION ALL
				SELECT FORM = '406', SECTION = '', 
				DWH_DATA_MART, IRB_PAYMENT_RK, [IRB_PAYMENT_SK]
				FROM [etl].[ETL_BO_3130_406]
				UNION ALL
				SELECT FORM = '405', SECTION = 'NO', 
				DWH_DATA_MART, IRB_PAYMENT_RK, [IRB_PAYMENT_SK]
				FROM [etl].[ETL_BO_3132]
			) u

			select @LAST_ID = @@IDENTITY 
			SET @FIRST_ID = @LAST_ID - @COUNT_BO_DWH - @COUNT_BO_3130_406 - @COUNT_BO_3132

			-- Fill VK_BASIC_REP
			SET @CURRENT_ID = @FIRST_ID

			INSERT INTO dbo.VK_BASIC_REP 
			    ([FILIAL],[POSTDATE],[TYPE_TOOLING],[OPER_TYPE],[DIRECTION_PAY],[COUNT],
				 [SHARE],[CCY],
				 [AMOUNT_ALL],[AMNT_INCOME],
				 [REF_NUM],[CODE_VO],
				 [SWIFT],[SWIFT_FIL],
				 ID_OPER
				 )
				SELECT [BRANCH_ID],[PAYMENT_DT],[CODE_405_ID],CAST([OPERATION_TYPE_CD] AS tinyint) as OPER_TYPE,[DIRECTION_ID],[PARTS_CNT],
				[PART_AMT],RIGHT([CCD_CURRENCY_ID], 3) as CCY,
				[PAYMENT_AMT],[PERCENT_PAYMENT_AMT],
				[REFERENCE_ID],RIGHT([VO_CODE], 5) as CODE_VO,
				RIGHT([ORG_SWIFT_ID], 11) as SWIFT, RIGHT([BRANCH_SWIFT_ID], 3),
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
				FROM [etl].[ETL_BO_DWH] 

			SET @CURRENT_ID = @FIRST_ID + @COUNT_BO_DWH
			
			INSERT INTO dbo.VK_BASIC_REP 
			    ([FILIAL],[POSTDATE],[CCY],[REF_NUM],[CODE_VO], 
				[SWIFT],[SWIFT_FIL],ID_OPER)
				SELECT [BRANCH_ID],[PAYMENT_DT],RIGHT([CCD_CURRENCY_ID], 3) as CCY,[REFERENCE_ID],RIGHT([VO_CODE], 5) as CODE_VO,
				RIGHT([ORG_SWIFT_ID], 11) as SWIFT,RIGHT([BRANCH_SWIFT_ID], 3),
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
		        FROM [etl].[ETL_BO_3130_406]

			SET @CURRENT_ID = @FIRST_ID + @COUNT_BO_DWH + @COUNT_BO_3130_406

			INSERT INTO dbo.VK_BASIC_REP 
			     ([FILIAL],[POSTDATE],[OPER_TYPE],[DIRECTION_PAY],
				  [CCY],[AMOUNT_ALL],[REF_NUM],[CODE_VO],ID_OPER)
				SELECT [BRANCH_ID],[PAYMENT_DT],CAST([OPERATION_CODE_ID] AS tinyint) as OPER_TYPE,[TRANSFER_DIRECTION_TXT],
				RIGHT([CURRENCY_ID], 3) as CCY,[PAYMENT_AMT],[PAYMENT_REFERENCE_ID],RIGHT([VO_CODE_ID], 5) as CODE_VO,
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
		        FROM [etl].[ETL_BO_3132]

			-- Fill VK_CLIENT_REP
			SET @CURRENT_ID = @FIRST_ID

			INSERT INTO dbo.VK_CLIENT_REP 
			    ([NAME_REZ],[TYPE_REZ],[BIC_PARTNER],[COUNTRY_REZ],[INN],
				 [NAME_NEREZ],[TYPE_NEREZ],[COUNTRY_NEREZ],[BANK_NAME],
				 [COUNTRY_BANK],[PARTNER_NAME],[COUNTRY_PARTNER],
				 [CUSTNO],ID_OPER)
				SELECT [RESIDENT_FULL_NM],RIGHT([RESIDENT_ACC_TAG_CD], 2),[BIK_NO],RIGHT([COUNTRY_NUM_CD],3),[RESIDENT_INN_NO],
				[NONRESIDENT_FULL_NM],RIGHT([NONRESIDENT_ACC_TAG_CD],2),RIGHT([NONRESIDENT_COUNTRY_NUM_CD],3),[COUNTERPARTY_CONTROL_TXT],
				RIGHT([COUNTERPARTY_BANK_COUNTRY_CD],3),[COUNTERPARTY_NONRES_NM],RIGHT([COUNTERPARTY_COUNTRY_CD],3),
				RIGHT([RESIDENT_CNUM_CD],8),LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
				FROM [etl].[ETL_BO_DWH]

			SET @CURRENT_ID = @FIRST_ID + @COUNT_BO_DWH

			INSERT INTO dbo.VK_CLIENT_REP 
			    ([NAME_REZ],[INN],[NAME_NEREZ], ID_OPER)
				SELECT [RESIDENT_FULL_NM],[RESIDENT_INN_NO],[NONRESIDENT_FULL_NM], 
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
		        FROM [etl].[ETL_BO_3130_406]

			SET @CURRENT_ID = @FIRST_ID + @COUNT_BO_DWH + @COUNT_BO_3130_406

			INSERT INTO dbo.VK_CLIENT_REP 
			    ([NAME_REZ],[TYPE_REZ],[BIC_PARTNER],[COUNTRY_REZ],
				 [NAME_NEREZ],[BANK_NAME],[CUSTNO],ID_OPER)
				SELECT [SND_NM],LEFT([LEGAL_FORM_ID],2) as TYPE_REZ,[SND_BIK_NO],LEFT([BANK_COUNTRY_ID],3) as COUNTRY_REZ,
				[RCV_NM],[RCV_BANK_NM],[RESIDENT_CNUM_CD],
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
		        FROM [etl].[ETL_BO_3132]
			
			-- Fill VK_ISSUER_REP
			SET @CURRENT_ID = @FIRST_ID

			INSERT INTO [dbo].[VK_ISSUER_REP]
			    ([ISSUER_NAME],[SECURITY_CODE],[REG_NUM_ISSUER],[DATE_REG_ISSUER],[REPAY_DATE],[CCY_ISSUER],
				 [ISSUER_CODE],[ISSUER_REESTR], ID_OPER)
				SELECT [ISSUER_NM],[ISSUER_INN_NO],[CB_REG_NO],[CB_REG_DT],[CB_SET_DT],[CB_CURRENCY_ID],
				CAST(RIGHT([ISSUER_TXT],2) as tinyint) as ISSUER_CODE,[ISSUER_CB_REESTR_TXT], 
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
				FROM [etl].[ETL_BO_DWH]

			-- Fill VK_NOTICE_REP
			SET @CURRENT_ID = @FIRST_ID

			INSERT INTO dbo.VK_NOTICE_REP 
			    ([NOTICE],[NOTICE_ISSUER],[NOTICE_EXCHANGE],[NOTICE_INST],[NOTICE_PROPERTY],[NOTICE_BANK], ID_OPER)
		    SELECT [COMMENT_TXT],[DIFF_ISSUER_PAYMENT_TXT],[CB_MUT_SET_TXT],[CB_INST_TXT],
			    NOTICE_PROPERTY = 
				case 
				when [PROPERTY_FLG] = '1' 
				then 'ИМУЩЕСТВО' else NULL
				end, 
				[FOR_ACC_OPERATION_TXT],
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
				FROM [etl].[ETL_BO_DWH]

			-- Fill VK_ADD_REP
			SET @CURRENT_ID = @FIRST_ID

			INSERT INTO [dbo].[VK_ADD_REP] ([CCD_USER],[NARRATIVE],[CITY_CODE],
						[NOTICE_REPORT],[DEP_ACC],[CONTRACT_NUM],[SOURCE_405],
						[MODIFY_DATE],[MODIFY_USER], ID_OPER)
			SELECT u.*, LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
            FROM (
				SELECT 
				[CCD_AUTHORIZATION_USER_ID],[NARRATIVE_TXT],RIGHT([CITY_ID],4) as CITY_CODE,
				[CCD_NARRATIVE_TXT],RIGHT([OFFICE_ID], 4) as DEP_ACC,[PAYMENT_CONTRACT_ID],[SOURCE_405_TXT],[DOC_UPDATE_DT],
				LEFT([UPDATE_USER_ID],15) as MODIFY_USER
				FROM [etl].[ETL_BO_DWH]
				UNION ALL
				SELECT 
				[CCD_AUTHORIZATION_USER_ID],[NARRATIVE_TXT],RIGHT([CITY_ID],4) as CITY_CODE,[CCD_NARRATIVE_TXT],
				RIGHT([OFFICE_ID], 4) as DEP_ACC,[PAYMENT_CONTRACT_ID],[SOURCE_405_TXT],[DOC_UPDATE_DT],
				LEFT([UPDATE_USER_ID],15) as MODIFY_USER
				FROM [etl].[ETL_BO_3130_406]
				UNION ALL
				SELECT 
				CCD_AUTHORIZATION_USER_ID = NULL,[NARRATIVE_TXT], CITY_ID = NULL, CCD_NARRATIVE_TXT = NULL,
				[BRANCH_ID], PAYMENT_CONTRACT_ID = NULL, SOURCE_405_TXT = NULL, DOC_UPDATE_DT = NULL, UPDATE_USER_ID = NULL
				FROM [etl].[ETL_BO_3132]
			) u

			-- Fill VK_406_REP
			SET @CURRENT_ID = @FIRST_ID + @COUNT_BO_DWH

			INSERT INTO dbo.VK_406_REP 
			    (OPER_KIND, NOTICE_40, AMOUNT_TO, AMOUNT_FROM, BIC_BANK, 
				 [COUNTRY_BANK_406],[ADD_CODE_10],
				 [KIND_REZ],[INN_NEREZ],
				 [KIND_NEREZ],[KIND_CONTRACT],
				 [TYPE_CONTRACT],[NOTICE_15],[NOTICE_16],[NOTICE_OTHER], ID_OPER)
				SELECT CAST([OPERATION_TYPE_CD] as tinyint) as OPER_KIND,[SHORT_DESC],[TO_NONRES_PAYMENT_AMT],[FROM_NONRES_PAYMENT_AMT],
			    [BIK_NO],
				[COUNTRY_NUM_CD],[ADD_10_CODE_TXT],
				RIGHT([BUYER_RESIDENT_TXT],10) as KIND_REZ,[NONRESIDENT_INN_NO],
				RIGHT([NONRESIDENT_TYPE_CD],3) as KIND_NEREZ,RIGHT([DEAL_TYPE_CD],3) as KIND_CONTRACT,
				RIGHT([DEAL_CD],3) as TYPE_CONTRACT,[CHAPTER_15_TXT],[CHAPTER_16_TXT],[OTHER_TXT], 
				LAST_ID = @CURRENT_ID + (row_number() OVER(ORDER BY (SELECT NULL)))
				FROM [etl].[ETL_BO_3130_406]

			END TRY
			BEGIN CATCH 
			SET @res = 'E'
			SET @ERROR_NUM = ERROR_NUMBER()
			SET @ERROR_MSG = ERROR_MESSAGE()
			SET @ERROR_LINE = ERROR_LINE()
			-- SELECT @ERROR_NUM AS ErrorNumber,
			 --      @ERROR_LINE AS ErrorLine,
			--       @ERROR_MSG AS ErrorMessage;
			END CATCH

			UPDATE [etl].[ETL_DWHS] SET [END_LOAD] = GETDATE() FROM [etl].[ETL_DWHS] WHERE  [ID_KEY] = @ID_DWH

			INSERT INTO [dbo].[ADM_DWH_LOAD] (DATE_LOAD, ID_DWH, [STATE])
				   VALUES (@DATE, @ID_DWH, @res);
		 END
	    
	   END

	END

