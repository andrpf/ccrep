using CcRep.Components.UserAuditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_OPER_TYPE")]
    public class OperType : AuditAbstract
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [Required]
        [Column("OPER_TYPE", TypeName = "TINYINT")]
        [Range(0, 99, ErrorMessage = "range [0..99] out")]
        [Display(Name = "Код операции")]
        [Index("IX_OperType", IsUnique = true)]
        [Remote("IsTypeNotExists", "api/OperTypes", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Это уже существует")]
        public byte Oper_Type { get; set; }

        [StringLength(255)]
        [Display(Name = "Описание кода")]
        [Column("OPER_TYPE_DESC", TypeName = "NVARCHAR")]
        public string OperTypeDesc { get; set; }

        public string ComplexName {
            get {
                return $"{Oper_Type}: {OperTypeDesc}";
            }
        }
    }
}