using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CcRep.Managers.Transactions
{
    public class GridData
    {
        // VK_ADDINF_REP
        public string Form { get; set; }
        public string Section { get; set; }
        public string Status { get; set; }
        public string SupdocFl { get; set; }

        // VK_BASIC_REP
        public string Filial { get; set; }
        public DateTime? PostDate { get; set; }
        public string TypeTooling { get; set; }
        public byte? OperType { get; set; }
        public string DirectionPay { get; set; }
        public long? Count { get; set; }
        public decimal? Share { get; set; }
        public string Ccy { get; set; }
        public decimal? AmountAll { get; set; }
        public decimal? AmntIncome { get; set; }
        public string RefNum { get; set; }
        public string CodeVO { get; set; }
        public DateTime? ValueDate { get; set; } // только раздел 1
        public string Swift { get; set; }
        public string SwiftFil { get; set; }

        // VK_CLIENT_REP
        public string NameRez { get; set; }
        public string TypeRez { get; set; }
        public string BicPartner { get; set; }
        public string CountryRez { get; set; }
        public string INN { get; set; }
        public string NameNerez { get; set; }
        public string TypeNerez { get; set; }
        public string CountryNerez { get; set; }
        public string BankName { get; set; }
        public string CountryBank { get; set; }
        public string PartnerName { get; set; }
        public string CountryPartner { get; set; }
        public string CustNo { get; set; }

        // VK_ISSUER_REP
        public string IssuerName { get; set; }
        public string SecurityCode { get; set; }
        public string RegNumIssuer { get; set; }
        public DateTime? DateRegIssuer { get; set; }
        public DateTime? RepayDate { get; set; }
        public string CcyIssuer { get; set; }
        public byte? IssuerCode { get; set; }
        public string IssuerReestr { get; set; }

        // VK_406_REP
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

        // VK_SUPDOC_REP
        public string CodeSupdoc { get; set; }
        public string SupdocName { get; set; }
        public byte? Prinvest { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SupdocType { get; set; }
        public string RegNumBranch { get; set; }
        public string ClientName { get; set; }
        public string SourceSupdoc { get; set; }
        public string PassportNum { get; set; }
        public DateTime? DocDate { get; set; }
        public decimal? AmountSD { get; set; }
        public DateTime? GiveSdDate { get; set; }
        public DateTime? UpdateSdDate { get; set; }

        // VK_ADDINF_REP
        public DateTime? DateLock { get; set; }
        public DateTime? DateRemove { get; set; }
        public string NumJD { get; set; }
        public string NumDVK { get; set; }
        public DateTime? DateRequest { get; set; }
        public DateTime? DateGetDoc { get; set; }
        public string CNum { get; set; }
        public string CBAccount { get; set; }

        public long OperationId { get; set; }

        public static string GetFieldName(int num_field)
        {
            Type t = typeof(GridData);
            PropertyInfo[] infos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return (num_field < infos.Length) ? infos[num_field].Name : string.Empty;
        }
    }
}