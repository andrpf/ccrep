using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_ISSUER_CODE")]
    public class IssuerCode
    {       
        [Key, Column("ISSUER_CODE", TypeName = "TINYINT")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(0,9,ErrorMessage = "range [0..9] out")]
        public byte Issuer_Code { get; set; }

        [StringLength(100)]        
        [Column("DESC_ISSUER_CODE", TypeName = "NVARCHAR")]
        public string DescIssuerCode { get; set; }
    }
}