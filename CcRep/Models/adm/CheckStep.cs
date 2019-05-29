using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.adm
{
    [Table("ADM_CHECK_STEP")]
    public class CheckStep
    {
        [Column("ID_HIST")]
        public int Id { get; set; }

        [Column("ID_CHECK", TypeName = "TINYINT")]
        [Required]
        public byte IdCheck { get; set; }

        [Column("DTM_START", TypeName = "DATETIME")]
        public DateTime DtmStart { get; set; }

        [Column("END_START", TypeName = "DATETIME")]
        public DateTime EndStart { get; set; }

        [StringLength(5)]
        [Column("STATUS", TypeName = "NVARCHAR")]
        public string Status { get; set; }
    }
}