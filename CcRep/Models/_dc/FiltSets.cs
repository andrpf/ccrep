using CcRep.Models._filtering;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CcRep.Models._dc
{
    public partial class CcRepContext
    {
        public DbSet<FiltTarget> FiltTargets { get; set; }
        public DbSet<FiltRule> FiltRules { get; set; }
        public DbSet<FiltRulesToUsers> FiltRulesToUsers { get; set; }
    }
}