using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_tasks_2.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20,MinimumLength=3)]
        public string Name { get; set; }
        [Range(20,30)]
        public int Age { get; set; }
        public Department Department { get; set; }
        [ForeignKey("Department")]
        public int Deptid { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]{2,3}")]
        public string Email { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public List<StudentCourse> studentcourses { get; set; }
    }
}
