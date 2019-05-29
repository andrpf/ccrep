using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.etl
{
    [Table("ETL_BO_DWH", Schema = "etl")]
    public class BODwh
    {
        [Key, Column("IRB_PAYMENT_RK", TypeName = "BIGINT")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IrbPaymentRK { get; set; }

        [Column("IRB_PAYMENT_SK", TypeName = "BIGINT")]
        [Required]
        [Index("IX_IrbPaymentSK", IsUnique = true)]
        public long IrbPaymentSK { get; set; }

        [Column("REPORT_DT", TypeName = "DATE")]
        public DateTime? ReportDt { get; set; }

        [StringLength(32)]
        [Column("BRANCH_ID", TypeName = "NVARCHAR")]
        public string BranchId { get; set; }

        [Column("PAYMENT_DT", TypeName = "DATE")]
        public DateTime? PaymentDt { get; set; }

        [StringLength(32)]
        [Column("CODE_405_ID", TypeName = "NVARCHAR")]
        public string Code405Id { get; set; }

        [StringLength(10)]
        [Column("OPERATION_TYPE_CD", TypeName = "NVARCHAR")]
        public string OperationTypeCd { get; set; }

        [StringLength(10)]
        [Column("DIRECTION_ID", TypeName = "NVARCHAR")]
        public string DirectionId { get; set; }

        [Column("PARTS_CNT")]
        public int? PartsCnt { get; set; }

        [Column("PART_AMT", TypeName = "DECIMAL")]
        public decimal? PartsAmt { get; set; }

        [StringLength(32)]
        [Column("CCD_CURRENCY_ID", TypeName = "NVARCHAR")]
        public string CcdCurrencyId { get; set; }

        [Column("PAYMENT_AMT", TypeName = "DECIMAL")]
        public decimal? PaymentAmt { get; set; }

        [Column("PERCENT_PAYMENT_AMT", TypeName = "DECIMAL")]
        public decimal? PercentPaymentAmt { get; set; }

        [StringLength(100)]
        [Column("RESIDENT_FULL_NM", TypeName = "NVARCHAR")]
        public string ResidentFullNm { get; set; }

        [StringLength(10)]
        [Column("RESIDENT_ACC_TAG_CD", TypeName = "NVARCHAR")]
        public string ResidentAccTagCd { get; set; }

        [StringLength(20)]
        [Column("BIK_NO", TypeName = "NVARCHAR")]
        public string BikNo { get; set; }

        [StringLength(3)]
        [Column("COUNTRY_NUM_CD", TypeName = "NVARCHAR")]
        public string CountryNumCd { get; set; }

        [Column("RESIDENT_INN_NO", TypeName = "BIGINT")]
        public long? ResidentInnNo { get; set; }

        [StringLength(100)]
        [Column("NONRESIDENT_FULL_NM", TypeName = "NVARCHAR")]
        public string NonResidentFullNm { get; set; }

        [StringLength(10)]
        [Column("NONRESIDENT_ACC_TAG_CD", TypeName = "NVARCHAR")]
        public string NONResidentAccTagCd { get; set; }

        [StringLength(3)]
        [Column("NONRESIDENT_COUNTRY_NUM_CD", TypeName = "NVARCHAR")]
        public string NONResidentCountryNumCd { get; set; }

        [StringLength(40)]
        [Column("ISSUER_NM", TypeName = "NVARCHAR")]
        public string IssuerNm { get; set; }

        [Column("ISSUER_INN_NO", TypeName = "BIGINT")]
        public long? IssuerInnNo { get; set; }

        [StringLength(12)]
        [Column("CB_REG_NO", TypeName = "NVARCHAR")]
        public string CBRegNo { get; set; }

        [Column("CB_REG_DT", TypeName = "DATE")]
        public DateTime? CBRegDt { get; set; }

        [Column("CB_SET_DT", TypeName = "DATE")]
        public DateTime? CBSetDt { get; set; }

        [StringLength(3)]
        [Column("CB_CURRENCY_ID", TypeName = "NVARCHAR")]
        public string CBCurrencyId { get; set; }

        [StringLength(40)]
        [Column("ISSUER_TXT", TypeName = "NVARCHAR")]
        public string IssuerTxt { get; set; }

        [StringLength(40)]
        [Column("ISSUER_CB_REESTR_TXT", TypeName = "NVARCHAR")]
        public string IssuerCBReestrTxt { get; set; }

        [StringLength(40)]
        [Column("COMMENT_TXT", TypeName = "NVARCHAR")]
        public string CommentTxt { get; set; }

        [StringLength(40)]
        [Column("DIFF_ISSUER_PAYMENT_TXT", TypeName = "NVARCHAR")]
        public string DiffIssuerPaymentTxt { get; set; }

        [StringLength(40)]
        [Column("CB_MUT_SET_TXT", TypeName = "NVARCHAR")]
        public string CBMutSetTxt { get; set; }

        [StringLength(40)]
        [Column("CB_INST_TXT", TypeName = "NVARCHAR")]
        public string CBInstTxt { get; set; }

        [StringLength(1)]
        [Column("PROPERTY_FLG", TypeName = "CHAR")]
        public string PropertyFlg { get; set; }

        [StringLength(40)]
        [Column("FOR_ACC_OPERATION_TXT", TypeName = "NVARCHAR")]
        public string ForAccOperationTxt { get; set; }

        [StringLength(40)]
        [Column("COUNTERPARTY_CONTROL_TXT", TypeName = "NVARCHAR")]
        public string CounterpartyControlTxt { get; set; }

        [StringLength(3)]
        [Column("COUNTERPARTY_BANK_COUNTRY_CD", TypeName = "NVARCHAR")]
        public string CounterpartyBankCountryCD { get; set; }

        [StringLength(40)]
        [Column("COUNTERPARTY_NONRES_NM", TypeName = "NVARCHAR")]
        public string CounterpartyNonresName { get; set; }

        [StringLength(3)]
        [Column("COUNTERPARTY_COUNTRY_CD", TypeName = "NVARCHAR")]
        public string CounterpartyCountryCD { get; set; }

        [StringLength(8)]
        [Column("ORG_SWIFT_ID", TypeName = "NVARCHAR")]
        public string OrgSwiftId { get; set; }

        [StringLength(5)]
        [Column("BRANCH_SWIFT_ID", TypeName = "NVARCHAR")]
        public string BranchSwiftId { get; set; }

        [StringLength(32)]
        [Column("REFERENCE_ID", TypeName = "NVARCHAR")]
        public string ReferenceId { get; set; }

        [StringLength(5)]
        [Column("VO_CODE", TypeName = "NVARCHAR")]
        public string VOCode { get; set; }

        [StringLength(15)]
        [Column("CCD_AUTHORIZATION_USER_ID", TypeName = "NVARCHAR")]
        public string CcdAuthorizationUserId { get; set; }

        [StringLength(255)]
        [Column("NARRATIVE_TXT", TypeName = "NVARCHAR")]
        public string NarrativeTxt { get; set; }

        [StringLength(5)]
        [Column("CITY_ID", TypeName = "NVARCHAR")]
        public string CityId { get; set; }

        [StringLength(255)]
        [Column("CCD_NARRATIVE_TXT", TypeName = "NVARCHAR")]
        public string CcdNarrativeTxt { get; set; }

        [StringLength(4)]
        [Column("OFFICE_ID", TypeName = "NVARCHAR")]
        public string OfficeId { get; set; }

        [StringLength(100)]
        [Column("PAYMENT_CONTRACT_ID", TypeName = "NVARCHAR")]
        public string PaymentContractId { get; set; }

        [StringLength(40)]
        [Column("SOURCE_405_TXT", TypeName = "NVARCHAR")]
        public string Source405Txt { get; set; }

        [Column("DOC_UPDATE_DT", TypeName = "DATE")]
        public DateTime DocUpdateDt { get; set; }

        [StringLength(15)]
        [Column("UPDATE_USER_ID", TypeName = "NVARCHAR")]
        public string UpdateUserId { get; set; }

        [StringLength(10)]
        [Column("RESIDENT_CNUM_CD", TypeName = "NVARCHAR")]
        public string ResidentCNumCD { get; set; }

        [StringLength(20)]
        [Column("DWH_DATA_MART", TypeName = "NVARCHAR")]
        public string DwhDataMart { get; set; }
    }
}