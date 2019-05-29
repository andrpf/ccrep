using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;

namespace CcRep.Components.UserAuditing
{
    public static class AuditingUtilities
    {
        public static void AddAuditingData(IEnumerable<DbEntityEntry> dbEntityEntries)
        {
            foreach (var entry in dbEntityEntries)
            {
                if (entry.Entity as IDateAuditing != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        (entry.Entity as IDateAuditing).CreateDate = DateTime.Now;
                    }
                }


                if (entry.Entity as IUserAuditing != null)
                {
                    string currentUserId = CurrentUserId;

                    if (entry.State == EntityState.Added)
                    {
                        (entry.Entity as IUserAuditing).UserCreatedId = currentUserId;
                        (entry.Entity as IUserAuditing).UserLastEditedId = currentUserId;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        (entry.Entity as IUserAuditing).UserLastEditedId = currentUserId;
                    }
                }
            }
        }

        private static string CurrentUserId
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.User == null
                    || HttpContext.Current.User.Identity == null)
                    return null;

                return HttpContext.Current.User.Identity.GetUserId();
            }
        }
    }
}