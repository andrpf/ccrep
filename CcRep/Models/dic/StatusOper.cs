using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_STATUS_OPER")]
    public class StatusOper
    {
        [StringLength(10)]
        [Key, Column("STATUS", TypeName = "NVARCHAR")]
        public string Status { get; set; }

        [StringLength(35)]
        [Column("NAME_STATUS", TypeName = "NVARCHAR")]
        public string NameStatus { get; set; }

        [StringLength(1)]
        [Column("SUPDOC_FL", TypeName = "CHAR")]
        public string SUPDOC_FL { get; set; }
    }
}