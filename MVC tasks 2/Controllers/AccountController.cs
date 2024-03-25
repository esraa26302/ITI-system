using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_tasks_2.Models;
using MVC_tasks_2.Models.ViewModels;
using System.Security.Claims;

namespace MVC_tasks_2.Controllers
{
    public class AccountController : Controller
    {
        ContextDataBase db = new ContextDataBase();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Register(Registerviewmodel model)
        {
            if (ModelState.IsValid)
            {
               
                var existingStudent = db.Students.FirstOrDefault(s => s.Email == model.Email && s.ImageData == null);
                if (existingStudent != null)
                {
                   
                    byte[] imageData = null;
                    if (model.ImageFile != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ImageFile.CopyToAsync(memoryStream);
                            imageData = memoryStream.ToArray();
                        }
                    }

                    
                    existingStudent.ImageData = imageData;
                    existingStudent.ImageMimeType = model.ImageFile != null ? model.ImageFile.ContentType : null;

                    
                    db.Students.Update(existingStudent);
                    db.SaveChanges();

                    User user = new User()
                    {
                        FirstName = model.FirstName,
                        secondName = model.secondName,
                        Age = model.Age,
                        Email = model.Email,
                        Password = model.Password
                    };
                    db.Users.Add(user);
                    db.SaveChanges();

                  
                    var role = db.Roles.FirstOrDefault(a => a.Name == "Student");
                    if (role != null)
                    {
                        user.Roles.Add(role);
                        db.SaveChanges();
                    }
                    return RedirectToAction("login");
                }
                else
                {
                    ModelState.AddModelError("", "this Email is not allowed to Register in Our System");
                    return View(model);



                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel model)
        {
            var result = db.Users.Include(a => a.Roles).FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
            if (result == null)
            {
                ModelState.AddModelError("", "The Email or Password is not valid");
                return View(model);
            }
            else
            {
                

                Claim c1 = new Claim(ClaimTypes.Name, result.FirstName);
                Claim c2 = new Claim(ClaimTypes.Email, result.Email);

                List<Claim> claims = new List<Claim>();
                foreach (var item in result.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Name));
                }
                ClaimsIdentity ci = new ClaimsIdentity("Cookies");
                ci.AddClaim(c1);
                ci.AddClaim(c2);

                foreach (var item in claims)
                {
                    ci.AddClaim(item);
                }
                ClaimsPrincipal cp = new ClaimsPrincipal();
                cp.AddIdentity(ci);
                await HttpContext.SignInAsync(cp);
                return RedirectToAction("Profile", "Account");
            }
        }

        public async Task< IActionResult> logOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public JsonResult IsEmailUnique(string email)
        {
            bool isUnique = !db.Users.Any(u => u.Email == email);
            return Json(isUnique);
        }

        public IActionResult GetStudentCourses()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
            {
                var studentEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (studentEmail != null)
                {
                    var student = db.Students.Include(s => s.studentcourses).ThenInclude(cs => cs.Course)
                                              .FirstOrDefault(s => s.Email == studentEmail);
                    if (student != null)
                    {
                        return View(student.studentcourses);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
            {
                var studentEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (studentEmail != null)
                {
                    var student = db.Students.Include(s => s.Department).ThenInclude(cs => cs.courses)
                                              .FirstOrDefault(s => s.Email == studentEmail);
                    if (student != null)
                    {
                        return View(student);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateImage(IFormFile imageData)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
            {
                var studentEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (studentEmail != null)
                {
                    var student = await db.Students.FirstOrDefaultAsync(s => s.Email == studentEmail);
                    if (student != null && imageData != null)
                    {
                        student.ImageMimeType = imageData.ContentType;
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageData.CopyToAsync(memoryStream);
                            student.ImageData = memoryStream.ToArray();
                        }
                        await db.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Profile");
        }








    }





}


