using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.out_
{
    [Table("OUT_CCREP_CUSTOMER_ACCOUNTS")]
    public class CcrepCustomerAccounts
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(5)]
        [Column("ACC_CODE", TypeName = "NVARCHAR")]
        public string AccCode { get; set; }

        [StringLength(255)]
        [Column("CLIENT", TypeName = "NVARCHAR")]
        public string Client { get; set; }

        [StringLength(1)]
        [Column("RESIDENT_FLG", TypeName = "CHAR")]
        public string ResidentFlg { get; set; }

        [StringLength(1)]
        [Column("CONTROL_FLG", TypeName = "CHAR")]
        public string ControlFlg { get; set; }
    }
}