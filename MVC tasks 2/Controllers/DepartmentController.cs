using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_tasks_2.Models;
using MVC_tasks_2.Models.Reposarity;

namespace MVC_tasks_2.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        IdeptRepo DepartmentRepo;
        public DepartmentController( IdeptRepo _DepartmentRepo)
        {
            DepartmentRepo = _DepartmentRepo;
            
        }


      
        public IActionResult Index()
        {

            var mod = DepartmentRepo.GetAll();

            return View(mod);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Department mod)
        {

            if (mod.DeptId != null && mod.DeptName?.Length > 2)
            {

               
                   DepartmentRepo.Add(mod);
                    return RedirectToAction("Index");
                
                }
                else
                {
                    return View();
                }
            
           

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {


            DepartmentRepo.delete(id.Value);
            return RedirectToAction("Index");
        }

    }
}
