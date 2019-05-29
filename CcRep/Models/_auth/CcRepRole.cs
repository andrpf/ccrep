using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models
{
    public class CcRepRole : IdentityRole<string, UsersToRoles>
    {
        public CcRepRole()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        [StringLength(30)]
        [Column("Desc", TypeName = "NVARCHAR")]
        public string Desc { get; set; }
    }
}