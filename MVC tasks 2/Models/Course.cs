namespace MVC_tasks_2.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int Duration { get; set; }
        public List<Department> Departments { get; set; }
        public List<StudentCourse> CoursesStudents { get; set; }
    }
}
