using CcRep.Components.UserAuditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_KEYWORDS_EXCEPTION")]
    public class KeywordsException : AuditAbstract
    {
        [Column("ID_KEY")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("KEYWORD", TypeName = "NVARCHAR")]
        [Index("IX_KeywordException_Keyword", IsUnique = true)]
        [Display(Name = "Слово исключение")]
        [Required]
        [Remote("IsTypeNotExists", "api/KeywordsExceptions", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Поле должно быть уникальным")]
        public string Keyword { get; set; }
    }
}