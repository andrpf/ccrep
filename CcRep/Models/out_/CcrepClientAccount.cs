using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.out_
{
    [Table("OUT_CCREP_CLIENT_ACCOUNT")]
    public class CcrepClientAccount
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        [Column("ACC_NO", TypeName = "NVARCHAR")]
        public string AccNo { get; set; }

        [StringLength(255)]
        [Required]
        [Column("CNUM", TypeName = "NVARCHAR")]
        public string CNum { get; set; }
    }
}