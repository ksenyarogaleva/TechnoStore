using System.ComponentModel.DataAnnotations;

namespace TechnoStore.WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Please, enter yout email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password should contain from 4 to 16 symbols")]
        [DataType(DataType.Password)]
        [StringLength(16,MinimumLength =4,ErrorMessage ="Password should contain from 4 to 16 symbols")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please,repeat your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal.")]
        public string ConfirmPassword { get; set; }
    }
}