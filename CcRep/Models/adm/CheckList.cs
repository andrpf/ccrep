using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.adm
{
    [Table("ADM_CHECK_LIST")]
    public class CheckList
    {
        [Key, Column("ID_CHECK", TypeName = "TINYINT")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte IdCheck { get; set; }

        [StringLength(65)]
        [Required]
        [Column("CHECK_NAME", TypeName = "NVARCHAR")]
        public string CheckName { get; set; }

        [StringLength(255)]
        [Required]
        [Column("COMMENTS", TypeName = "NVARCHAR")]
        public string Comments { get; set; }
    }
}