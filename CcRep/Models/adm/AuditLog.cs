using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.adm
{
    [Table("ADM_AUDIT_LOG")]
    public class AuditLog
    {       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("SEQ_NO", TypeName = "BIGINT")]
        public long SeqNo { get; set; }

        [StringLength(30)]
        [Required]
        [Column("TABLE_NAME", TypeName = "NVARCHAR")]
        public string TableName { get; set; }

        [Required]
        [Column("TRAN_DATE", TypeName = "DATETIME")]
        public DateTime TranDate { get; set; }

        [Required]       
        [Column("TRAN_MODE")]
        public char TranMode { get; set; }

        [Required]
        [StringLength(30)]
        [Column("USER_ID", TypeName = "NVARCHAR")]
        public string UserId { get; set; }

        [Required]
        [Column("RECORD_KEY")]
        public int RecordKey { get; set; }      

        public List<AuditCol> AuditCols { get; set; }
    }
}