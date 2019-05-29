using CcRep.Components.UserAuditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_ADD_REP")]
    public class AddRep : AuditAbstract
    {
        [Key, Column("ID_OPER")]
        [ForeignKey("AddInfRep")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdOper { get; set; }
        public AddInfRep AddInfRep { get; set; }

        [StringLength(40)]
        [DisplayName("CCD-пользователь")]
        [Column("CCD_USER", TypeName = "NVARCHAR")]
        public string CcdUser { get; set; }

        [StringLength(255)]
        [DisplayName("Назначение")]
        [Column("NARRATIVE", TypeName = "NVARCHAR")]
        public string Narrative { get; set; }

        [StringLength(4)]
        [DisplayName("Код города")]
        [Column("CITY_CODE", TypeName = "NVARCHAR")]
        public string CityCode { get; set; }

        [StringLength(255)]
        [DisplayName("Примечание")]
        [Column("NOTICE_REPORT", TypeName = "NVARCHAR")]
        public string NoticeReport { get; set; }

        [StringLength(4)]
        [DisplayName("Офис по счету")]
        [Column("DEP_ACC", TypeName = "NVARCHAR")]
        public string DepAcc { get; set; }

        [StringLength(100)]
        [DisplayName("Номер контракта")]
        [Column("CONTRACT_NUM", TypeName = "NVARCHAR")]
        public string ContractNum { get; set; }

        [StringLength(40)]
        [DisplayName("Источник 405")]
        [Column("SOURCE_405", TypeName = "NVARCHAR")]
        public string Source405 { get; set; }

        [Column("MODIFY_DATE", TypeName = "DATETIME")]
        [DisplayName("Дата изменения")]
        public DateTime? ModifyDate { get; set; }

        [StringLength(15)]
        [DisplayName("Кто изменил")]
        [Column("MODIFY_USER", TypeName = "NVARCHAR")]
        public string ModifyUser { get; set; }
    }
}