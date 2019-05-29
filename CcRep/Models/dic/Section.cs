using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_SECTION")]
    public class Section
    {
        [StringLength(3)]
        [Key, Column("SECTION", TypeName = "CHAR")]
        public string _Section { get; set; }
    }
}