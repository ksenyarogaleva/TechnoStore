using System.ComponentModel.DataAnnotations;

namespace TechnoStore.Common.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
