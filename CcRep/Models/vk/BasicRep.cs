using CcRep.Models.dic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel;

namespace CcRep.Models.vk
{
    [Table("VK_BASIC_REP")]
    public class BasicRep
    {
        [ForeignKey("AddInfRep")]
        [Key, Column("ID_OPER")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdOper { get; set; }
        public AddInfRep AddInfRep { get; set; }

        [StringLength(4)]
        [Column("FILIAL", TypeName = "NVARCHAR")]
        [DisplayName("Филиал")]
        public string Filial { get; set; }
     //   public Filial NCFilial { get; set; }

        [Column("POSTDATE", TypeName = "DATE")]
        [DisplayName("Дата операции")]
        public DateTime? PostDate { get; set; }

        [StringLength(20)]
        [DisplayName("Тип Ц/Б")]
        [Column("TYPE_TOOLING", TypeName = "NVARCHAR")]
        public string TypeTooling { get; set; }

        [Column("OPER_TYPE", TypeName = "TINYINT")]
        [DisplayName("Вид операции")]
        public byte? OperType { get; set; }
     //   public OperType Oper_Type { get; set; }

        [StringLength(10)]
        [Column("DIRECTION_PAY", TypeName = "NVARCHAR")]
        [DisplayName("Направление платежа")]
        public string DirectionPay { get; set; }
      //  public DirectionPay Direction_Pay { get; set; }
        
        [Column("COUNT", TypeName = "BIGINT")]
        [DisplayName("Количество")]
        public long? Count { get; set; }

        [Column("SHARE", TypeName = "DECIMAL")]
        [DisplayName("Размер доли")]
        public decimal? Share { get; set; }

        [StringLength(3)]
        [Column("CCY", TypeName = "NVARCHAR")]
        [DisplayName("Валюта")]
        public string Ccy { get; set; }

        [Column("AMOUNT_ALL", TypeName = "DECIMAL")]
        [DisplayName("Сумма платежа")]
        public decimal? AmountAll { get; set; }

        [Column("AMNT_INCOME", TypeName = "DECIMAL")]
        [DisplayName("Сумма %")]
        public decimal? AmntIncome { get; set; }

        [StringLength(50)]
        [DisplayName("Референс")]
        [Column("REF_NUM", TypeName = "NVARCHAR")]
        public string RefNum { get; set; }

        [StringLength(5)]
        [DisplayName("КВО")]
        [Column("CODE_VO", TypeName = "NVARCHAR")]
        public string CodeVO { get; set; }

     //   [Column("CODE_VO_ID")]
     //   public int CodeVOId { get; set; }
     //   public SettingCodeVO SettingCodeVO { get; set; }

        [Column("VALUEDATE", TypeName = "DATE")]
        [DisplayName("Дата по сделке")]
        public DateTime? ValueDate { get; set; }

        [StringLength(11)]
        [DisplayName("Код SWIFT")]
        [Column("SWIFT", TypeName = "NVARCHAR")]
        public string Swift { get; set; }

        [StringLength(3)]
        [DisplayName("SWIFT-код филиала")]
        [Column("SWIFT_FIL", TypeName = "NVARCHAR")]
        public string SwiftFil { get; set; }
    }
}