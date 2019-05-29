using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.dic
{
    [Table("DIC_SETTING_CODE_VO_SECTIONS")]
    public class SettingCodeVOSection
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Раздел")]
        public short SectionNo { get; set; }

        public string Desc { get; set; }

        public ICollection<SettingCodeVO> SettingCodeVOes { get; set; }
    }
}