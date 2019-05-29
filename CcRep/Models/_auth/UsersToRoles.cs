using CcRep.Components.UserAuditing;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CcRep.Models
{
    public class UsersToRoles : IdentityUserRole<string>, IDateAuditing, IUserAuditing
    {
        [ForeignKey("UserLastEdited")]
        [Column("LAST_EDITED_BY")]
        public string UserLastEditedId { get; set; }
        public virtual CcRepUser UserLastEdited { get; set; }

        [ForeignKey("UserCreated")]
        [Column("CREATED_BY")]
        public string UserCreatedId { get; set; }
        public virtual CcRepUser UserCreated { get; set; }

        [Display(Name = "Дата создания")]
        [Column("CREATE_DATE", TypeName = "DATETIME")]
        public DateTime? CreateDate { get; set; }
    }
}