using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using CcRep.Components.Correction;

namespace CcRep.Managers.Export
{
    public class KlikoInfo
    {
        public string Form { get; set; }
        public string Section { get; set; }

        // FORM 405 разделы 1 и 2

        // VK_BASIC_REP
        public string Filial { get; set; }
        public DateTime? PostDate { get; set; }
        public string CodeVO { get; set; }
        public string TypeTooling { get; set; }
        public byte? OperType { get; set; }
        public string DirectionPay { get; set; }
        public long? Count { get; set; }
        public decimal? Share { get; set; }
        public string Ccy { get; set; }
        public decimal? AmountAll { get; set; }
        public decimal? AmntIncome { get; set; }
        public DateTime? ValueDate { get; set; } // только раздел 1
        public string Swift { get; set; }
        public string SwiftFil { get; set; }

        // VK_CLIENT_REP
        public string BicPartner { get; set; }
        public string CountryRez { get; set; }
        public string NameRez { get; set; }
        public string TypeRez { get; set; }
        public string INN { get; set; }
        public string NameNerez { get; set; }
        public string TypeNerez { get; set; }
        public string CountryNerez { get; set; }
        public string BankName { get; set; }
        public string CountryBank { get; set; }
        public string PartnerName { get; set; }
        public string CountryPartner { get; set; }

        // VK_ISSUER_REP
        public string IssuerName { get; set; }
        public string SecurityCode { get; set; }
        public string RegNumIssuer { get; set; }
        public DateTime? DateRegIssuer { get; set; }
        public DateTime? RepayDate { get; set; }
        public string CcyIssuer { get; set; }
        public byte? IssuerCode { get; set; }
        public string IssuerReestr { get; set; }

        // VK_NOTICE_REP
        public string Notice { get; set; }
        public string NoticeIssuer { get; set; }
        public string NoticeExchange { get; set; }
        public string NoticeInst { get; set; }
        public string NoticeProperty { get; set; }
        public string NoticeBank { get; set; }

        // Form 406

        // Атрибуты VK_406_REP
        public byte? OperKind { get; set; }
        public string Notice40 { get; set; }
        public decimal? AmountTo { get; set; }
        public decimal? AmountFrom { get; set; }
        public string BicBank { get; set; }
        public string CountryBank406 { get; set; }
        public string AddCode10 { get; set; }
        public string KindRez { get; set; }
        public string KindNerez { get; set; }
        public string KindContract { get; set; }
        public string TypeContract { get; set; }
        public string Notice15 { get; set; }
        public string Notice16 { get; set; }
        public string NoticeOther { get; set; }
    }
}