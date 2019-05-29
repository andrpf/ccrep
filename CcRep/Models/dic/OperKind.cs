using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_OPER_KIND")]
    public class OperKind
    {
        [Key, Column("OPER_KIND", TypeName = "TINYINT")]   
        [Range(0,99)]       
        public byte Oper_Kind { get; set; }       

        [StringLength(255)]
        [Column("DESC_OPER_KIND", TypeName = "NVARCHAR")]
        public string DescOperKind { get; set; }

     //   public ICollection<Rep406> Rep406s { get; set; }
    }
}