Set Identity_Insert VK_HEADER_REP On

INSERT INTO [dbo].[VK_HEADER_REP]
			    ( [ID_REP], [BEGIN_DATE], [END_DATE], [LAST_EDITED_BY], [ARCHIVE_DATE], [COMMENT])
				  VALUES (1, CONVERT(date, '30.08.2006', 104), CONVERT(date, '28.04.2017', 104), NULL,
				  CONVERT(date, '28.04.2017', 104), 'Данные из БД MS Access')

INSERT INTO [dbo].[VK_HEADER_REP]
			    ( [ID_REP], [BEGIN_DATE], [END_DATE], [LAST_EDITED_BY], [ARCHIVE_DATE], [COMMENT])
				  VALUES (2, CONVERT(date, '09.01.2017', 104), CONVERT(date, '31.08.2018', 104), NULL, 
				  CONVERT(date, '31.08.2018', 104), 'Данные из Excel файла')

Set Identity_Insert VK_HEADER_REP Off	
