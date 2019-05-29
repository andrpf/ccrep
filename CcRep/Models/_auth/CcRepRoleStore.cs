using CcRep.Models._dc;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Models
{
    public class CcRepRoleStore : RoleStore<CcRepRole, string, UsersToRoles>
    {
        public CcRepRoleStore(CcRepContext context) 
        : base(context) 
    {
        }
    }
}