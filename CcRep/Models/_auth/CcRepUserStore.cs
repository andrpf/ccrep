using CcRep.Models._dc;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Models
{
    public class CcRepUserStore : UserStore<CcRepUser, CcRepRole, string, CcRepUserLogin, UsersToRoles, CcRepUserClaim>
    {
        public CcRepUserStore(CcRepContext context) : base(context) 
    {
        }
    }
}