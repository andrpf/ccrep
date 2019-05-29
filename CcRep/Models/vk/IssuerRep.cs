using CcRep.Components.UserAuditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_ISSUER_REP")]
    public class IssuerRep : AuditAbstract
    {
        [Key, Column("ID_OPER")]
        [ForeignKey("AddInfRep")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdOper { get; set; }
        public AddInfRep AddInfRep { get; set; }

        [StringLength(255)]
        [DisplayName("Нименование эмитета Ц/Б")]
        [Column("ISSUER_NAME", TypeName = "NVARCHAR")]
        public string IssuerName { get; set; }

        [StringLength(40)]
        [DisplayName("Код эмитета Ц/Б")]
        [Column("SECURITY_CODE", TypeName = "NVARCHAR")]
        public string SecurityCode { get; set; }

        [StringLength(120)]
        [DisplayName("Регистрационный номер Ц/Б")]
        [Column("REG_NUM_ISSUER", TypeName = "NVARCHAR")]
        public string RegNumIssuer { get; set; }

        [Column("DATE_REG_ISSUER", TypeName = "DATE")]
        [DisplayName("Дата регистрации выпуска")]
        public DateTime? DateRegIssuer { get; set; }

        [Column("REPAY_DATE", TypeName = "DATE")]
        [DisplayName("Дата погашения Ц/Б")]
        public DateTime? RepayDate { get; set; }

        [StringLength(3)]
        [DisplayName("Валюта Ц/Б")]
        [Column("CCY_ISSUER", TypeName = "NVARCHAR")]
        public string CcyIssuer { get; set; }

        [Column("ISSUER_CODE", TypeName = "TINYINT")]
        [DisplayName("Тип эмитета")]
        public byte? IssuerCode { get; set; }

        [StringLength(40)]
        [DisplayName("Код эмитета из реестра Ц/Б")]
        [Column("ISSUER_REESTR", TypeName = "NVARCHAR")]
        public string IssuerReestr { get; set; }
    }
}