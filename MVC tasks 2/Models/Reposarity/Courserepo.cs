using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC_tasks_2.Models.Reposarity
{
    public interface ICourserepo
    {
        public List<Course> GetAll();
        public Course getbyid(int id);
        void Save();
        public void MangeCourses(int id, List<int> CoursesToRemove, List<int> CoursesToAdd);

    }

    public class Courserepo : ICourserepo
    {
        ContextDataBase db = new ContextDataBase();
        public List<Course> GetAll()
        {
            return db.Courses.ToList();

        }
        public Course getbyid(int id)
        {
            return db.Courses.FirstOrDefault(a => a.Id == id);
        }
        public void Save (){
           db.SaveChanges();
               }
        public void MangeCourses(int id, List<int> CoursesToRemove, List<int> CoursesToAdd)
        {
            Department dept = db.Departments.Include(a => a.courses).FirstOrDefault(a => a.DeptId == id);
            foreach (var item in CoursesToRemove)
            {
                Course c = db.Courses.FirstOrDefault(a => a.Id == item);
                dept.courses.Remove(c);
            }
            foreach (var item in CoursesToAdd)
            {
                Course c = db.Courses.FirstOrDefault(a => a.Id == item);
                dept.courses.Add(c);
            }
            db.SaveChanges();
         

        }
    }
}
