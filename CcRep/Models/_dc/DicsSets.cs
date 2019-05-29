using CcRep.Models.dic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CcRep.Models._dc
{
    public partial class CcRepContext
    {
        // DIC
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<SettingCodeVOSection> SettingCodeVOSections { get; set; }
        public DbSet<AcctReport> AcctReports { get; set; }
        public DbSet<SettingCodeVO> SettingCodeVOs { get; set; }
        public DbSet<KeywordsException> KeywordsExceptions { get; set; }
        public DbSet<CodeTooling> CodeToolings { get; set; }
        public DbSet<OperType> OperTypes { get; set; }
        public DbSet<DirectionPay> DirectionPays { get; set; }
        public DbSet<TypeClient> TypeClients { get; set; }
        public DbSet<StatusRez> StatusRezs { get; set; }
        public DbSet<IssuerCode> IssuerCodes { get; set; }
        public DbSet<OperKind> OperKinds { get; set; }
        public DbSet<AddValue10> AddValues10 { get; set; }
        public DbSet<KindRez> KindRezs { get; set; }
        public DbSet<KindNeRez> KindNerezs { get; set; }
        public DbSet<KindContract> KindContracts { get; set; }
        public DbSet<TypeContract> TypeContracts { get; set; }
        public DbSet<CodeVORep> CodeVOReps { get; set; }
        public DbSet<PDType> PDTypes { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<AccountClient> AccountClients { get; set; }
        public DbSet<Filial> Filials { get; set; }
        public DbSet<StatusOper> StatusOpers { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Section> Sections { get; set; }

        // public DbSet<TypeClRez> TypeClRezs { get; set; }
        // public DbSet<TypeClNerez> TypeClNerezs { get; set; }
    }
}