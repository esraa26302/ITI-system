using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_tasks_2.Models;
using MVC_tasks_2.Models.Reposarity;

namespace MVC_tasks_2.Controllers
{
    [Authorize (Roles= "Admin , Instructor")]
    public class StudentController : Controller
    {
        
        IStudentRepo studentrepo;
        IdeptRepo deptrepo;
        public StudentController(IStudentRepo _studentRepo ,IdeptRepo _deptrepo )
        {
            studentrepo = _studentRepo;
            deptrepo = _deptrepo;
            
        }
       
       
        public IActionResult Index()
        {
            var model = studentrepo.GetAll();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult create()
        {
            ViewBag.deptlist = deptrepo.GetAll();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Student std, IFormFile ImageData)
        {
            if (ImageData != null && ImageData.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageData.CopyToAsync(memoryStream);
                    std.ImageData = memoryStream.ToArray();
                }
                std.ImageMimeType = ImageData.ContentType;
            }


                 studentrepo.addAsync(std);

                return RedirectToAction("Index");
            }
           
        


        public IActionResult details(int id)
        {
            var model = studentrepo.getbyid(id);
            return PartialView(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var model = studentrepo.getbyid(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            ViewBag.deptlist = deptrepo.GetAll();
            return View(model);
           
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            
            var existingStudent = studentrepo.getbyid(std.Id);

                std.ImageData = existingStudent.ImageData;
                std.ImageMimeType = existingStudent.ImageMimeType;
         

           

            studentrepo.Update(std);
        
            

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var student = studentrepo.getbyid(id.Value);
            if (student == null)
            {
                return NotFound();
            }

           
           studentrepo.deleteuser(id.Value);

            
            studentrepo.delete(id.Value);

            return RedirectToAction("Index");
        }


    }
}
