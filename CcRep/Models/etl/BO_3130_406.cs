using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.etl
{
    [Table("ETL_BO_3130_406", Schema = "etl")]
    public class BO_3130_406
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

        [StringLength(10)]
        [Column("OPERATION_TYPE_CD", TypeName = "NVARCHAR")]
        public string OperationTypeCd { get; set; }

        [StringLength(10)]
        [Column("SHORT_DESC", TypeName = "NVARCHAR")]
        public string ShortDesc { get; set; }

        [StringLength(32)]
        [Column("CCD_CURRENCY_ID", TypeName = "NVARCHAR")]
        public string CcdCurrencyId { get; set; }

        [Column("TO_NONRES_PAYMENT_AMT", TypeName = "DECIMAL")]
        public decimal? ToNonresPaymentAmt { get; set; }

        [Column("FROM_NONRES_PAYMENT_AMT", TypeName = "DECIMAL")]
        public decimal? FromNonresPaymentAmt { get; set; }

        [StringLength(20)]
        [Column("BIK_NO", TypeName = "NVARCHAR")]
        public string BikNo { get; set; }

        [StringLength(8)]
        [Column("ORG_SWIFT_ID", TypeName = "NVARCHAR")]
        public string OrgSwiftId { get; set; }

        [StringLength(5)]
        [Column("BRANCH_SWIFT_ID", TypeName = "NVARCHAR")]
        public string BranchSwiftId { get; set; }

        [StringLength(3)]
        [Column("COUNTRY_NUM_CD", TypeName = "NVARCHAR")]
        public string CountryNumCd { get; set; }

        [StringLength(100)]
        [Column("RESIDENT_FULL_NM", TypeName = "NVARCHAR")]
        public string ResidentFullNm { get; set; }

        [StringLength(32)]
        [Column("ADD_10_CODE_TXT", TypeName = "NVARCHAR")]
        public string Add10CodeTxt { get; set; }

        [Column("RESIDENT_INN_NO", TypeName = "BIGINT")]
        public long? ResidentInnNo { get; set; }

        [StringLength(32)]
        [Column("BUYER_RESIDENT_TXT", TypeName = "NVARCHAR")]
        public string BuyerResidentTxt { get; set; }

        [StringLength(100)]
        [Column("NONRESIDENT_FULL_NM", TypeName = "NVARCHAR")]
        public string NonResidentFullNm { get; set; }

        [Column("NONRESIDENT_INN_NO", TypeName = "BIGINT")]
        public long? NonresidentInnNo { get; set; }

        [StringLength(3)]
        [Column("NONRESIDENT_TYPE_CD", TypeName = "NVARCHAR")]
        public string NONResidentTypeCd { get; set; }

        [StringLength(3)]
        [Column("DEAL_TYPE_CD", TypeName = "NVARCHAR")]
        public string DealTypeCd { get; set; }

        [StringLength(3)]
        [Column("DEAL_CD", TypeName = "NVARCHAR")]
        public string DealCd { get; set; }

        [StringLength(40)]
        [Column("CHAPTER_15_TXT", TypeName = "NVARCHAR")]
        public string Chapter15Txt { get; set; }

        [StringLength(40)]
        [Column("CHAPTER_16_TXT", TypeName = "NVARCHAR")]
        public string Chapter16Txt { get; set; }

        [StringLength(40)]
        [Column("OTHER_TXT", TypeName = "NVARCHAR")]
        public string OtherTxt { get; set; }

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
        public DateTime? DocUpdateDt { get; set; }

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