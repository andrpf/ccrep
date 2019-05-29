using CcRep.Models._dc;
using CcRep.Models.vk;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CcRep.Areas.Reports.Models
{
    public class TranCreateViewModel
    {
        public AddInfRep AddInfRep { get; set; }
        public BasicRep BasicRep { get; set; }
        public ClientRep ClientRep { get; set; }
        public IssuerRep IssuerRep { get; set; }
        public Rep406 Rep406 { get; set; }
        public SupdocRep Supdoc { get; set; }
        public NoticeRep NoticeRep { get; set; }

        //Dropdown lists
        public SelectList CodeToolingsList { get; set; }
        public SelectList OperTypesList { get; set; }
        public SelectList DirPayList { get; set; }
        public SelectList TypeClientRez { get; set; }
        public SelectList TypeClientNerez { get; set; }
        public SelectList IssuerCodesList { get; set; }
        public SelectList OperKindList { get; set; }
        public SelectList AddValue10List { get; set; }
        public SelectList KindRezList { get; set; }
        public SelectList KindNerezList { get; set; }
        public SelectList KindContractList { get; set; }
        public SelectList TypeContractList { get; set; }
        public SelectList SectionsList { get; set; }
        public List<SelectListItem> SupdocFlList { get; set; }
        public List<SelectListItem> FormList { get; set; }

        public TranCreateViewModel()
        { }
        public TranCreateViewModel(HeaderRep headerRep)
        {
            AddInfRep = new AddInfRep() { IdRep = headerRep.Id };
            AddInfRep.HeaderRep = headerRep;
            BasicRep = new BasicRep();
            ClientRep = new ClientRep();
            IssuerRep = new IssuerRep();
            Rep406 = new Rep406();
            Supdoc = new SupdocRep();
            NoticeRep = new NoticeRep();
        }

        public void setViewLists(CcRepContext db)
        {
            CodeToolingsList = new SelectList(db.CodeToolings, "TypeTooling", "TypeTooling");
            OperTypesList = new SelectList(db.OperTypes, "Oper_Type", "ComplexName");
            DirPayList = new SelectList(db.DirectionPays, "Direction_Pay", "Direction_Pay");
            AddValue10List = new SelectList(db.AddValues10, "AddValue", "DescAdd");
            OperKindList = new SelectList(db.OperKinds, "Oper_Kind", "Oper_Kind");
            KindRezList = new SelectList(db.KindRezs, "Kind_Rez", "Kind_Rez");
            KindNerezList = new SelectList(db.KindNerezs, "Kind_Nerez", "Kind_Nerez");
            KindContractList = new SelectList(db.KindContracts, "Kind_Contract", "DescKindContract");
            TypeContractList = new SelectList(db.TypeContracts, "Type_Contract", "DescTypeContract");
            SectionsList = new SelectList(db.Sections, "_Section", "_Section");

            SupdocFlList = new List<SelectListItem>() {
                new SelectListItem() {Text = "Да", Value = "Y" },
                new SelectListItem() {Text = "Нет", Value = "N" }
            };
            FormList = new List<SelectListItem>() {
                new SelectListItem() {Text = "405", Value = "405" },
                new SelectListItem() {Text = "406", Value = "406" }
            };

            var typeClientsR = from tCl in db.TypeClients
                               where tCl.StatusRezs.Any(c => c.Status == "REZ")
                               select tCl;

            var typeClientsNR = from tCl in db.TypeClients
                                where tCl.StatusRezs.Any(c => c.Status == "NEREZ")
                                select tCl;

            TypeClientRez = new SelectList(typeClientsR, "Type_Client", "Type_Client");

            TypeClientNerez = new SelectList(typeClientsNR, "Type_Client", "Type_Client");

            IssuerCodesList = new SelectList(db.IssuerCodes, "Issuer_Code", "DescIssuerCode");
        }
    }
}