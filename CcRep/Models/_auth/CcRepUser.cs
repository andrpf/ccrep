using CcRep.Models._filtering;
using CcRep.Models.dic;
using CcRep.ValidationAttributes;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models
{
    public class CcRepUser : IdentityUser<string, CcRepUserLogin, UsersToRoles,CcRepUserClaim>
    {
        public CcRepUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Display(Name = "Логин")]
        public override string UserName { get; set; }

        public override string PasswordHash { get; set; }
        public object Name { get; internal set; }

        [Display(Name = "Полное имя")]
        public string FullName { get; set; }

        [Display(Name = "Блокировка")]
        public bool Locked
        {
            get { return locked; }
            set {
                locked = value;
                if (value)
                {
                    EndDt = DateTime.Now;
                }
                else
                {
                    EndDt = null;
                }
            }
        }
        private bool locked;

        [DefaultDateTimeValue("Now")]
        [Display(Name = "Дата добавления")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "DATE")]
        [Display(Name = "Дата закрытия")]
        public DateTime? EndDt { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UsersToFilials> Filials { get; set; }

        public virtual ICollection<FiltRulesToUsers> FiltRulesToUser { get; set; }
    }
}