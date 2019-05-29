using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Helpers.BreadCrumbs
{
    public class Crumbs : List<Crumb>
    {
        public static Crumbs getCrumbsNoLinks(params string[] list)
        {
            Crumbs crumbs = new Crumbs();

            for (int i = 0; i < list.Length; i++)
            {
                crumbs.Add(new Crumb() { Name = list[i]});
            }

            return crumbs;
        }
    }
}