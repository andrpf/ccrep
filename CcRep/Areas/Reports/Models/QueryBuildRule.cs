using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CcRep.Components.Filtering;
using Newtonsoft.Json;

namespace CcRep.Areas.Reports.Models
{
    public class QueryBuildRule
    {
        public string id { get; set; }

        public string type { get; set; }

        [JsonProperty(PropertyName = "operator")]
        public string _operator { get; set; }

        public string value { get; set; }

        public string field { get; set; }

        public string input { get; set; }
    }
}