using CcRep.Models.etl;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CcRep.Models.adm
{
    [Table("ADM_DWH_LOAD")]
    public class DwhLoad
    {
        [Column("ID_KEY"), Required]
        public int Id { get; set; }

        [Column("DATE_LOAD", TypeName = "DATE"), Required]
        public DateTime DateLoad { get; set; }

        [Column("ID_DWH"), Required]
        public int IdDwh { get; set; }
        public Dwhs Dwhs { get; set; }

        [StringLength(1)]
        [Column("STATE", TypeName = "CHAR"), Required]
        public string State { get; set; }
    }
}