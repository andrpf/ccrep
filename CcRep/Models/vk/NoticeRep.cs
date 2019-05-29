using CcRep.Components.UserAuditing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_NOTICE_REP")]
    public class NoticeRep : AuditAbstract
    {
        [ForeignKey("AddInfRep")]
        [Key, Column("ID_OPER")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdOper { get; set; }
        public AddInfRep AddInfRep { get; set; }

        [StringLength(255)]
        [DisplayName("Примечание для 405")]
        [Column("NOTICE", TypeName = "NVARCHAR")]
        public string Notice { get; set; }

        [StringLength(40)]
        [DisplayName("Для платежей за пакет Ц/Б")]
        [Column("NOTICE_ISSUER", TypeName = "NVARCHAR")]
        public string NoticeIssuer { get; set; }

        [StringLength(40)]
        [DisplayName("Для операций мены")]
        [Column("NOTICE_EXCHANGE", TypeName = "NVARCHAR")]
        public string NoticeExchange { get; set; }

        [StringLength(40)]
        [DisplayName("Для платежей в рассрочку")]
        [Column("NOTICE_INST", TypeName = "NVARCHAR")]
        public string NoticeInst { get; set; }

        [StringLength(40)]
        [DisplayName("Для вкладов в имущество")]
        [Column("NOTICE_PROPERTY", TypeName = "NVARCHAR")]
        public string NoticeProperty { get; set; }

        [StringLength(40)]
        [DisplayName("Для операций через иностранные банки")]
        [Column("NOTICE_BANK", TypeName = "NVARCHAR")]
        public string NoticeBank { get; set; }
    }
}