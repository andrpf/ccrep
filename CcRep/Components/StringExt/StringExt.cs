using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Components.StringExt
{
    public static class StringExt
    {
        public static System.String CutEnd(this System.String s, System.String what)
        {
            return s.TrimEnd(what.ToCharArray());
        }
    }
}