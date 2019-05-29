using System.ComponentModel.DataAnnotations;

namespace CcRep.Areas.System.ViewModels
{
    public class AddUser
    {
        [Required]
        [Display(Name = "Логин пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Заблокировать")]
        public bool Locked { get; set; }
    }
}