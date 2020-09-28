using System.ComponentModel.DataAnnotations;

namespace NewsFeed.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "User")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
