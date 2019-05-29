using CcRep.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Components.UserAuditing
{
    public abstract class AuditAbstract : IDateAuditing, IUserAuditing
    {
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