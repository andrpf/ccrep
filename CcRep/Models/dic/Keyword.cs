using CcRep.Components.UserAuditing;
using CcRep.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_KEYWORDS")]
    public class Keyword : AuditAbstract
    {
        public Keyword()
        {
            BegDate = DateTime.Now;
        }

        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(100)]
        [Column("KEYWORD", TypeName = "NVARCHAR")]
        [Index("IX_Keyword", IsUnique = true)]
        [Display(Name = "Ключевое слово")]
        [Required]
        [Remote("IsKeywordNotExist", "api/Keywords", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Поле должно быть уникальным")]
        public string Key_word { get; set; }

        [Display(Name = "Пояснения")]
        [StringLength(255)]
        [Column("DESCRIPTION", TypeName = "NVARCHAR")]
        public string Description { get; set; }

        [Column("BEG_DATE", TypeName = "DATE")]
        [Required]
        [DefaultDateTimeValue("Now")]
        [DataType(DataType.Text)]
        [Display(Name = "Дата начала действия")]
        public DateTime? BegDate { get; set; }

        [Column("END_DATE", TypeName = "DATE")]
        [Display(Name = "Дата окончания действия")]
        public DateTime? EndDate { get; set; }
    }
}