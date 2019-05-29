using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.out_
{
    [Table("OUT_CCREP_KEYWORDS")]
    public class CcrepKeyWords
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        [Column("KEYWORD_TXT", TypeName = "NVARCHAR")]
        public string KeywordTxt { get; set; }
    }
}