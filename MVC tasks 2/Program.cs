using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVC_tasks_2.Models;
using MVC_tasks_2.Models.Reposarity;

namespace MVC_tasks_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IStudentRepo,StudentRepo>();
            builder.Services.AddTransient<IdeptRepo, DepartmentRepo>();
            builder.Services.AddTransient<ICourserepo, Courserepo>();
            builder.Services.AddTransient<IStudentCourse,StudentCourseRepo>();
            builder.Services.AddDbContext<ContextDataBase>(a =>
            {
                a.UseSqlServer("Data Source= . ;Initial Catalog=EsraaDataMVC; Integrated Security=True;TrustServerCertificate=true;Encrypt=True");
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
