using CcRep.Components.UserAuditing;
using CcRep.Models.dic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models
{
    [Table("ASP_USERS_TO_FILIALS")]
    public class UsersToFilials : IDateAuditing, IUserAuditing
    {
        [Key]
        public int Id { get; set; }

        [Column("USER", TypeName = "NVARCHAR")]
        [ForeignKey("User")]
        [StringLength(128)]
        [Required]
        public string UserId { get; set; }
        public CcRepUser User { get; set; }

        [StringLength(4)]
        [ForeignKey("Filial")]
        [Required]
        [Column("FILIAL", TypeName = "NVARCHAR")]
        public string FilialId { get; set; }
        public Filial Filial { get; set; }

        [Display(Name = "Дата создания")]
        [Column("CREATE_DATE", TypeName = "DATETIME")]
        public DateTime? CreateDate { get; set; }

        [Column("LAST_EDITED_BY", TypeName = "NVARCHAR")]
        [ForeignKey("UserLastEdited")]
        [StringLength(128)]
        public string UserLastEditedId { get; set; }
        public CcRepUser UserLastEdited { get; set; }

        [Column("CREATED_BY", TypeName = "NVARCHAR")]
        [ForeignKey("UserCreated")]
        [StringLength(128)]
        public string UserCreatedId { get; set; }
        public CcRepUser UserCreated { get; set; }
    }
}