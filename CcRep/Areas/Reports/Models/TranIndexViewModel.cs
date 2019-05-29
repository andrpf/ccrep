using CcRep.Models.vk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Areas.Reports.Models
{
    public class TranIndexViewModel
    {
        public AddInfRep DummyAddInfRep { get; }
        public BasicRep DummyBasicRep { get;}
        public ClientRep DummyClientRep { get; }
        public IssuerRep DummyIssuerRep { get; }
        public Rep406 DummyRep406 { get; set; }
        public SupdocRep DummySupdoc { get; set; }

        public TranIndexViewModel()
        {
            DummyAddInfRep = new AddInfRep();
            DummyBasicRep = new BasicRep();
            DummyClientRep = new ClientRep();
            DummyIssuerRep = new IssuerRep();
            DummyRep406 = new Rep406();
            DummySupdoc = new SupdocRep();
        }
        public HeaderRep Report { get; set; }

        public List<AddInfRep> AddInfReps { get; set; }
    }
}