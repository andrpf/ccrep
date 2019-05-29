using System;
using System.ComponentModel;
using CcRep.Components.UserAuditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.vk
{
    [Table("VK_SUPDOC_REP")]
    public class SupdocRep : AuditAbstract
    {
        [Key, Column("ID_OPER")]
        [ForeignKey("AddInfRep")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdOper { get; set; }

        [StringLength(4)]
        [DisplayName("Код ПД")]
        [Column("CODE_SUPDOC", TypeName = "NVARCHAR")]
        public string CodeSupdoc { get; set; }

        [StringLength(255)]
        [DisplayName("Тип документа")]
        [Column("SUPDOC_NAME", TypeName = "NVARCHAR")]
        public string SupdocName { get; set; }
      
        [DisplayName("Prinvest")]
        [Column("PRINVEST", TypeName = "TINYINT")]
        public byte? Prinvest { get; set; }

        [Column("ENTRY_DATE", TypeName = "DATE")]
        [DisplayName("Дата документа")]
        public DateTime? EntryDate { get; set; }

        [StringLength(1)]
        [DisplayName("Тип контракта")]
        [Column("SUPDOC_TYPE", TypeName = "NCHAR")]
        public string SupdocType { get; set; }

        [StringLength(4)]
        [DisplayName("Отделение")]
        [Column("REG_NUM_BRANCH", TypeName = "NVARCHAR")]
        public string RegNumBranch { get; set; }

        [StringLength(250)]
        [DisplayName("Наименование клиента")]
        [Column("CLIENT_NAME", TypeName = "NVARCHAR")]
        public string ClientName { get; set; }

        [StringLength(50)]
        [DisplayName("Источник ПД")]
        [Column("SOURCE_SUPDOC", TypeName = "NVARCHAR")]
        public string SourceSupdoc { get; set; }

        [StringLength(20)]
        [DisplayName("Номер паспорта сделки")]
        [Column("PASSPORT_NUM", TypeName = "NVARCHAR")]
        public string PassportNum { get; set; }

        [DisplayName("Дата ПД")]
        [Column("DOC_DATE")]
        public DateTime? DocDate { get; set; }

        [DisplayName("Дата последней корректировки ПД")]
        [Column("UPDATE_SD_DATE")]
        public DateTime? UpdateSdDate { get; set; }

        public AddInfRep AddInfRep { get; set; }
    }
}