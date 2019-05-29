using CcRep.Models.dic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.adm
{
    [Table("ADM_CHECK_ERROR")]
    public class CheckError
    {
        [Column("ID_HIST")]
        public int Id { get; set; }

        [Column("ID_CHECK", TypeName = "TINYINT")]
        [Required]
        public byte IdCheck { get; set; }

        [StringLength(4)]        
        [Column("FILIAL", TypeName = "NVARCHAR")]
        public string Filial { get; set; }
        public Filial NCFilial;

        [StringLength(2)]       
        [Column("TYPE_REZ", TypeName = "NVARCHAR")]
        public string TypeRez { get; set; }

        [StringLength(32)]       
        [Column("REF_NUM", TypeName = "NVARCHAR")]
        public string Name { get; set; }

        [Column("POSTDATE", TypeName = "DATE")]
        public DateTime DtmStart { get; set; }

        [Column("AMOUNT_ALL", TypeName = "DECIMAL")]
        public decimal EndStart { get; set; }

        [Column("SUPDOC_FL", TypeName = "NVARCHAR")]
        public char SupdocFl { get; set; }
    }
}