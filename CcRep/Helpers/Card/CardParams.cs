using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Helpers.Card
{
    public class CardParams
    {
        public bool Reload { get; set; } = false;

        public bool Close { get; set; } = false;

        public bool Hide { get; set; } = true;

        public bool FullScreen { get; set; } = true;
    }
}