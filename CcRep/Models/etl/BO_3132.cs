using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.etl
{
    [Table("ETL_BO_3132", Schema = "etl")]
    public class BO_3132
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

        [StringLength(40)]
        [Column("PAYMENT_DIRECTION_TXT", TypeName = "NVARCHAR")]
        public string PaymentDirectionTxt { get; set; }

        [StringLength(10)]
        [Column("TRANSFER_SECTION_TXT", TypeName = "NVARCHAR")]
        public string TransferSectionTxt { get; set; }

        [StringLength(10)]
        [Column("TRANSFER_DIRECTION_TXT", TypeName = "NVARCHAR")]
        public string TransferDirectionTxt { get; set; }

        [StringLength(10)]
        [Column("LEGAL_FORM_ID", TypeName = "NVARCHAR")]
        public string LegalFormId { get; set; }

        [StringLength(10)]
        [Column("BANK_COUNTRY_ID", TypeName = "NVARCHAR")]
        public string BankCountryId { get; set; }

        [StringLength(10)]
        [Column("OPERATION_CODE_ID", TypeName = "NVARCHAR")]
        public string OperationCode { get; set; }

        [StringLength(10)]
        [Column("CURRENCY_ID", TypeName = "NVARCHAR")]
        public string CurrencyId { get; set; }

        [Column("PAYMENT_AMT", TypeName = "DECIMAL")]
        public decimal? PaymentAmt { get; set; }

        [Column("PAYMENT_DT", TypeName = "DATE")]
        public DateTime? PaymentDt { get; set; }

        [StringLength(40)]
        [Column("PAYMENT_REFERENCE_ID", TypeName = "NVARCHAR")]
        public string PaymentReferenceId { get; set; }

        [StringLength(10)]
        [Column("PAYMENT_REFERENCE_ID_2", TypeName = "NVARCHAR")]
        public string PaymentReferenceId2 { get; set; }

        [StringLength(10)]
        [Column("BRANCH_ID", TypeName = "NVARCHAR")]
        public string BranchId { get; set; }

        [StringLength(255)]
        [Column("NARRATIVE_TXT", TypeName = "NVARCHAR")]
        public string NarrativeTxt { get; set; }

        [StringLength(20)]
        [Column("SND_CB_ACCOUNT_ID", TypeName = "NVARCHAR")]
        public string SndCBAccountId { get; set; }

        [StringLength(100)]
        [Column("SND_NM", TypeName = "NVARCHAR")]
        public string SndNM { get; set; }

        [StringLength(100)]
        [Column("SND_BANK_NM", TypeName = "NVARCHAR")]
        public string SndBankNm { get; set; }

        [StringLength(20)]
        [Column("SND_BIK_NO", TypeName = "NVARCHAR")]
        public string SndBikNo { get; set; }

        [StringLength(20)]
        [Column("RCV_CB_ACCOUNT_ID", TypeName = "NVARCHAR")]
        public string RcvCBAccountId { get; set; }

        [StringLength(100)]
        [Column("RCV_NM", TypeName = "NVARCHAR")]
        public string RcvNm { get; set; }

        [StringLength(100)]
        [Column("RCV_BANK_NM", TypeName = "NVARCHAR")]
        public string RcvBankNm { get; set; }

        [StringLength(20)]
        [Column("RCV_BIK_NO", TypeName = "NVARCHAR")]
        public string RcvBikNm { get; set; }

        [StringLength(2)]
        [Column("MODULE_ID", TypeName = "NVARCHAR")]
        public string ModuleId { get; set; }

        [StringLength(5)]
        [Column("VO_CODE_ID", TypeName = "NVARCHAR")]
        public string VOCodeId { get; set; }

        [StringLength(2)]
        [Column("TG_SND_TYPE_ID", TypeName = "NVARCHAR")]
        public string TGSndTypeId { get; set; }

        [StringLength(2)]
        [Column("SND_TYPE_ID", TypeName = "NVARCHAR")]
        public string SndTypeId { get; set; }

        [StringLength(2)]
        [Column("TG_BENEF_TYPE_ID", TypeName = "NVARCHAR")]
        public string TGBenefTypeId { get; set; }

        [StringLength(2)]
        [Column("BENEF_TYPE_ID", TypeName = "NVARCHAR")]
        public string BenefTypeId { get; set; }

        [StringLength(10)]
        [Column("SND_BRANCH_ID", TypeName = "NVARCHAR")]
        public string SndBranchId { get; set; }

        [StringLength(10)]
        [Column("RCV_BRANCH_ID", TypeName = "NVARCHAR")]
        public string RcvBranchId { get; set; }

        [StringLength(10)]
        [Column("REAL_BRANCH_ID", TypeName = "NVARCHAR")]
        public string RealBranchId { get; set; }

        [StringLength(10)]
        [Column("RESIDENT_CNUM_CD", TypeName = "NVARCHAR")]
        public string ResidentCNumCD { get; set; }

        [StringLength(20)]
        [Column("DWH_DATA_MART", TypeName = "NVARCHAR")]
        public string DwhDataMart { get; set; }
    }
}