using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_tasks_2.Models;
using MVC_tasks_2.Models.Reposarity;

namespace MVC_tasks_2.Controllers
{
    [Authorize]
    public class departmentCourseController : Controller
    {
        IStudentRepo studentrepo;
        IdeptRepo deptrepo;
        ICourserepo courserepo;
        IStudentCourse studentCourserepo;
        public departmentCourseController(ICourserepo _courserepo , IStudentRepo _studentrepo,IdeptRepo _deptrepo ,IStudentCourse _studentcourserepo)
        {
            studentrepo = _studentrepo;
            deptrepo = _deptrepo;
            courserepo = _courserepo;
            studentCourserepo = _studentcourserepo;

        }
        
            
        
       
       

       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowCourses(int? id)
        {
            var model = deptrepo.getbyidCourses(id.Value);
            return View(model);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult MangeCourses(int? id)
        {
            var model = deptrepo.getbyidCourses(id.Value);
            var allcourses = courserepo.GetAll();
            var CoursesInDept = model.courses.ToList();
            var CoursesOutDept = allcourses.Where(c => !CoursesInDept.Any(d => d.Id == c.Id)).ToList(); 
            ViewBag.CourseOutDept = CoursesOutDept;
            return View(model);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult MangeCourses(int id, List<int> CoursesToRemove, List<int> CoursesToAdd)
        {

            courserepo.MangeCourses(id, CoursesToRemove, CoursesToAdd);
            return RedirectToAction("Index", "Department");

            }

        [Authorize(Roles = "Instructor")]
        public IActionResult addStudentDegree(int deptid, int crsId)
        {
            Department dept = deptrepo.getbyidStudents(deptid);
            Course Course = courserepo.getbyid(crsId);
            ViewBag.Course = Course;
            return View(dept);
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public IActionResult addStudentDegree(int deptid, int crsId, Dictionary<int, int> degree)
        {
            foreach (var item in degree)
            {
                var stdcrs = studentCourserepo.getstudentincourse(item.Key,crsId) ;
                if (stdcrs == null)
                {
                    StudentCourse studentcourse = new StudentCourse() { StudentId = item.Key, CourseId = crsId, Degree = item.Value };
                    studentCourserepo.AddStudentCourse(studentcourse);
                }
                else
                {
                    stdcrs.Degree = item.Value;
                }



            }
            studentCourserepo.Save();
            return RedirectToAction("Index", "StudentDegree");

        }
       
    }

}

