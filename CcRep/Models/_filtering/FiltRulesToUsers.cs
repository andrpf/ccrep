using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models._filtering
{
    [Table("FILT_RULES_TO_USERS")]
    public class FiltRulesToUsers
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("FiltRule")]
        public int FiltRuleId { get; set; }
        public FiltRule FiltRule { get; set; }

        [Required]
        [ForeignKey("User")]
        [Index("IX_User_Filt_Targ_Unique", 1, IsUnique = true)]
        public string UserId { get; set; }
        public CcRepUser User { get; set; }

        [Required]
        [ForeignKey("FiltTarget")]
        [Index("IX_User_Filt_Targ_Unique", 2, IsUnique = true)]
        public int FiltTargetId { get; set; }
        public FiltTarget FiltTarget { get; set; }
    }
}