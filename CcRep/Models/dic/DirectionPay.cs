using CcRep.Models.vk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CcRep.Models.dic
{
    [Table("DIC_DIRECTION_CODES")]
    public class DirectionPay
    {
        [StringLength(5)]
        [Display(Name = "Код операции")]
        [Key, Column("DIRECTION_PAY", TypeName = "NVARCHAR")]      
        public string Direction_Pay { get; set; }

        [StringLength(255)]
        [Display(Name = "Операция")]
        [Column("DESC_DIRECT", TypeName = "NVARCHAR")]
        public string DescDirect { get; set; }

     //   public ICollection<BasicRep> BasicReps { get; set; }

        public ICollection<SettingCodeVO> SettingCodeVOs { get; set; }
    }
}