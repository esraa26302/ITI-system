namespace MVC_tasks_2.Models.Reposarity
{
    public interface IStudentCourse
    {
        StudentCourse getstudentincourse(int key, int crsId);
        void AddStudentCourse(StudentCourse studentCourse);
        void Save();
    }
    public class StudentCourseRepo :IStudentCourse
    {
        ContextDataBase db = new ContextDataBase();

        public StudentCourse getstudentincourse(int key, int crsId)
        {

            return db.StudentsCourses.FirstOrDefault(a => a.StudentId == key && a.CourseId == crsId);
        }
        public void AddStudentCourse(StudentCourse studentCourse)
        {
            db.StudentsCourses.Add(studentCourse);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
