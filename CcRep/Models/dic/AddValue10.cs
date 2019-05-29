using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_ADD_VALUE_10")]
    public class AddValue10
    {
        [Key, Column("ADD_VALUE", TypeName = "TINYINT")]
        public byte AddValue { get; set; }

        [StringLength(255)]
        [Column("DESC_ADD", TypeName = "NVARCHAR")]
        public string DescAdd { get; set; }

      //  public ICollection<Rep406> Rep406s { get; set; }
    }
}