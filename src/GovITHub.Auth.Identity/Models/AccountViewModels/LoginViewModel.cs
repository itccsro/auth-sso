using System.ComponentModel.DataAnnotations;

namespace GovITHub.Auth.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Persistent?")]
        public bool RememberMe { get; set; }
    }
}
