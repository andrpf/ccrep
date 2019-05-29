using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CcRep.Managers.Transactions;

namespace CcRep.Areas.Reports.Models
{
    public class FindTranDataModel
    {
        
        public QueryBuildRule[] Rules { get; set; }

        public int ReportId { get; set; }

        public int StartRowNo { get; set; }

        public int PageLength { get; set; }

        public bool onlyDuple { get; set; }

        public FindTranOrder[] OrderRules { get; set; }
    }

    public enum Dir
    {
        asc,
        desc
    }

    public class FindTranOrder
    {
        public short column { get; set; }

        public Dir dir { get; set; }
    }
}