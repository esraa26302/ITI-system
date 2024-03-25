using Microsoft.EntityFrameworkCore;
using MVC_tasks_2.Migrations;

namespace MVC_tasks_2.Models.Reposarity
{
    public interface IStudentRepo
    {
        List<Student> GetAll();
        Student getbyid(int id);
        Task addAsync(Student std);
        void Update(Student std);
        void delete(int id);
        void Save();
        void deleteuser(int id);


    }
    public class StudentRepo : IStudentRepo
    {


        ContextDataBase db = new ContextDataBase();
        public List<Student> GetAll()
        {
            return (db.Students.Include(a => a.Department).ToList());

        }

        public Student getbyid(int id)
        {
            return db.Students.Include(a => a.Department).FirstOrDefault(a => a.Id == id);
        }
        public async Task addAsync(Student std)
        {
            db.Students.Add(std);
            await db.SaveChangesAsync();
        }
        public void Update(Student std)
        {

            var existingStudent = db.Students.Local.FirstOrDefault(s => s.Id == std.Id);
            if (existingStudent != null)
            {
                db.Entry(existingStudent).State = EntityState.Detached;
            }


            db.Students.Update(std);
            db.SaveChanges();
        }


        public void delete(int id)
        {



            var model = getbyid(id);

            db.Students.Remove(model);
            db.SaveChanges();
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void deleteuser(int id) {
            var student = getbyid(id);
            var user = db.Users.FirstOrDefault(u => u.Email == student.Email);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
}





}
}
