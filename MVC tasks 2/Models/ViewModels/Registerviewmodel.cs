using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVC_tasks_2.Models.ViewModels
{
    public class Registerviewmodel
    {
       
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string secondName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Remote("IsEmailUnique", "Account", HttpMethod = "POST", ErrorMessage = "Email already exists")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[@#]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter and one special character (@, #).")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Range(20, 30, ErrorMessage = "Age must be between 20 and 30")]
        public int Age { get; set; }
        [Display(Name = "Profile Picture")]
        public IFormFile ImageFile { get; set; }

    }
}
