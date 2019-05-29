using CcRep.Components.UserAuditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_ACCT_REPORT")]
    public class AcctReport : AuditAbstract
    {
        [Column("ID_KEY")]
        public int Id { get; set; }

        [Display(Name = "Балансовый счет")]
        [StringLength(5)]
        [Required]
        [Index("IX_Acc2", IsUnique = true)]
        [Column("ACC2", TypeName = "NVARCHAR")]
        [Remote("IsAcc2NotExists", "api/AcctReports", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Это уже существует")]
        public string Acc2 { get; set; }

        [Display(Name = "Резидент")]
        [Column("RESIDENT")]
        [Required]
        public bool Resident { get; set; }

        [Display(Name = "Контроль конрагента")]
        [Column("CNTR_PARTNER")]
        [Required]
        public bool CntrPartner { get; set; }

        public virtual ICollection<ClientType> ClientTypes { get; set; } = new HashSet<ClientType>();
    }
}