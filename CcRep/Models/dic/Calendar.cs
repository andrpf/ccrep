using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_CALENDAR", Schema = "etl")]
    public class Calendar
    {        
        [Key, Column("DAT", TypeName = "DATE")]
        public DateTime Date { get; set; }

        [StringLength(3)]        
        [Column("CCY", TypeName = "NVARCHAR")]
        public string Ccy { get; set; }

        [StringLength(1)]
        [Column("HOLIDAY", TypeName = "CHAR")]
        public string Holiday { get; set; }
    }
}