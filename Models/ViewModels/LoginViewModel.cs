using System.ComponentModel.DataAnnotations;

namespace DogGo.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a valid email")]

        public string Email { get; set; }
    }
}
