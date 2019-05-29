using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.au
{
    [Table("AU_PRM")]
    public class Prm
    {
        [StringLength(10)]
        [Key, Column("PRM_CODE", TypeName = "NVARCHAR")]
        public string PrmCode { get; set; }

        [StringLength(50)]
        [Column("PRM_NAME", TypeName = "NVARCHAR")]
        public string PrmName { get; set; }
    }
}