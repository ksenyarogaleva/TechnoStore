using System.ComponentModel.DataAnnotations;
namespace TechnoStore.Common.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
    }
}
