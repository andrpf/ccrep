namespace CcRep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DIC_ACCOUNT_CLIENT",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    CNUM = c.String(nullable: false, maxLength: 8),
                    CBACCOUNT = c.String(nullable: false, maxLength: 20),
                    DESCRIPTION = c.String(maxLength: 100),
                    BEG_DATE = c.DateTime(nullable: false, storeType: "date"),
                    END_DATE = c.DateTime(storeType: "date"),
                })
                .PrimaryKey(t => t.ID_KEY)
                .Index(t => new { t.CNUM, t.CBACCOUNT }, unique: true);

            CreateTable(
                "dbo.DIC_ACCT_REPORT",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    ACC2 = c.String(nullable: false, maxLength: 5),
                    RESIDENT = c.Boolean(nullable: false),
                    CNTR_PARTNER = c.Boolean(nullable: false),
                    LAST_EDITED_BY = c.String(maxLength: 128),
                    CREATED_BY = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.ID_KEY)
                .ForeignKey("dbo.ASP_USERS", t => t.CREATED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.LAST_EDITED_BY)
                .Index(t => t.ACC2, unique: true, name: "IX_Acc2")
                .Index(t => t.LAST_EDITED_BY)
                .Index(t => t.CREATED_BY);

            CreateTable(
                "dbo.DIC_CLIENT_TYPES",
                c => new
                {
                    Id = c.Short(nullable: false, identity: true),
                    NameShort = c.String(nullable: false, maxLength: 5),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.NameShort, unique: true);

            CreateTable(
                "dbo.ASP_USERS",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    UserName = c.String(nullable: false, maxLength: 256),
                    PasswordHash = c.String(),
                    FullName = c.String(),
                    Locked = c.Boolean(nullable: false),
                    CreatedAt = c.DateTime(),
                    EndDt = c.DateTime(storeType: "date"),
                    Email = c.String(maxLength: 256),
                    SecurityStamp = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.ASP_USER_CLAIMS",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LAST_EDITED_BY = c.String(maxLength: 128),
                    CREATED_BY = c.String(maxLength: 128),
                    CREATE_DATE = c.DateTime(),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ASP_USERS", t => t.CREATED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.LAST_EDITED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.UserId, cascadeDelete: true)
                .Index(t => t.LAST_EDITED_BY)
                .Index(t => t.CREATED_BY)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.ASP_USERS_TO_FILIALS",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    USER = c.String(nullable: false, maxLength: 128),
                    FILIAL = c.String(nullable: false, maxLength: 4),
                    CREATE_DATE = c.DateTime(),
                    LAST_EDITED_BY = c.String(maxLength: 128),
                    CREATED_BY = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DIC_FILIAL", t => t.FILIAL, cascadeDelete: true)
                .ForeignKey("dbo.ASP_USERS", t => t.CREATED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.LAST_EDITED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.USER, cascadeDelete: true)
                .Index(t => t.USER)
                .Index(t => t.FILIAL)
                .Index(t => t.LAST_EDITED_BY)
                .Index(t => t.CREATED_BY);

            CreateTable(
                "dbo.DIC_FILIAL",
                c => new
                {
                    NC_FILIAL = c.String(nullable: false, maxLength: 4),
                    LC_FILIAL = c.String(nullable: false, maxLength: 3),
                    LICENSE = c.String(nullable: false, maxLength: 4),
                    NAME_FILIAL = c.String(nullable: false, maxLength: 80),
                })
                .PrimaryKey(t => t.NC_FILIAL);

            CreateTable(
                "dbo.FILT_RULES_TO_USERS",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FiltRuleId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                    FiltTargetId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FILT_RULES", t => t.FiltRuleId, cascadeDelete: true)
                .ForeignKey("dbo.FILT_FILTERING_TARGETS", t => t.FiltTargetId, cascadeDelete: true)
                .ForeignKey("dbo.ASP_USERS", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FiltRuleId)
                .Index(t => new { t.UserId, t.FiltTargetId }, unique: true, name: "IX_User_Filt_Targ_Unique");

            CreateTable(
                "dbo.FILT_RULES",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    RULES_JSON = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.FILT_FILTERING_TARGETS",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    NAME = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ASP_USER_LOGINS",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ASP_USERS", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.ASP_USERS_TO_ROLES",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                    LAST_EDITED_BY = c.String(maxLength: 128),
                    CREATED_BY = c.String(maxLength: 128),
                    CREATE_DATE = c.DateTime(),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ASP_USERS", t => t.CREATED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.LAST_EDITED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.ASP_ROLES", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.LAST_EDITED_BY)
                .Index(t => t.CREATED_BY);

            CreateTable(
                "dbo.VK_ADDINF_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false, identity: true),
                    FORM = c.String(maxLength: 3),
                    SECTION = c.String(maxLength: 3),
                    STATUS = c.String(maxLength: 10),
                    AUTHOR = c.String(maxLength: 20),
                    DATE_CREATE = c.DateTime(storeType: "date"),
                    SOURCE = c.String(maxLength: 6),
                    ID_SOURCE = c.Int(),
                    DATE_LOCK = c.DateTime(storeType: "date"),
                    USER_LOCK = c.String(maxLength: 20),
                    DATE_REMOVE = c.DateTime(),
                    NUM_JD = c.String(maxLength: 100),
                    NUM_DVK = c.String(maxLength: 100),
                    DATE_REQUEST = c.DateTime(storeType: "date"),
                    DATE_GET_DOC = c.DateTime(storeType: "date"),
                    SUPDOC_FL = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    SOURCE_NAME = c.String(maxLength: 40),
                    CNUM = c.String(maxLength: 8),
                    CBACCOUNT = c.String(maxLength: 20),
                    IRB_PAYMENT_RK = c.Long(nullable: false),
                    PAYMENT_RK = c.Long(nullable: false),
                    ID_REP = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_HEADER_REP", t => t.ID_REP, cascadeDelete: true)
                .Index(t => t.ID_REP);

            CreateTable(
                "dbo.VK_ADD_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false),
                    CCD_USER = c.String(maxLength: 40),
                    NARRATIVE = c.String(maxLength: 255),
                    CITY_CODE = c.String(maxLength: 4),
                    NOTICE_REPORT = c.String(maxLength: 255),
                    DEP_ACC = c.String(maxLength: 4),
                    CONTRACT_NUM = c.String(maxLength: 100),
                    SOURCE_405 = c.String(maxLength: 40),
                    MODIFY_DATE = c.DateTime(),
                    MODIFY_USER = c.String(maxLength: 15),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER)
                .Index(t => t.ID_OPER);

            CreateTable(
                "dbo.VK_BASIC_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false),
                    FILIAL = c.String(maxLength: 4),
                    POSTDATE = c.DateTime(storeType: "date"),
                    TYPE_TOOLING = c.String(maxLength: 20),
                    OPER_TYPE = c.Byte(),
                    DIRECTION_PAY = c.String(maxLength: 10),
                    COUNT = c.Long(),
                    SHARE = c.Decimal(precision: 18, scale: 6),
                    CCY = c.String(maxLength: 3),
                    AMOUNT_ALL = c.Decimal(precision: 18, scale: 2),
                    AMNT_INCOME = c.Decimal(precision: 18, scale: 2),
                    REF_NUM = c.String(maxLength: 50),
                    CODE_VO = c.String(maxLength: 5),
                    VALUEDATE = c.DateTime(storeType: "date"),
                    SWIFT = c.String(maxLength: 11),
                    SWIFT_FIL = c.String(maxLength: 3),
                    SettingCodeVO_Id = c.Int(),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER)
                .ForeignKey("dbo.DIC_SETTING_CODE_VO", t => t.SettingCodeVO_Id)
                .Index(t => t.ID_OPER)
                .Index(t => t.SettingCodeVO_Id);

            CreateTable(
                "dbo.VK_CLIENT_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false),
                    NAME_REZ = c.String(maxLength: 255),
                    TYPE_REZ = c.String(maxLength: 2),
                    BIC_PARTNER = c.String(maxLength: 12),
                    COUNTRY_REZ = c.String(maxLength: 3),
                    INN = c.String(maxLength: 12),
                    NAME_NEREZ = c.String(maxLength: 255),
                    TYPE_NEREZ = c.String(maxLength: 2),
                    COUNTRY_NEREZ = c.String(maxLength: 3),
                    BANK_NAME = c.String(maxLength: 255),
                    COUNTRY_BANK = c.String(maxLength: 3),
                    PARTNER_NAME = c.String(maxLength: 255),
                    COUNTRY_PARTNER = c.String(maxLength: 3),
                    CUSTNO = c.String(maxLength: 8),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER)
                .Index(t => t.ID_OPER);

            CreateTable(
                "dbo.VK_COMMENTS_REP",
                c => new
                {
                    ID_KEY = c.Long(nullable: false, identity: true),
                    ID_OPER = c.Long(nullable: false),
                    DATE = c.DateTime(),
                    USER_ID = c.String(maxLength: 128),
                    COMMENTS = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.ID_KEY)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER, cascadeDelete: true)
                .ForeignKey("dbo.ASP_USERS", t => t.USER_ID)
                .Index(t => t.ID_OPER)
                .Index(t => t.USER_ID);

            CreateTable(
                "dbo.VK_HEADER_REP",
                c => new
                {
                    ID_REP = c.Int(nullable: false, identity: true),
                    BEGIN_DATE = c.DateTime(nullable: false, storeType: "date"),
                    END_DATE = c.DateTime(nullable: false, storeType: "date"),
                    LAST_EDITED_BY = c.String(maxLength: 128),
                    CREATED_BY = c.String(maxLength: 128),
                    USER_ARH = c.String(maxLength: 128),
                    CREATE_DATE = c.DateTime(),
                    ARCHIVE_DATE = c.DateTime(storeType: "date"),
                    COMMENT = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.ID_REP)
                .ForeignKey("dbo.ASP_USERS", t => t.USER_ARH)
                .ForeignKey("dbo.ASP_USERS", t => t.CREATED_BY)
                .ForeignKey("dbo.ASP_USERS", t => t.LAST_EDITED_BY)
                .Index(t => new { t.BEGIN_DATE, t.END_DATE }, unique: true, name: "IX_Name_Begin_End_Dates")
                .Index(t => t.LAST_EDITED_BY)
                .Index(t => t.CREATED_BY)
                .Index(t => t.USER_ARH);

            CreateTable(
                "dbo.VK_ISSUER_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false),
                    ISSUER_NAME = c.String(maxLength: 255),
                    SECURITY_CODE = c.String(maxLength: 40),
                    REG_NUM_ISSUER = c.String(maxLength: 120),
                    DATE_REG_ISSUER = c.DateTime(storeType: "date"),
                    REPAY_DATE = c.DateTime(storeType: "date"),
                    CCY_ISSUER = c.String(maxLength: 3),
                    ISSUER_CODE = c.Byte(),
                    ISSUER_REESTR = c.String(maxLength: 40),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER)
                .Index(t => t.ID_OPER);

            CreateTable(
                "dbo.VK_NOTICE_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false),
                    NOTICE = c.String(maxLength: 255),
                    NOTICE_ISSUER = c.String(maxLength: 40),
                    NOTICE_EXCHANGE = c.String(maxLength: 40),
                    NOTICE_INST = c.String(maxLength: 40),
                    NOTICE_PROPERTY = c.String(maxLength: 40),
                    NOTICE_BANK = c.String(maxLength: 40),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER)
                .Index(t => t.ID_OPER);

            CreateTable(
                "dbo.VK_406_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false),
                    OPER_KIND = c.Byte(),
                    NOTICE_40 = c.String(maxLength: 40),
                    AMOUNT_TO = c.Decimal(precision: 18, scale: 2),
                    AMOUNT_FROM = c.Decimal(precision: 18, scale: 2),
                    BIC_BANK = c.String(maxLength: 12),
                    COUNTRY_BANK_406 = c.String(maxLength: 3),
                    ADD_CODE_10 = c.String(maxLength: 32),
                    KIND_REZ = c.String(maxLength: 10),
                    INN_NEREZ = c.String(maxLength: 15),
                    KIND_NEREZ = c.String(maxLength: 3),
                    KIND_CONTRACT = c.String(maxLength: 3),
                    TYPE_CONTRACT = c.String(maxLength: 3),
                    NOTICE_15 = c.String(maxLength: 40),
                    NOTICE_16 = c.String(maxLength: 40),
                    NOTICE_OTHER = c.String(maxLength: 40),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER)
                .Index(t => t.ID_OPER);

            CreateTable(
                "dbo.VK_SUPDOC_REP",
                c => new
                {
                    ID_OPER = c.Long(nullable: false),
                    CODE_SUPDOC = c.String(maxLength: 4),
                    SUPDOC_NAME = c.String(maxLength: 255),
                    PRINVEST = c.Byte(),
                    ENTRY_DATE = c.DateTime(storeType: "date"),
                    SUPDOC_TYPE = c.String(maxLength: 1, fixedLength: true),
                    REG_NUM_BRANCH = c.String(maxLength: 4),
                    CLIENT_NAME = c.String(maxLength: 250),
                    SOURCE_SUPDOC = c.String(maxLength: 50),
                    PASSPORT_NUM = c.String(maxLength: 20),
                    DOC_DATE = c.DateTime(),
                    UPDATE_SD_DATE = c.DateTime(),
                })
                .PrimaryKey(t => t.ID_OPER)
                .ForeignKey("dbo.VK_ADDINF_REP", t => t.ID_OPER)
                .Index(t => t.ID_OPER);

            CreateTable(
                "dbo.DIC_ADD_VALUE_10",
                c => new
                {
                    ADD_VALUE = c.Byte(nullable: false),
                    DESC_ADD = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.ADD_VALUE);

            CreateTable(
                "etl.ETL_BO_3130_406",
                c => new
                {
                    IRB_PAYMENT_RK = c.Long(nullable: false),
                    IRB_PAYMENT_SK = c.Long(nullable: false),
                    REPORT_DT = c.DateTime(storeType: "date"),
                    BRANCH_ID = c.String(maxLength: 32),
                    PAYMENT_DT = c.DateTime(storeType: "date"),
                    OPERATION_TYPE_CD = c.String(maxLength: 10),
                    SHORT_DESC = c.String(maxLength: 10),
                    CCD_CURRENCY_ID = c.String(maxLength: 32),
                    TO_NONRES_PAYMENT_AMT = c.Decimal(precision: 18, scale: 2),
                    FROM_NONRES_PAYMENT_AMT = c.Decimal(precision: 18, scale: 2),
                    BIK_NO = c.String(maxLength: 20),
                    ORG_SWIFT_ID = c.String(maxLength: 8),
                    BRANCH_SWIFT_ID = c.String(maxLength: 5),
                    COUNTRY_NUM_CD = c.String(maxLength: 3),
                    RESIDENT_FULL_NM = c.String(maxLength: 100),
                    ADD_10_CODE_TXT = c.String(maxLength: 32),
                    RESIDENT_INN_NO = c.Long(),
                    BUYER_RESIDENT_TXT = c.String(maxLength: 32),
                    NONRESIDENT_FULL_NM = c.String(maxLength: 100),
                    NONRESIDENT_INN_NO = c.Long(),
                    NONRESIDENT_TYPE_CD = c.String(maxLength: 3),
                    DEAL_TYPE_CD = c.String(maxLength: 3),
                    DEAL_CD = c.String(maxLength: 3),
                    CHAPTER_15_TXT = c.String(maxLength: 40),
                    CHAPTER_16_TXT = c.String(maxLength: 40),
                    OTHER_TXT = c.String(maxLength: 40),
                    REFERENCE_ID = c.String(maxLength: 32),
                    VO_CODE = c.String(maxLength: 5),
                    CCD_AUTHORIZATION_USER_ID = c.String(maxLength: 15),
                    NARRATIVE_TXT = c.String(maxLength: 255),
                    CITY_ID = c.String(maxLength: 5),
                    CCD_NARRATIVE_TXT = c.String(maxLength: 255),
                    OFFICE_ID = c.String(maxLength: 4),
                    PAYMENT_CONTRACT_ID = c.String(maxLength: 100),
                    SOURCE_405_TXT = c.String(maxLength: 40),
                    DOC_UPDATE_DT = c.DateTime(storeType: "date"),
                    UPDATE_USER_ID = c.String(maxLength: 15),
                    RESIDENT_CNUM_CD = c.String(maxLength: 10),
                    DWH_DATA_MART = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.IRB_PAYMENT_RK)
                .Index(t => t.IRB_PAYMENT_SK, unique: true, name: "IX_IrbPaymentSK");

            CreateTable(
                "etl.ETL_BO_3132",
                c => new
                {
                    IRB_PAYMENT_RK = c.Long(nullable: false),
                    IRB_PAYMENT_SK = c.Long(nullable: false),
                    REPORT_DT = c.DateTime(storeType: "date"),
                    PAYMENT_DIRECTION_TXT = c.String(maxLength: 40),
                    TRANSFER_SECTION_TXT = c.String(maxLength: 10),
                    TRANSFER_DIRECTION_TXT = c.String(maxLength: 10),
                    LEGAL_FORM_ID = c.String(maxLength: 10),
                    BANK_COUNTRY_ID = c.String(maxLength: 10),
                    OPERATION_CODE_ID = c.String(maxLength: 10),
                    CURRENCY_ID = c.String(maxLength: 10),
                    PAYMENT_AMT = c.Decimal(precision: 18, scale: 2),
                    PAYMENT_DT = c.DateTime(storeType: "date"),
                    PAYMENT_REFERENCE_ID = c.String(maxLength: 40),
                    PAYMENT_REFERENCE_ID_2 = c.String(maxLength: 10),
                    BRANCH_ID = c.String(maxLength: 10),
                    NARRATIVE_TXT = c.String(maxLength: 255),
                    SND_CB_ACCOUNT_ID = c.String(maxLength: 20),
                    SND_NM = c.String(maxLength: 100),
                    SND_BANK_NM = c.String(maxLength: 100),
                    SND_BIK_NO = c.String(maxLength: 20),
                    RCV_CB_ACCOUNT_ID = c.String(maxLength: 20),
                    RCV_NM = c.String(maxLength: 100),
                    RCV_BANK_NM = c.String(maxLength: 100),
                    RCV_BIK_NO = c.String(maxLength: 20),
                    MODULE_ID = c.String(maxLength: 2),
                    VO_CODE_ID = c.String(maxLength: 5),
                    TG_SND_TYPE_ID = c.String(maxLength: 2),
                    SND_TYPE_ID = c.String(maxLength: 2),
                    TG_BENEF_TYPE_ID = c.String(maxLength: 2),
                    BENEF_TYPE_ID = c.String(maxLength: 2),
                    SND_BRANCH_ID = c.String(maxLength: 10),
                    RCV_BRANCH_ID = c.String(maxLength: 10),
                    REAL_BRANCH_ID = c.String(maxLength: 10),
                    RESIDENT_CNUM_CD = c.String(maxLength: 10),
                    DWH_DATA_MART = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.IRB_PAYMENT_RK)
                .Index(t => t.IRB_PAYMENT_SK, unique: true, name: "IX_IrbPaymentSK");

            CreateTable(
                "etl.ETL_BO_DWH",
                c => new
                {
                    IRB_PAYMENT_RK = c.Long(nullable: false),
                    IRB_PAYMENT_SK = c.Long(nullable: false),
                    REPORT_DT = c.DateTime(storeType: "date"),
                    BRANCH_ID = c.String(maxLength: 32),
                    PAYMENT_DT = c.DateTime(storeType: "date"),
                    CODE_405_ID = c.String(maxLength: 32),
                    OPERATION_TYPE_CD = c.String(maxLength: 10),
                    DIRECTION_ID = c.String(maxLength: 10),
                    PARTS_CNT = c.Int(),
                    PART_AMT = c.Decimal(precision: 5, scale: 2),
                    CCD_CURRENCY_ID = c.String(maxLength: 32),
                    PAYMENT_AMT = c.Decimal(precision: 18, scale: 2),
                    PERCENT_PAYMENT_AMT = c.Decimal(precision: 18, scale: 2),
                    RESIDENT_FULL_NM = c.String(maxLength: 100),
                    RESIDENT_ACC_TAG_CD = c.String(maxLength: 10),
                    BIK_NO = c.String(maxLength: 20),
                    COUNTRY_NUM_CD = c.String(maxLength: 3),
                    RESIDENT_INN_NO = c.Long(),
                    NONRESIDENT_FULL_NM = c.String(maxLength: 100),
                    NONRESIDENT_ACC_TAG_CD = c.String(maxLength: 10),
                    NONRESIDENT_COUNTRY_NUM_CD = c.String(maxLength: 3),
                    ISSUER_NM = c.String(maxLength: 40),
                    ISSUER_INN_NO = c.Long(),
                    CB_REG_NO = c.String(maxLength: 12),
                    CB_REG_DT = c.DateTime(storeType: "date"),
                    CB_SET_DT = c.DateTime(storeType: "date"),
                    CB_CURRENCY_ID = c.String(maxLength: 3),
                    ISSUER_TXT = c.String(maxLength: 40),
                    ISSUER_CB_REESTR_TXT = c.String(maxLength: 40),
                    COMMENT_TXT = c.String(maxLength: 40),
                    DIFF_ISSUER_PAYMENT_TXT = c.String(maxLength: 40),
                    CB_MUT_SET_TXT = c.String(maxLength: 40),
                    CB_INST_TXT = c.String(maxLength: 40),
                    PROPERTY_FLG = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    FOR_ACC_OPERATION_TXT = c.String(maxLength: 40),
                    COUNTERPARTY_CONTROL_TXT = c.String(maxLength: 40),
                    COUNTERPARTY_BANK_COUNTRY_CD = c.String(maxLength: 3),
                    COUNTERPARTY_NONRES_NM = c.String(maxLength: 40),
                    COUNTERPARTY_COUNTRY_CD = c.String(maxLength: 3),
                    ORG_SWIFT_ID = c.String(maxLength: 8),
                    BRANCH_SWIFT_ID = c.String(maxLength: 5),
                    REFERENCE_ID = c.String(maxLength: 32),
                    VO_CODE = c.String(maxLength: 5),
                    CCD_AUTHORIZATION_USER_ID = c.String(maxLength: 15),
                    NARRATIVE_TXT = c.String(maxLength: 255),
                    CITY_ID = c.String(maxLength: 5),
                    CCD_NARRATIVE_TXT = c.String(maxLength: 255),
                    OFFICE_ID = c.String(maxLength: 4),
                    PAYMENT_CONTRACT_ID = c.String(maxLength: 100),
                    SOURCE_405_TXT = c.String(maxLength: 40),
                    DOC_UPDATE_DT = c.DateTime(nullable: false, storeType: "date"),
                    UPDATE_USER_ID = c.String(maxLength: 15),
                    RESIDENT_CNUM_CD = c.String(maxLength: 10),
                    DWH_DATA_MART = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.IRB_PAYMENT_RK)
                .Index(t => t.IRB_PAYMENT_SK, unique: true, name: "IX_IrbPaymentSK");

            CreateTable(
                "dbo.DIC_CALENDAR",
                c => new
                {
                    DAT = c.DateTime(nullable: false, storeType: "date"),
                    CCY = c.String(maxLength: 3),
                    HOLIDAY = c.String(maxLength: 1, fixedLength: true, unicode: false),
                })
                .PrimaryKey(t => t.DAT);

            CreateTable(
                "dbo.ADM_CHECK_ERROR",
                c => new
                {
                    ID_HIST = c.Int(nullable: false, identity: true),
                    ID_CHECK = c.Byte(nullable: false),
                    FILIAL = c.String(maxLength: 4),
                    TYPE_REZ = c.String(maxLength: 2),
                    REF_NUM = c.String(maxLength: 32),
                    POSTDATE = c.DateTime(nullable: false, storeType: "date"),
                    AMOUNT_ALL = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.ID_HIST);

            CreateTable(
                "dbo.ADM_CHECK_HEADER",
                c => new
                {
                    ID_HIST = c.Int(nullable: false, identity: true),
                    ID_REP = c.Int(nullable: false),
                    NAME = c.String(nullable: false, maxLength: 15),
                    USER_ID = c.String(nullable: false, maxLength: 20),
                    DTM_START = c.DateTime(nullable: false),
                    END_START = c.DateTime(nullable: false),
                    STATUS = c.String(nullable: false, maxLength: 5),
                })
                .PrimaryKey(t => t.ID_HIST);

            CreateTable(
                "dbo.ADM_CHECK_LIST",
                c => new
                {
                    ID_CHECK = c.Byte(nullable: false),
                    CHECK_NAME = c.String(nullable: false, maxLength: 65),
                    COMMENTS = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.ID_CHECK);

            CreateTable(
                "dbo.ADM_CHECK_STEP",
                c => new
                {
                    ID_HIST = c.Int(nullable: false, identity: true),
                    ID_CHECK = c.Byte(nullable: false),
                    DTM_START = c.DateTime(nullable: false),
                    END_START = c.DateTime(nullable: false),
                    STATUS = c.String(maxLength: 5),
                })
                .PrimaryKey(t => t.ID_HIST);

            CreateTable(
                "dbo.DIC_CODE_TOOLING",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    TYPE_TOOLING = c.String(nullable: false, maxLength: 20),
                    DESC_TOOLING = c.String(maxLength: 255),
                    BEG_DATE = c.DateTime(nullable: false, storeType: "date"),
                    END_DATE = c.DateTime(storeType: "date"),
                })
                .PrimaryKey(t => t.ID_KEY)
                .Index(t => t.TYPE_TOOLING, unique: true, name: "IX_TypeTooling");

            CreateTable(
                "dbo.DIC_SETTING_CODE_VO",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    CODE_VO = c.String(nullable: false, maxLength: 5),
                    REPLACE405 = c.String(maxLength: 5),
                    CODE_VO_DESC = c.String(maxLength: 512),
                    CODE_405_DESC = c.String(maxLength: 255),
                    OPERATION_CODE = c.String(maxLength: 5),
                    SECTION = c.Short(),
                    DIRECTION_PAY = c.Byte(),
                    ISSUER_NEREZ = c.Boolean(),
                    ISSUER_REZ = c.Boolean(),
                    PROPERTY_FLG = c.Boolean(),
                    INCLUDE_405 = c.Boolean(),
                    INCLUDE_406 = c.Boolean(),
                })
                .PrimaryKey(t => t.ID_KEY)
                .ForeignKey("dbo.DIC_DIRECTION_CODES", t => t.OPERATION_CODE)
                .ForeignKey("dbo.DIC_SETTING_CODE_VO_SECTIONS", t => t.SECTION)
                .Index(t => t.CODE_VO, unique: true, name: "IX_CodeVO")
                .Index(t => t.OPERATION_CODE)
                .Index(t => t.SECTION);

            CreateTable(
                "dbo.DIC_DIRECTION_CODES",
                c => new
                {
                    DIRECTION_PAY = c.String(nullable: false, maxLength: 5),
                    DESC_DIRECT = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.DIRECTION_PAY);

            CreateTable(
                "dbo.DIC_SETTING_CODE_VO_SECTIONS",
                c => new
                {
                    SectionNo = c.Short(nullable: false),
                    Desc = c.String(),
                })
                .PrimaryKey(t => t.SectionNo);

            CreateTable(
                "dbo.DIC_CODE_VO_REP",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    REPORT = c.String(nullable: false, maxLength: 3),
                    SECTION = c.String(maxLength: 3),
                    CODE_VO = c.String(nullable: false, maxLength: 5),
                    DESC_CODE_VO = c.String(maxLength: 512),
                })
                .PrimaryKey(t => t.ID_KEY)
                .Index(t => new { t.REPORT, t.CODE_VO }, unique: true, name: "IX_ReportCodeVO");

            CreateTable(
                "dbo.ADM_DWH_LOAD",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    DATE_LOAD = c.DateTime(nullable: false, storeType: "date"),
                    ID_DWH = c.Int(nullable: false),
                    STATE = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Dwhs_Id = c.Int(),
                })
                .PrimaryKey(t => t.ID_KEY)
                .ForeignKey("etl.ETL_DWHS", t => t.Dwhs_Id)
                .Index(t => t.Dwhs_Id);

            CreateTable(
                "etl.ETL_DWHS",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    PARAM_NAME = c.String(maxLength: 80),
                    PARAM_VALUE = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    START_LOAD = c.DateTime(),
                    END_LOAD = c.DateTime(),
                    ID_REP = c.Int(),
                    LAUNCH_AUTHOR = c.String(maxLength: 20),
                })
                .PrimaryKey(t => t.ID_KEY);

            CreateTable(
                "dbo.DIC_ISSUER_CODE",
                c => new
                {
                    ISSUER_CODE = c.Byte(nullable: false),
                    DESC_ISSUER_CODE = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.ISSUER_CODE);

            CreateTable(
                "dbo.DIC_KEYWORDS",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    KEYWORD = c.String(maxLength: 100),
                    DESCRIPTION = c.String(maxLength: 255),
                    BEG_DATE = c.DateTime(nullable: false, storeType: "date"),
                    END_DATE = c.DateTime(storeType: "date"),
                })
                .PrimaryKey(t => t.ID_KEY)
                .Index(t => t.KEYWORD, unique: true, name: "IX_Keyword");

            CreateTable(
                "dbo.DIC_KEYWORDS_EXCEPTION",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    KEYWORD = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.ID_KEY)
                .Index(t => t.KEYWORD, unique: true, name: "IX_KeywordException_Keyword");

            CreateTable(
                "dbo.DIC_KIND_CONTRACT",
                c => new
                {
                    KIND_CONTRACT = c.String(nullable: false, maxLength: 2),
                    DESC_KIND = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.KIND_CONTRACT);

            CreateTable(
                "dbo.DIC_KIND_NEREZ",
                c => new
                {
                    KIND_NEREZ = c.String(nullable: false, maxLength: 1, fixedLength: true),
                    DESC_KIND = c.String(maxLength: 150),
                })
                .PrimaryKey(t => t.KIND_NEREZ);

            CreateTable(
                "dbo.DIC_KIND_REZ",
                c => new
                {
                    KIND_REZ = c.String(nullable: false, maxLength: 10),
                    DESC_KIND = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.KIND_REZ);

            CreateTable(
                "out.OUT_LOG",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    DATE_LOG = c.DateTime(nullable: false, storeType: "date"),
                    STATUS = c.String(maxLength: 1),
                    START_DATE = c.DateTime(nullable: false),
                    END_DATE = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID_KEY);

            CreateTable(
                "dbo.DIC_OPER_KIND",
                c => new
                {
                    OPER_KIND = c.Byte(nullable: false),
                    DESC_OPER_KIND = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.OPER_KIND);

            CreateTable(
                "dbo.DIC_OPER_TYPE",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    OPER_TYPE = c.Byte(nullable: false),
                    OPER_TYPE_DESC = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.ID_KEY)
                .Index(t => t.OPER_TYPE, unique: true, name: "IX_OperType");

            CreateTable(
                "dbo.DIC_PD_TYPE",
                c => new
                {
                    ID_KEY = c.Int(nullable: false, identity: true),
                    CODE_PD = c.String(nullable: false, maxLength: 4),
                    DESC_CODE_PD = c.String(maxLength: 255),
                    BEG_DATE = c.DateTime(nullable: false, storeType: "date"),
                    END_DATE = c.DateTime(storeType: "date"),
                })
                .PrimaryKey(t => t.ID_KEY)
                .Index(t => t.CODE_PD, unique: true, name: "IX_CodePD");

            CreateTable(
                "dbo.ASP_ROLES",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Desc = c.String(maxLength: 30),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.DIC_SECTION",
                c => new
                {
                    SECTION = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                })
                .PrimaryKey(t => t.SECTION);

            CreateTable(
                "dbo.DIC_STATUS_OPER",
                c => new
                {
                    STATUS = c.String(nullable: false, maxLength: 10),
                    NAME_STATUS = c.String(maxLength: 35),
                    SUPDOC_FL = c.String(maxLength: 1, fixedLength: true, unicode: false),
                })
                .PrimaryKey(t => t.STATUS);

            CreateTable(
                "dbo.DIC_STATUS_REZ",
                c => new
                {
                    STATUS = c.String(nullable: false, maxLength: 5),
                    NAME_STATUS = c.String(maxLength: 35),
                })
                .PrimaryKey(t => t.STATUS);

            CreateTable(
                "dbo.DIC_TYPE_CLIENT",
                c => new
                {
                    TYPE_CLIENT = c.String(nullable: false, maxLength: 2),
                    DESCRIPTION = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.TYPE_CLIENT);

            CreateTable(
                "etl.ETL_TBCVK",
                c => new
                {
                    N_PC = c.String(nullable: false, maxLength: 22),
                    LOAD_TYPE = c.String(maxLength: 4),
                    CODE_SUPDOC = c.String(maxLength: 2),
                    PRINVEST = c.String(maxLength: 1, fixedLength: true),
                    ENTRY_DATE = c.DateTime(storeType: "date"),
                    SUPDOC_TYPE = c.String(maxLength: 1, fixedLength: true),
                    REG_NUM_BRANCH = c.String(maxLength: 4),
                    CLIENT_NAME = c.String(maxLength: 250),
                    Amount_SD = c.Decimal(precision: 16, scale: 2),
                    POSTDATE = c.DateTime(storeType: "date"),
                    SOURCE_SUPDOC = c.String(maxLength: 50),
                    PASSPORT_NUM = c.String(maxLength: 20),
                    DOC_DATE = c.DateTime(),
                    UPDATE_SD_DATE = c.DateTime(),
                })
                .PrimaryKey(t => t.N_PC);

            CreateTable(
                "dbo.DIC_TYPE_CONTRACT",
                c => new
                {
                    TYPE_CONTRACT = c.String(nullable: false, maxLength: 1, fixedLength: true),
                    DESC_TYPE_CONTRACT = c.String(maxLength: 200),
                })
                .PrimaryKey(t => t.TYPE_CONTRACT);

            CreateTable(
                "dbo.DIC_ACCT_REPORT_TO_CLIENT_TYPE",
                c => new
                {
                    ACCT_REPORT_ID = c.Int(nullable: false),
                    CLIENT_TYPE_ID = c.Short(nullable: false),
                })
                .PrimaryKey(t => new { t.ACCT_REPORT_ID, t.CLIENT_TYPE_ID })
                .ForeignKey("dbo.DIC_ACCT_REPORT", t => t.ACCT_REPORT_ID, cascadeDelete: true)
                .ForeignKey("dbo.DIC_CLIENT_TYPES", t => t.CLIENT_TYPE_ID, cascadeDelete: true)
                .Index(t => t.ACCT_REPORT_ID)
                .Index(t => t.CLIENT_TYPE_ID);

            CreateTable(
                "dbo.DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS",
                c => new
                {
                    SETTING_CODE_VO_ID = c.Int(nullable: false),
                    CODE_TOOLING_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.SETTING_CODE_VO_ID, t.CODE_TOOLING_ID })
                .ForeignKey("dbo.DIC_SETTING_CODE_VO", t => t.SETTING_CODE_VO_ID, cascadeDelete: true)
                .ForeignKey("dbo.DIC_CODE_TOOLING", t => t.CODE_TOOLING_ID, cascadeDelete: true)
                .Index(t => t.SETTING_CODE_VO_ID)
                .Index(t => t.CODE_TOOLING_ID);

            CreateTable(
                "dbo.DIC_TYPE_CLIENT_TO_STATUS",
                c => new
                {
                    TYPE_CLIENT = c.String(nullable: false, maxLength: 2),
                    STATUS = c.String(nullable: false, maxLength: 5),
                })
                .PrimaryKey(t => new { t.TYPE_CLIENT, t.STATUS })
                .ForeignKey("dbo.DIC_TYPE_CLIENT", t => t.TYPE_CLIENT, cascadeDelete: true)
                .ForeignKey("dbo.DIC_STATUS_REZ", t => t.STATUS, cascadeDelete: true)
                .Index(t => t.TYPE_CLIENT)
                .Index(t => t.STATUS);

        }

        public override void Down()
        {
            DropForeignKey("dbo.DIC_TYPE_CLIENT_TO_STATUS", "STATUS", "dbo.DIC_STATUS_REZ");
            DropForeignKey("dbo.DIC_TYPE_CLIENT_TO_STATUS", "TYPE_CLIENT", "dbo.DIC_TYPE_CLIENT");
            DropForeignKey("dbo.ASP_USERS_TO_ROLES", "RoleId", "dbo.ASP_ROLES");
            DropForeignKey("dbo.ADM_DWH_LOAD", "Dwhs_Id", "etl.ETL_DWHS");
            DropForeignKey("dbo.DIC_SETTING_CODE_VO", "SECTION", "dbo.DIC_SETTING_CODE_VO_SECTIONS");
            DropForeignKey("dbo.DIC_SETTING_CODE_VO", "OPERATION_CODE", "dbo.DIC_DIRECTION_CODES");
            DropForeignKey("dbo.DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS", "CODE_TOOLING_ID", "dbo.DIC_CODE_TOOLING");
            DropForeignKey("dbo.DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS", "SETTING_CODE_VO_ID", "dbo.DIC_SETTING_CODE_VO");
            DropForeignKey("dbo.VK_BASIC_REP", "SettingCodeVO_Id", "dbo.DIC_SETTING_CODE_VO");
            DropForeignKey("dbo.VK_SUPDOC_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.VK_406_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.VK_NOTICE_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.VK_ISSUER_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.VK_ADDINF_REP", "ID_REP", "dbo.VK_HEADER_REP");
            DropForeignKey("dbo.VK_HEADER_REP", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.VK_HEADER_REP", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.VK_HEADER_REP", "USER_ARH", "dbo.ASP_USERS");
            DropForeignKey("dbo.VK_COMMENTS_REP", "USER_ID", "dbo.ASP_USERS");
            DropForeignKey("dbo.VK_COMMENTS_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.VK_CLIENT_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.VK_BASIC_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.VK_ADD_REP", "ID_OPER", "dbo.VK_ADDINF_REP");
            DropForeignKey("dbo.DIC_ACCT_REPORT", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_ACCT_REPORT", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USERS_TO_ROLES", "UserId", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USERS_TO_ROLES", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USERS_TO_ROLES", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USER_LOGINS", "UserId", "dbo.ASP_USERS");
            DropForeignKey("dbo.FILT_RULES_TO_USERS", "UserId", "dbo.ASP_USERS");
            DropForeignKey("dbo.FILT_RULES_TO_USERS", "FiltTargetId", "dbo.FILT_FILTERING_TARGETS");
            DropForeignKey("dbo.FILT_RULES_TO_USERS", "FiltRuleId", "dbo.FILT_RULES");
            DropForeignKey("dbo.ASP_USERS_TO_FILIALS", "USER", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USERS_TO_FILIALS", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USERS_TO_FILIALS", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USERS_TO_FILIALS", "FILIAL", "dbo.DIC_FILIAL");
            DropForeignKey("dbo.ASP_USER_CLAIMS", "UserId", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USER_CLAIMS", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.ASP_USER_CLAIMS", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_ACCT_REPORT_TO_CLIENT_TYPE", "CLIENT_TYPE_ID", "dbo.DIC_CLIENT_TYPES");
            DropForeignKey("dbo.DIC_ACCT_REPORT_TO_CLIENT_TYPE", "ACCT_REPORT_ID", "dbo.DIC_ACCT_REPORT");
            DropIndex("dbo.DIC_TYPE_CLIENT_TO_STATUS", new[] { "STATUS" });
            DropIndex("dbo.DIC_TYPE_CLIENT_TO_STATUS", new[] { "TYPE_CLIENT" });
            DropIndex("dbo.DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS", new[] { "CODE_TOOLING_ID" });
            DropIndex("dbo.DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS", new[] { "SETTING_CODE_VO_ID" });
            DropIndex("dbo.DIC_ACCT_REPORT_TO_CLIENT_TYPE", new[] { "CLIENT_TYPE_ID" });
            DropIndex("dbo.DIC_ACCT_REPORT_TO_CLIENT_TYPE", new[] { "ACCT_REPORT_ID" });
            DropIndex("dbo.ASP_ROLES", "RoleNameIndex");
            DropIndex("dbo.DIC_PD_TYPE", "IX_CodePD");
            DropIndex("dbo.DIC_OPER_TYPE", "IX_OperType");
            DropIndex("dbo.DIC_KEYWORDS_EXCEPTION", "IX_KeywordException_Keyword");
            DropIndex("dbo.DIC_KEYWORDS", "IX_Keyword");
            DropIndex("dbo.ADM_DWH_LOAD", new[] { "Dwhs_Id" });
            DropIndex("dbo.DIC_CODE_VO_REP", "IX_ReportCodeVO");
            DropIndex("dbo.DIC_SETTING_CODE_VO", new[] { "SECTION" });
            DropIndex("dbo.DIC_SETTING_CODE_VO", new[] { "OPERATION_CODE" });
            DropIndex("dbo.DIC_SETTING_CODE_VO", "IX_CodeVO");
            DropIndex("dbo.DIC_CODE_TOOLING", "IX_TypeTooling");
            DropIndex("etl.ETL_BO_DWH", "IX_IrbPaymentSK");
            DropIndex("etl.ETL_BO_3132", "IX_IrbPaymentSK");
            DropIndex("etl.ETL_BO_3130_406", "IX_IrbPaymentSK");
            DropIndex("dbo.VK_SUPDOC_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_406_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_NOTICE_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_ISSUER_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_HEADER_REP", new[] { "USER_ARH" });
            DropIndex("dbo.VK_HEADER_REP", new[] { "CREATED_BY" });
            DropIndex("dbo.VK_HEADER_REP", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.VK_HEADER_REP", "IX_Name_Begin_End_Dates");
            DropIndex("dbo.VK_COMMENTS_REP", new[] { "USER_ID" });
            DropIndex("dbo.VK_COMMENTS_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_CLIENT_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_BASIC_REP", new[] { "SettingCodeVO_Id" });
            DropIndex("dbo.VK_BASIC_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_ADD_REP", new[] { "ID_OPER" });
            DropIndex("dbo.VK_ADDINF_REP", new[] { "ID_REP" });
            DropIndex("dbo.ASP_USERS_TO_ROLES", new[] { "CREATED_BY" });
            DropIndex("dbo.ASP_USERS_TO_ROLES", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.ASP_USERS_TO_ROLES", new[] { "RoleId" });
            DropIndex("dbo.ASP_USERS_TO_ROLES", new[] { "UserId" });
            DropIndex("dbo.ASP_USER_LOGINS", new[] { "UserId" });
            DropIndex("dbo.FILT_RULES_TO_USERS", "IX_User_Filt_Targ_Unique");
            DropIndex("dbo.FILT_RULES_TO_USERS", new[] { "FiltRuleId" });
            DropIndex("dbo.ASP_USERS_TO_FILIALS", new[] { "CREATED_BY" });
            DropIndex("dbo.ASP_USERS_TO_FILIALS", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.ASP_USERS_TO_FILIALS", new[] { "FILIAL" });
            DropIndex("dbo.ASP_USERS_TO_FILIALS", new[] { "USER" });
            DropIndex("dbo.ASP_USER_CLAIMS", new[] { "UserId" });
            DropIndex("dbo.ASP_USER_CLAIMS", new[] { "CREATED_BY" });
            DropIndex("dbo.ASP_USER_CLAIMS", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.ASP_USERS", "UserNameIndex");
            DropIndex("dbo.DIC_CLIENT_TYPES", new[] { "NameShort" });
            DropIndex("dbo.DIC_ACCT_REPORT", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_ACCT_REPORT", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.DIC_ACCT_REPORT", "IX_Acc2");
            DropIndex("dbo.DIC_ACCOUNT_CLIENT", new[] { "CNUM", "CBACCOUNT" });
            DropTable("dbo.DIC_TYPE_CLIENT_TO_STATUS");
            DropTable("dbo.DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS");
            DropTable("dbo.DIC_ACCT_REPORT_TO_CLIENT_TYPE");
            DropTable("dbo.DIC_TYPE_CONTRACT");
            DropTable("etl.ETL_TBCVK");
            DropTable("dbo.DIC_TYPE_CLIENT");
            DropTable("dbo.DIC_STATUS_REZ");
            DropTable("dbo.DIC_STATUS_OPER");
            DropTable("dbo.DIC_SECTION");
            DropTable("dbo.ASP_ROLES");
            DropTable("dbo.DIC_PD_TYPE");
            DropTable("dbo.DIC_OPER_TYPE");
            DropTable("dbo.DIC_OPER_KIND");
            DropTable("out.OUT_LOG");
            DropTable("dbo.DIC_KIND_REZ");
            DropTable("dbo.DIC_KIND_NEREZ");
            DropTable("dbo.DIC_KIND_CONTRACT");
            DropTable("dbo.DIC_KEYWORDS_EXCEPTION");
            DropTable("dbo.DIC_KEYWORDS");
            DropTable("dbo.DIC_ISSUER_CODE");
            DropTable("etl.ETL_DWHS");
            DropTable("dbo.ADM_DWH_LOAD");
            DropTable("dbo.DIC_CODE_VO_REP");
            DropTable("dbo.DIC_SETTING_CODE_VO_SECTIONS");
            DropTable("dbo.DIC_DIRECTION_CODES");
            DropTable("dbo.DIC_SETTING_CODE_VO");
            DropTable("dbo.DIC_CODE_TOOLING");
            DropTable("dbo.ADM_CHECK_STEP");
            DropTable("dbo.ADM_CHECK_LIST");
            DropTable("dbo.ADM_CHECK_HEADER");
            DropTable("dbo.ADM_CHECK_ERROR");
            DropTable("dbo.DIC_CALENDAR");
            DropTable("etl.ETL_BO_DWH");
            DropTable("etl.ETL_BO_3132");
            DropTable("etl.ETL_BO_3130_406");
            DropTable("dbo.DIC_ADD_VALUE_10");
            DropTable("dbo.VK_SUPDOC_REP");
            DropTable("dbo.VK_406_REP");
            DropTable("dbo.VK_NOTICE_REP");
            DropTable("dbo.VK_ISSUER_REP");
            DropTable("dbo.VK_HEADER_REP");
            DropTable("dbo.VK_COMMENTS_REP");
            DropTable("dbo.VK_CLIENT_REP");
            DropTable("dbo.VK_BASIC_REP");
            DropTable("dbo.VK_ADD_REP");
            DropTable("dbo.VK_ADDINF_REP");
            DropTable("dbo.ASP_USERS_TO_ROLES");
            DropTable("dbo.ASP_USER_LOGINS");
            DropTable("dbo.FILT_FILTERING_TARGETS");
            DropTable("dbo.FILT_RULES");
            DropTable("dbo.FILT_RULES_TO_USERS");
            DropTable("dbo.DIC_FILIAL");
            DropTable("dbo.ASP_USERS_TO_FILIALS");
            DropTable("dbo.ASP_USER_CLAIMS");
            DropTable("dbo.ASP_USERS");
            DropTable("dbo.DIC_CLIENT_TYPES");
            DropTable("dbo.DIC_ACCT_REPORT");
            DropTable("dbo.DIC_ACCOUNT_CLIENT");
        }
    }
}
