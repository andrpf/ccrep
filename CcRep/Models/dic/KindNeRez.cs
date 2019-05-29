using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_KIND_NEREZ")]
    public class KindNeRez
    {
        [StringLength(1)]
        [Key]
        [Column("KIND_NEREZ", TypeName = "NCHAR")]        
        public string Kind_Nerez { get; set; }

        [StringLength(150)]
        [Column("DESC_KIND", TypeName = "NVARCHAR")]
        public string DescKind { get; set; }

      //  public ICollection<Rep406> Rep406s { get; set; }
    }
}