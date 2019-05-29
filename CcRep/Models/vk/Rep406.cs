using CcRep.Components.UserAuditing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_406_REP")]
    public class Rep406 : AuditAbstract
    {
        [Key, Column("ID_OPER")]
        [ForeignKey("AddInfRep")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdOper { get; set; }

        public AddInfRep AddInfRep { get; set; }

        [Column("OPER_KIND", TypeName = "TINYINT")]
        [DisplayName("Код операции")]
        public byte? OperKind { get; set; }

        [StringLength(40)]
        [DisplayName("Назначение по коду 40")]
        [Column("NOTICE_40", TypeName = "NVARCHAR")]
        public string Notice40 { get; set; }

        [Column("AMOUNT_TO", TypeName = "DECIMAL")] 
        [DisplayName("Сумма в пользу нерезидента")]
        public decimal? AmountTo { get; set; }

        [Column("AMOUNT_FROM", TypeName = "DECIMAL")]
        [DisplayName("Сумма поступлений от нерезидента")]
        public decimal? AmountFrom { get; set; }

        [StringLength(12)]
        [DisplayName("Бик Банка")]
        [Column("BIC_BANK", TypeName = "NVARCHAR")]
        public string BicBank { get; set; }

        [StringLength(3)]
        [DisplayName("Код страны банка")]
        [Column("COUNTRY_BANK_406", TypeName = "NVARCHAR")]
        public string CountryBank406 { get; set; }

        [StringLength(32)]
        [DisplayName("Доп-ый код к графе 10")]
        [Column("ADD_CODE_10", TypeName = "NVARCHAR")]
        public string AddCode10 { get; set; }

        [StringLength(10)]
        [DisplayName("Резидент-покупатель")]
        [Column("KIND_REZ", TypeName = "NVARCHAR")]
        public string KindRez { get; set; }

        [StringLength(15)]
        [DisplayName("ИНН нерезидента")]
        [Column("INN_NEREZ", TypeName = "NVARCHAR")]
        public string InnNerez { get; set; }

        [StringLength(3)]
        [DisplayName("Тип нерезидента")]
        [Column("KIND_NEREZ", TypeName = "NVARCHAR")]
        public string KindNerez { get; set; }

        [StringLength(3)]
        [DisplayName("Вид контракта")]
        [Column("KIND_CONTRACT", TypeName = "NVARCHAR")]
        public string KindContract { get; set; }

        [StringLength(3)]
        [DisplayName("Тип контракта")]
        [Column("TYPE_CONTRACT", TypeName = "NVARCHAR")]
        public string TypeContract { get; set; }

        [StringLength(40)]
        [DisplayName("Примечание к графе 15")]
        [Column("NOTICE_15", TypeName = "NVARCHAR")]
        public string Notice15 { get; set; }

        [StringLength(40)]
        [DisplayName("Примечание к графе 16")]
        [Column("NOTICE_16", TypeName = "NVARCHAR")]
        public string Notice16 { get; set; }

        [StringLength(40)]
        [DisplayName("Примечание - другие")]
        [Column("NOTICE_OTHER", TypeName = "NVARCHAR")]
        public string NoticeOther { get; set; }
    }
}