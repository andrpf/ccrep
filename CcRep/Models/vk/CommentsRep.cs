using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_COMMENTS_REP")]
    public class CommentsRep
    {
        [Column("ID_KEY")]
        public long Id { get; set; }

        [Column("ID_OPER"), Required]
        [ForeignKey("AddInfRep")]
        public long IdOper { get; set; }
        public AddInfRep AddInfRep { get; set; }

        [Column("DATE")]
        public DateTime? Date { get; set; }

        [Column("USER_ID", TypeName = "NVARCHAR")]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual CcRepUser User { get; set; }

        [StringLength(100)]
        [Column("COMMENTS", TypeName = "NVARCHAR")]
        public string Comments { get; set; }
    }
}