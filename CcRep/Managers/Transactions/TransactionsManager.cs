using CcRep.Areas.Reports.Models;
using CcRep.Models._dc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Managers.Transactions
{
    public class TransactionsManager
    {
        public CcRepContext context { get; set; }

        public TransactionsManager(CcRepContext db)
        {
            context = db;
        }

        public bool AddTranFromUserInput(TranCreateViewModel tranViewModel)
        {


            return true;
        }
    }
}