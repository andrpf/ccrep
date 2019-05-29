using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CcRep.Models._filtering
{
    [Table("FILT_FILTERING_TARGETS")]
    public class FiltTarget
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME", TypeName = "NVARCHAR")]
        public string Name { get; set; }
    }
}