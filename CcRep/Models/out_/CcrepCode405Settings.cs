using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.out_
{
    [Table("OUT_CCREP_CODE_405_SETTINGS")]
    public class CcrepCode405Settings
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(5)]        
        [Column("CODE_VO", TypeName = "NVARCHAR")]
        public string CodeVO { get; set; }

        [StringLength(20)]
        [Column("CODE_405", TypeName = "NVARCHAR")]
        public string Code405 { get; set; }

        [StringLength(5)]
        [Column("REPLACE_CODE_405", TypeName = "NVARCHAR")]
        public string ReplaceCode405 { get; set; }

        [StringLength(255)]
        [Column("CODE_VO_DESC", TypeName = "NVARCHAR")]
        public string CodeVODesc { get; set; }

        [StringLength(255)]
        [Column("CODE_405_DESC", TypeName = "NVARCHAR")]
        public string Code405Desc { get; set; }

        [StringLength(1)]
        [Column("SECTION", TypeName = "CHAR")]
        public string Section { get; set; }

        [StringLength(255)]
        [Column("OPERATION_CODE", TypeName = "NVARCHAR")]
        public string OperationCode { get; set; }

        [StringLength(1)]
        [Column("DIRECTION_FLG",TypeName = "CHAR")]
        public string DirectionFlg { get; set; }

        [StringLength(1)]
        [Column("NONRESIDENT_FLG", TypeName = "CHAR")]
        public string NonResidentFlg { get; set; }

        [StringLength(1)]
        [Column("RESIDENT_FLG", TypeName = "CHAR")]
        public string ResidentFlg { get; set; }

        [StringLength(1)]
        [Column("PROPERTY_FLG", TypeName = "CHAR")]
        public string PropertyFlg { get; set; }

        [StringLength(1)]
        [Column("INCLUDE_405_FLG", TypeName = "CHAR")]
        public string Include405Flg { get; set; }

        [StringLength(1)]
        [Column("INCLUDE_406_FLG", TypeName = "CHAR")]
        public string Include406Flg { get; set; }
    }
}