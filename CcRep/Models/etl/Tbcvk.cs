using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.etl
{
    [Table("ETL_TBCVK", Schema = "etl")]
    public class Tbcvk
    {
        [StringLength(22)]
        [Key, Column("N_PC", TypeName = "NVARCHAR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string NPC { get; set; }

        [StringLength(4)]
        [Column("LOAD_TYPE", TypeName = "NVARCHAR")]
        public string LoadType { get; set; }

        [StringLength(2)]
        [Column("CODE_SUPDOC", TypeName = "NVARCHAR")]
        public string CodeSupdoc { get; set; }

        [StringLength(1)]
        [Column("PRINVEST", TypeName = "NCHAR")]
        public string Prinvest { get; set; }

        [Column("ENTRY_DATE", TypeName = "DATE")]
        public DateTime? EntryDate { get; set; }

        [StringLength(1)]
        [Column("SUPDOC_TYPE", TypeName = "NCHAR")]
        public string SupdocType { get; set; }

        [StringLength(4)]
        [Column("REG_NUM_BRANCH", TypeName = "NVARCHAR")]
        public string RegNumBranch { get; set; }

        [StringLength(250)]
        [Column("CLIENT_NAME", TypeName = "NVARCHAR")]
        public string ClientName { get; set; }
     
        [Column("Amount_SD")]
        public decimal? AmountCD { get; set; }

        [Column("POSTDATE", TypeName = "DATE")]
        public DateTime? PostDate { get; set; }

        [StringLength(50)]
        [Column("SOURCE_SUPDOC", TypeName = "NVARCHAR")]
        public string SourceSupdoc { get; set; }

        [StringLength(20)]
        [Column("PASSPORT_NUM", TypeName = "NVARCHAR")]
        public string PassportNum { get; set; }
      
        [Column("DOC_DATE")]
        public DateTime? DocDate { get; set; }
      
        [Column("UPDATE_SD_DATE")]
        public DateTime? UpdateSdDate { get; set; }
    }
}