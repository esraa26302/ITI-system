using Microsoft.EntityFrameworkCore;

namespace MVC_tasks_2.Models
{
    public class ContextDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= . ;Initial Catalog=EsraaDataMVC; Integrated Security=True;TrustServerCertificate=true;Encrypt=True;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ContextDataBase()
        {
            
        }
        public ContextDataBase(DbContextOptions options): base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(c => new {c.StudentId,c.CourseId});
            modelBuilder.Entity<Student>().Property(s => s.ImageData).HasColumnType("varbinary(max)");
            base.OnModelCreating(modelBuilder);
        }

    }
}
