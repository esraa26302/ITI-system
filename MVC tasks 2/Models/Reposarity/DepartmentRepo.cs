using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC_tasks_2.Models.Reposarity
{
    public interface IdeptRepo
    {
        public List<Department> GetAll();
        public Department GetByID(int id);
        public void Add(Department department);
        public void update(Department department);
        public void delete(int id);
        public Department getbyidCourses(int id);
        public Department getbyidStudents(int id);
    }
    public class DepartmentRepo : IdeptRepo
    {
        ContextDataBase db /*= new ContextDataBase()*/;
        public DepartmentRepo(ContextDataBase _db)
        {
            db= _db;
        }
        public List<Department>  GetAll()
        {
            return (db.Departments.ToList());

        }
        public Department GetByID (int id)
        {
            return db.Departments.FirstOrDefault(a => a.DeptId == id);
        }
        public void Add (Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }
        public void update (Department department)
        {
            db.Departments.Update(department);
            db.SaveChanges();
        }
        public void delete (int id)
        {
            var dept= db.Departments.FirstOrDefault(a => a.DeptId == id);
           db.Departments.Remove(dept);
            db.SaveChanges ();
        }
        public  Department getbyidCourses(int id)
        {
            return db.Departments.Include(a => a.courses).FirstOrDefault(a => a.DeptId == id);

        }

        public Department getbyidStudents(int id)
        {
            return db.Departments.Include(a => a.Students).FirstOrDefault(a => a.DeptId == id);
                }
        
    }
}
