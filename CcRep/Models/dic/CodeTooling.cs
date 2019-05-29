using CcRep.Components.UserAuditing;
using CcRep.Models.vk;
using CcRep.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_CODE_TOOLING")]
    public class CodeTooling : AuditAbstract
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(20)]
        [Column("TYPE_TOOLING", TypeName = "NVARCHAR")]
        [Required]
        [Index("IX_TypeTooling", IsUnique = true)]
        [Display(Name = "Код инструмента")]
        [Remote("IsTypeNotExists", "api/CodeToolings", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Это уже существует")]
        public string TypeTooling { get; set; }

        [StringLength(255)]
        [Column("DESC_TOOLING", TypeName = "NVARCHAR")]
        [Display(Name = "Наименование")]
        [DataType(DataType.MultilineText)]
        public string DescTooling { get; set; }

        [Column("BEG_DATE", TypeName = "DATE")]
        [Required]
        [DefaultDateTimeValue("Now")]
        [DataType(DataType.Text)]
        [Display(Name = "Дата открытия")]
        public DateTime BegDate { get; set; } = DateTime.Now;

        [Column("END_DATE", TypeName = "DATE")]
        [Display(Name = "Дата закрытия")]
        [DataType(DataType.Text)]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<SettingCodeVO> SettingCodeVOs { get; set; } = new HashSet<SettingCodeVO>();
    }
}