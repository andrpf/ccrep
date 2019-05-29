using CcRep.Components.UserAuditing;
using CcRep.Models.vk;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_SETTING_CODE_VO")]
    public class SettingCodeVO : AuditAbstract
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(5)]
        [Required]
        [Index("IX_CodeVO", IsUnique = true)]
        [Column("CODE_VO", TypeName = "NVARCHAR")]
        [Display(Name = "КВО")]
        [Remote("IsCodeVoNotExists", "api/SettingCodeVOes", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Значение поля должно быть уникальным")]
        public string CodeVO { get; set; }

        /*
        [StringLength(20)]
        [Column("CODE_405", TypeName = "NVARCHAR")]
        [Display(Name = "Код для 405")]
        public string Code405 { get; set; }       
        */

        [StringLength(5)]
        [Display(Name = "Заменяющий КВО для 405")]
        [Column("REPLACE405", TypeName = "NVARCHAR")]
        public string Replace405 { get; set; }

        [StringLength(512)]
        [Column("CODE_VO_DESC", TypeName = "NVARCHAR")]
        [Display(Name = "Наименование КВО")]
        public string CodeVODesc { get; set; }

        [StringLength(255)]
        [Display(Name = "Наименование кода для 405")]
        [Column("CODE_405_DESC", TypeName = "NVARCHAR")]
        public string Code405Desc { get; set; }

        [StringLength(5)]
        [Column("OPERATION_CODE", TypeName = "NVARCHAR")]
        [ForeignKey("OperationCode")]
        [Display(Name = "Код операции")]
        public string OperationCodeRef { get; set; }
        public virtual DirectionPay OperationCode { get; set; }

        [Column("SECTION", TypeName = "SMALLINT")]
        [ForeignKey("Section")]
        [Display(Name = "Раздел")]
        public short? SectionRef { get; set; }
        public virtual SettingCodeVOSection Section { get; set; }

        [Column("DIRECTION_PAY", TypeName = "TINYINT")]
        [Range(1, 2, ErrorMessage = "Допустимые значения: 1, 2")]
        [Display(Name = "Направление платежа")]
        public byte? DirectionPay { get; set; }

        [Column("ISSUER_NEREZ")]
        [Display(Name = "Эмитент нерезидент")]
        public Nullable<bool> IssuerNerez { get; set; }

        [Column("ISSUER_REZ")]
        [Display(Name = "Эмитент резидент")]
        public Nullable<bool> IssuerRez { get; set; }

        [Column("PROPERTY_FLG")]
        [Display(Name = "Вклад в имущество")]
        public Nullable<bool> PropertyFlg { get; set; }

        [Column("INCLUDE_405")]
        [Display(Name = "Включать в 405")]
        public Nullable<bool> Include405 { get; set; }

        [Column("INCLUDE_406")]
        [Display(Name = "Включать в 406")]
        public Nullable<bool> Include406 { get; set; }

        public ICollection<BasicRep> BasicReps { get; set; }

        public virtual ICollection<CodeTooling> CodeToolings { get; set; } = new HashSet<CodeTooling>();
    }
}