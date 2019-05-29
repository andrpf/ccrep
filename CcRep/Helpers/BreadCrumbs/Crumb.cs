using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Helpers.BreadCrumbs
{
    public class Crumb
    {
        public string Name { get; set; }

        private string link;

        public string Link
        {
            get { return link; }

            set {

                link = value.Trim();
            }
        }
    }
}