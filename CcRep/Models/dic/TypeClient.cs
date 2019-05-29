using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_TYPE_CLIENT")]
    public class TypeClient
    {
        [StringLength(2)]
        [Key, Column("TYPE_CLIENT", TypeName = "NVARCHAR")]
        public string Type_Client { get; set; }

        [StringLength(100)]
        [Column("DESCRIPTION", TypeName = "NVARCHAR")]
        public string Description { get; set; }

        public virtual ICollection<StatusRez> StatusRezs { get; set; } = new HashSet<StatusRez>();
    }
}