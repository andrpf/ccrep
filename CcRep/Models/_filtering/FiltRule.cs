using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CcRep.Models._filtering
{
    [Table("FILT_RULES")]
    public class FiltRule
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("RULES_JSON", TypeName = "NVARCHAR")]
        public string RulesJson { get; set; }

        public List<FiltRulesToUsers> UsersToFiltRules { get; set; }
    }
}