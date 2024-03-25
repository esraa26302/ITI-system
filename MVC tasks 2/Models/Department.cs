using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_tasks_2.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeptId { get; set; }
        [Required]
        public string DeptName { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> courses { get; set; }
        public bool states { get; set; } = true;

    }
}
