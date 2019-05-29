using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_KIND_REZ")]

    public class KindRez
    {
        [StringLength(10)]
        [Key, Column("KIND_REZ", TypeName = "NVARCHAR")]
        public string Kind_Rez { get; set; }

        [StringLength(255)]
        [Column("DESC_KIND", TypeName = "NVARCHAR")]
        public string DescKind { get; set; }

      //  public ICollection<Rep406> Rep406s { get; set; }
    }
}