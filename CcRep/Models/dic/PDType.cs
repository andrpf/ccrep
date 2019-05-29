using CcRep.Components.UserAuditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_PD_TYPE")]
    public class PDType : AuditAbstract
    {
        public PDType()
        {
            BegDate = DateTime.Now;
        }

        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(4)]
        [Column("CODE_PD", TypeName = "NVARCHAR")]
        [Required]
        [Index("IX_CodePD", IsUnique = true)]
        [Display(Name = "Код ПД")]
        [Remote("IsTypeNotExists", "api/PDTypes", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Это уже существует")]
        public string CodePD { get; set; }

        [StringLength(255)]
        [Column("DESC_CODE_PD", TypeName = "NVARCHAR")]
        [Display(Name = "Наименование")]
        public string DescCodePD { get; set; }

        [Column("BEG_DATE", TypeName = "DATE")]
        [Required]
        [Display(Name = "Дата начала действия")]
        public DateTime BegDate { get; set; }

        [Column("END_DATE", TypeName = "DATE")]
        [Display(Name = "Дата окончания действия")]
        public DateTime? EndDate { get; set; }
    }
}