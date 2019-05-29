using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CcRep.Models.vk;

namespace CcRep.Models.dic
{
    [Table("DIC_TYPE_CL_REZ")]
    public class TypeClRez
    {
        [StringLength(2)]
        [Key, Column("TYPE_CLIENT", TypeName = "NVARCHAR")]
        public string Type_Client { get; set; }

        [StringLength(100)]
        [Column("DESCRIPTION", TypeName = "NVARCHAR")]
        public string Description { get; set; }       

        public ICollection<ClientRep> ClientReps { get; set; }
    }
}