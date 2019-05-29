using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_TYPE_CONTRACT")]
    public class TypeContract
    {
        [StringLength(1)]
        [Key, Column("TYPE_CONTRACT", TypeName = "NCHAR")]
        public string Type_Contract { get; set; }

        [StringLength(200)]
        [Column("DESC_TYPE_CONTRACT", TypeName = "NVARCHAR")]
        public string DescTypeContract { get; set; }

      //  public ICollection<Rep406> Rep406s { get; set; }
    }
}