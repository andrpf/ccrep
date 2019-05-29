using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.out_
{
    [Table("OUT_LOG", Schema = "out")]
    public class Log
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [Required]
        [Column("DATE_LOG", TypeName = "DATE")]
        public DateTime DateLog { get; set; }

        [StringLength(1)]
        [Column("STATUS", TypeName = "NVARCHAR")]
        public string Status { get; set; }

        [Column("START_DATE", TypeName = "DATETIME")]
        public DateTime StartDate { get; set; }

        [Column("END_DATE", TypeName = "DATETIME")]
        public DateTime EndDate { get; set; }
    }
}