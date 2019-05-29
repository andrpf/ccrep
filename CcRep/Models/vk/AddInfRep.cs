using System;
using System.ComponentModel;
using System.Collections.Generic;
using CcRep.Components.UserAuditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_ADDINF_REP")]
    public class AddInfRep : AuditAbstract
    {
        [Key,Column("ID_OPER")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdOper { get; set; }

        [StringLength(3)]
        [Column("FORM", TypeName = "NVARCHAR")]
        [DisplayName("Форма")]
        public string Form { get; set; }

        [StringLength(3)]
        [DisplayName("Раздел")]
        [Column("SECTION", TypeName = "NVARCHAR")]
        public string Section { get; set; }

        [StringLength(10)]
        [Column("STATUS", TypeName = "NVARCHAR")]
        [DisplayName("Статус")]
        public string Status { get; set; }

      // public CodeVORep CodeVORep { get; set; }

        [StringLength(20)]
        [Column("AUTHOR", TypeName = "NVARCHAR")]
        [DisplayName("Автор")]
        public string Author { get; set; }

        [Column("DATE_CREATE", TypeName = "DATE")]
        [DisplayName("Дата создания")]
        public DateTime? DateCreate { get; set; }

        [StringLength(6)]
        [DisplayName("Источник")]
        [Column("SOURCE", TypeName = "NVARCHAR")]
        public string Source { get; set; }

        [Column("ID_SOURCE")]
        [DisplayName("ID Источника")]
        public int? IdSource { get; set; }

        [Column("DATE_LOCK", TypeName = "DATE")]
        [DisplayName("Дата блокировки")]
        public DateTime? DateLock { get; set; }

        [StringLength(20)]
        [DisplayName("Кем заблокирован")]
        [Column("USER_LOCK", TypeName = "NVARCHAR")]
        public string UserLock { get; set; }

        [Column("DATE_REMOVE", TypeName = "DATETIME")]
        [DisplayName("Дата удаления")]
        public DateTime? DateRemove { get; set; }

        [StringLength(100)]
        [DisplayName("Номер в JD")]
        [Column("NUM_JD", TypeName = "NVARCHAR")]
        public string NumJD { get; set; }

        [StringLength(100)]
        [DisplayName("Номер в DVK")]
        [Column("NUM_DVK", TypeName = "NVARCHAR")]
        public string NumDVK { get; set; }

        [Column("DATE_REQUEST", TypeName = "DATE")]
        [DisplayName("Дата запроса куратору")]
        public DateTime? DateRequest { get; set; }

        [Column("DATE_GET_DOC", TypeName = "DATE")]
        [DisplayName("Дата получения документов")]
        public DateTime? DateGetDoc { get; set; }

        [StringLength(1)]
        [DisplayName("Признак ПД")]
        [Column("SUPDOC_FL", TypeName = "CHAR")]
        public string SupdocFl { get; set; }

        [StringLength(40)]
        [Column("SOURCE_NAME", TypeName = "NVARCHAR")]
        public string SourceName { get; set; }

        [StringLength(8)]
        [DisplayName("Клиентский номер")]
        [Column("CNUM", TypeName = "NVARCHAR")]
        public string CNum { get; set; }

        [StringLength(20)]
        [DisplayName("Номер счета")]
        [Column("CBACCOUNT", TypeName = "NVARCHAR")]
        public string CBAccount { get; set; }

        [Column("IRB_PAYMENT_RK", TypeName = "BIGINT")]
        [Required]
        public long IrbPaymentRK { get; set; }

        [Column("PAYMENT_RK", TypeName = "BIGINT")]
        [Required]
        public long PaymentRK { get; set; }

        [Column("ID_REP")]
        [Required]
        [ForeignKey("HeaderRep")]
        public int IdRep { get; set; }
        public HeaderRep HeaderRep { get; set; }

        public ICollection<CommentsRep> CommentsReps { get; set; }

        public virtual BasicRep BasicRep { get; set; }
        public virtual ClientRep ClientRep { get; set; }
        public virtual IssuerRep IssuerRep { get; set; }
        public virtual NoticeRep NoticeRep { get; set; }
        public virtual AddRep AddRep { get; set; }
        public virtual Rep406 Rep406 { get; set; }
        public virtual SupdocRep SupdocRep { get; set; }
    }
}