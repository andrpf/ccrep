using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.adm
{
    [Table("ADM_CHECK_HEADER")]
    public class CheckHeader
    {
        [Column("ID_HIST")]
        public int Id { get; set; }

        [Column("ID_REP")]
        public int IdRep { get; set; }

        [StringLength(15)]
        [Required]
        [Column("NAME", TypeName = "NVARCHAR")]
        public string Name { get; set; }

        [StringLength(20)]
        [Required]
        [Column("USER_ID", TypeName = "NVARCHAR")]
        public string UserId { get; set; }

        [Column("DTM_START", TypeName = "DATETIME")]
        public DateTime DtmStart { get; set; }

        [Column("END_START", TypeName = "DATETIME")]
        public DateTime EndStart { get; set; }

        [StringLength(5)]
        [Required]
        [Column("STATUS", TypeName = "NVARCHAR")]
        public string Status { get; set; }
    }
}