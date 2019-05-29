using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_CODE_VO_REP")]
    public class CodeVORep
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(3)]
        [Required]
        [Index("IX_ReportCodeVO", 1, IsUnique = true)]
        [Column("REPORT", TypeName = "NVARCHAR")]
        public string Report { get; set; }

        [StringLength(3)]
        [Column("SECTION", TypeName = "NVARCHAR")]
        public string Section { get; set; }

        [StringLength(5)]
        [Required]
        [Index("IX_ReportCodeVO", 2, IsUnique = true)]
        [Column("CODE_VO", TypeName = "NVARCHAR")]
        public string CodeVO { get; set; } 

        [StringLength(512)]
        [Column("DESC_CODE_VO", TypeName = "NVARCHAR")]
        public string DescCodeVO { get; set; }
    }
}