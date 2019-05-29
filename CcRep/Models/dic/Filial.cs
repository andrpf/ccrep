using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_FILIAL")]
    public class Filial
    {
        [StringLength(4)]
        [Key, Column("NC_FILIAL", TypeName = "NVARCHAR")]
        public string NCFilial { get; set; }

        [StringLength(3)]
        [Required, Column("LC_FILIAL", TypeName = "NVARCHAR")]
        public string LCFilial { get; set; }

        [StringLength(4)]
        [Required, Column("LICENSE", TypeName = "NVARCHAR")]
        public string License { get; set; }

        [StringLength(80)]
        [Required, Column("NAME_FILIAL", TypeName = "NVARCHAR")]
        public string NameFilial { get; set; }

        [InverseProperty("Filial")]
        public List<UsersToFilials> Users { get; set; }
    }
}