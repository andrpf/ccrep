using CcRep.Components.UserAuditing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_CLIENT_REP")]
    public class ClientRep : AuditAbstract
    {
        [Key, Column("ID_OPER")]
        [ForeignKey("AddInfRep")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdOper { get; set; }
        public AddInfRep AddInfRep { get; set; }

        [StringLength(255)]
        [DisplayName("Наименование резидента")]
        [Column("NAME_REZ", TypeName = "NVARCHAR")]
        public string NameRez { get; set; }

        [StringLength(2)]
        [DisplayName("Тип участника резидента")]
        [Column("TYPE_REZ", TypeName = "NVARCHAR")]
        public string TypeRez { get; set; }

        [StringLength(12)]
        [DisplayName("Бик Банка контрагента резидента")]
        [Column("BIC_PARTNER", TypeName = "NVARCHAR")]
        public string BicPartner { get; set; }

        [StringLength(3)]
        [DisplayName("Код страны Банка резидента")]
        [Column("COUNTRY_REZ", TypeName = "NVARCHAR")]
        public string CountryRez { get; set; }

        [StringLength(12)]
        [DisplayName("ИНН резидента")]
        [Column("INN", TypeName = "NVARCHAR")]
        public string INN { get; set; }

        [StringLength(255)]
        [DisplayName("Наименование нерезидента")]
        [Column("NAME_NEREZ", TypeName = "NVARCHAR")]
        public string NameNerez { get; set; }

        [StringLength(2)]
        [DisplayName("Тип участника нерезидента")]
        [Column("TYPE_NEREZ", TypeName = "NVARCHAR")]
        public string TypeNerez { get; set; }

        [StringLength(3)]
        [DisplayName("Код страны нерезидента")]
        [Column("COUNTRY_NEREZ", TypeName = "NVARCHAR")]
        public string CountryNerez { get; set; }

        [StringLength(255)]
        [DisplayName("Наименование Банка нерезидента")]
        [Column("BANK_NAME", TypeName = "NVARCHAR")]
        public string BankName { get; set; }

        [StringLength(3)]
        [DisplayName("Код страны Банка нерезидента")]
        [Column("COUNTRY_BANK", TypeName = "NVARCHAR")]
        public string CountryBank { get; set; }

        [StringLength(255)]
        [DisplayName("Имя контрагента нерезидента")]
        [Column("PARTNER_NAME", TypeName = "NVARCHAR")]
        public string PartnerName { get; set; }

        [StringLength(3)]
        [DisplayName("Код страны нерезидента")]
        [Column("COUNTRY_PARTNER", TypeName = "NVARCHAR")]
        public string CountryPartner { get; set; }

        [StringLength(8)]
        [DisplayName("Код клиента резидента")]
        [Column("CUSTNO", TypeName = "NVARCHAR")]
        public string CustNo { get; set; }
    }
}