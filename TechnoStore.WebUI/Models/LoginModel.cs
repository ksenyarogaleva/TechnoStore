using System.ComponentModel.DataAnnotations;

namespace TechnoStore.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Please, enter your username")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please,enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}