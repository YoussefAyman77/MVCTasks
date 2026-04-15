using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Task02MVC.Models;
using Task02MVC.ViewModels;

namespace Task02MVC.Controllers
{
    public class StudentController : Controller
    {
        StudentBL StuBl = new StudentBL();
        DepartmentBL DepartmentBL = new DepartmentBL();
        public IActionResult Index()
        {
            List<Student> StuList = StuBl.GetAll();
            return View("Index", StuList);
        }
        public IActionResult ShowByID(int Id)
        {
            Student stu = StuBl.GetById(Id);
            if (stu == null) return NotFound();
            return View("ShowByID", stu);
        }
        public IActionResult Add()
        {
            var studentAddEditVM = new StudentAddEditVM();
            List<Department> AllDepartments = DepartmentBL.GetAll();
            studentAddEditVM.Departments = new SelectList(AllDepartments,"Id","Name");
            return View("Add", studentAddEditVM);
        }
        [HttpPost]
        public IActionResult SaveAdd(StudentAddEditVM studentVM)
        {
            if (ModelState.IsValid)
            {
                Student AddedSTudent = new Student()
                {
                    Name = studentVM.Name,
                    Age = studentVM.Age,
                    DepartmentId = studentVM.DepartmentId,
                };
                StuBl.AddStudent(AddedSTudent);
                StuBl.Save();
                return RedirectToAction(nameof(Index));
            }
            List<Department> AllDepartments = DepartmentBL.GetAll();
            studentVM.Departments = new SelectList(AllDepartments, "Id", "Name");
            return View("Add", studentVM);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student Studet = StuBl.GetById(id);
            StudentAddEditVM EditedStudetn = new StudentAddEditVM()
            {
                Id = Studet.Id,
                Name = Studet.Name,
                Age = Studet.Age,
                DepartmentId = Studet.DepartmentId,
            };
            List<Department> AllDepartments = DepartmentBL.GetAll();
            EditedStudetn.Departments = new SelectList(AllDepartments,"Id","Name");
            return View("Edit", EditedStudetn);
        }
        [HttpPost]
        public IActionResult SaveEdit(StudentAddEditVM studentVM)
        {
            if (ModelState.IsValid)
            {
                Student EditedSTudent = StuBl.GetById(studentVM.Id);
                EditedSTudent.Name = studentVM.Name;
                EditedSTudent.Age = studentVM.Age;
                EditedSTudent.DepartmentId = studentVM.DepartmentId;
                StuBl.Save();
                return RedirectToAction(nameof(Index));
            }
            List<Department> AllDepartments = DepartmentBL.GetAll();
            studentVM.Departments = new SelectList(AllDepartments,"Id","Name");
            return View("Edit", studentVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student delStu = StuBl.GetById(id);
            return View("Delete", delStu);
        }
        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            Student Deldtu = StuBl.GetById(id);
            if (Deldtu != null)
            {
                StuBl.Delete(Deldtu);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
