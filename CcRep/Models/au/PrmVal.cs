using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.au
{
    [Table("AU_PRM_VAL")]
    public class PrmVal
    {
        [Column("ID_PRM")]
        public int Id { get; set; }

        [Column("ID_USER")]
        public int IdUser { get; set; }
        public User User { get; set; }

        [StringLength(10)]
        [Column("PRM_CODE", TypeName = "NVARCHAR")]
        public string PrmCode { get; set; }

        [StringLength(10)]
        [Column("PRM_VAL", TypeName = "NVARCHAR")]
        public string Prm_Val { get; set; }

        [Column("DATE_BEG", TypeName = "DATE")]
        public DateTime DateBeg { get; set; }

        [StringLength(20)]
        [Column("USR_OUT", TypeName = "NVARCHAR")]
        public string UsrOut { get; set; }

        [Column("DT_OUT", TypeName = "DATETIME")]
        public DateTime DtOut { get; set; }
    }
}