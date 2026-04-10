using Microsoft.AspNetCore.Mvc;
using Task02MVC.Models;

namespace Task02MVC.Controllers
{
    public class StudentController : Controller
    {
        StudentBL StuBl = new StudentBL();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowAll()
        {
            List<Student> StuList = StuBl.GetAll();
            return View("ShowAll", StuList);
        }

        public IActionResult ShowByID(int Id)
        {
            Student stu = StuBl.GetById(Id);
            if (stu == null) return NotFound();
            return View("ShowByID", stu);
        }
    }
}
