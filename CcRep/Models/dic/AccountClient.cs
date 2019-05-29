using CcRep.Components.UserAuditing;
using CcRep.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_ACCOUNT_CLIENT")]
    public class AccountClient : AuditAbstract
    {
        public AccountClient()
        {
            BegDate = DateTime.Now;
        }
        [Column("ID_KEY")]
        public int Id { get; set; }

        [StringLength(8)]
        [Column("CNUM", TypeName = "NVARCHAR")]
        [Index("IX_CNUM_CBACCOUNT", 1, IsUnique = true)]
        [Required]
        [Remote("IsAccClExists", "api/AccountClients", areaName: "Directories", AdditionalFields = "Id,CBAccount", ErrorMessage = "Номер и счет ЦБ клиента должны быть уникальны")]
        [Display(Name = "Номер клиента")]
        public string CNum { get; set; }

        [StringLength(20)]
        [Column("CBACCOUNT", TypeName = "NVARCHAR")]
        [Required]
        [Index("IX_CNUM_CBACCOUNT", 2, IsUnique = true)]
        [Display(Name = "Счет ЦБ клиента")]
        public string CBAccount { get; set; }

        [StringLength(100)]
        [Column("DESCRIPTION", TypeName = "NVARCHAR")]
        [Display(Name = "Пояснения")]
        public string Description { get; set; }

        [Column("BEG_DATE", TypeName = "DATE")]
        [Required]
        [DefaultDateTimeValue("Now")]
        [DataType(DataType.Text)]
        [Display(Name = "Дата начала действия")]
        public DateTime? BegDate { get; set; }

        [Column("END_DATE", TypeName = "DATE")]
        [DataType(DataType.Text)]
        [Display(Name = "Дата окончания действия")]
        public DateTime? EndDate { get; set; }
    }
}