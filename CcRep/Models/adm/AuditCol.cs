using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.adm
{
    [Table("ADM_AUDIT_COL")]
    public class AuditCol
    {
        [Key, Column("SEQ_NO", Order = 0, TypeName = "BIGINT")]
        [Required]
        [ForeignKey("AuditLog")]       
        public long SeqNo { get; set; }
        public AuditLog AuditLog { get; set; }

        [Key, Column("COL_NAME", Order = 2, TypeName = "NVARCHAR")]
        [StringLength(30)]
        [Required]
        public string ColName { get; set; }

        [StringLength(255)]
        [Column("OLD_VALUE", TypeName = "NVARCHAR")]
        public string OldValue { get; set; }

        [StringLength(255)]
        [Column("NEW_VALUE", TypeName = "NVARCHAR")]
        public string NewValue { get; set; }
    }
}