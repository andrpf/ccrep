using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_KIND_CONTRACT")]
    public class KindContract
    {
        [StringLength(2)]
        [Key, Column("KIND_CONTRACT", TypeName = "NVARCHAR")]
        public string Kind_Contract { get; set; }

        [StringLength(100)]
        [Column("DESC_KIND", TypeName = "NVARCHAR")]
        public string DescKindContract { get; set; }

     //   public ICollection<Rep406> Rep406s { get; set; }
    }
}