using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_tasks_2.Models;
using MVC_tasks_2.Models.Reposarity;

namespace MVC_tasks_2.Controllers
{
    [Authorize(Roles = "Admin , Instructor")]
    public class StudentDegreeController : Controller
    {
        ContextDataBase db = new ContextDataBase();
        public IActionResult Index()
        {

            var model = db.StudentsCourses.Include(a=>a.Course).Include(a=>a.Student).ToList();
            return View(model);
        }
        

    }
}
