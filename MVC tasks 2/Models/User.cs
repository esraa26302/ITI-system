using System.ComponentModel.DataAnnotations;

namespace MVC_tasks_2.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string secondName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[@#]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter and one special character (@, #).")]
        public string Password { get; set; }
        [Range(20, 30, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }


        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

    }
}
