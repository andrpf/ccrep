using CcRep.Components.UserAuditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CcRep.Models.dic
{
    [Table("DIC_CLIENT_TYPES")]
    public class ClientType : AuditAbstract
    {
        public short Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(5)]
        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Тип клиента")]
        [Remote("IsTypeExists", "api/ClientTypes", areaName: "Directories", AdditionalFields = "Id", ErrorMessage = "Это уже существует")]
        public string NameShort { get; set; }

        [Display(Name = "Информация о типе клиента")]
        public string Description { get; set; }

        public virtual ICollection<AcctReport> AcctReports { get; set; } = new HashSet<AcctReport>();

        public override string ToString()
        {
            return this.NameShort;
        }
    }
}