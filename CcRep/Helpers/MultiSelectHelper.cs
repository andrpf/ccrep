using CcRep.Models;
using CcRep.Models.dic;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CcRep.Helpers
{
    public static class MultiSelectHelper
    {
        public static MultiSelectList GetUserFilialsList(CcRepUser user, List<Filial> Filials)
        {
            string[] defaultSelected = user.Filials != null ? user.Filials.Select(item => item.Filial.NCFilial.ToString()).ToArray() : Array.Empty<string>();

            List<SelectListItem> allItems = new List<SelectListItem>();

            Filials.ForEach(Ct =>
            {
                allItems.Add(new SelectListItem()
                {
                    Text = $"{Ct.NCFilial}: {Ct.NameFilial}",
                    Value = Ct.NCFilial,
                });
            });

            return new MultiSelectList(allItems, "Value", "Text", defaultSelected);
        }

        public static MultiSelectList GetUserRolesList(CcRepUser user, List<CcRepRole> roles)
        {
            var defaultSelected = user.Roles.Select(item => item.RoleId.ToString()).ToArray();

            List<SelectListItem> allItems = new List<SelectListItem>();

            roles.ForEach(Ct =>
            {
                allItems.Add(new SelectListItem()
                {
                    Text = Ct.Desc,
                    Value = Ct.Id.ToString(),
                });
            });

            return new MultiSelectList(allItems, "Value", "Text", defaultSelected);
        }
    }
}