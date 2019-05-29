using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_STATUS_REZ")]
    public class StatusRez
    {
        [StringLength(5)]
        [Key, Column("STATUS", TypeName = "NVARCHAR")]
        public string Status { get; set; }

        [StringLength(35)]
        [Column("NAME_STATUS", TypeName = "NVARCHAR")]
        public string NameStatus { get; set; }

        public virtual ICollection<TypeClient> TypeClients { get; set; } = new HashSet<TypeClient>();
    }
}