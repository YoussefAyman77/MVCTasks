using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Task02MVC.Models;
using Task02MVC.Reposatories;
using Task02MVC.ViewModels;

namespace Task02MVC.Controllers
{
    public class StudentController : Controller
    {
        IStudentReposatory StuBl;
        IDepartmentReposatory DepartmentBL;
        public StudentController(IStudentReposatory _stbl, IDepartmentReposatory departmentBl)
        {
            StuBl = _stbl;
            DepartmentBL = departmentBl;
        }
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
        [ValidateAntiForgeryToken]
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
                TempData["Success"] = $"Student {studentVM.Name} with Department {DepartmentBL.GetById(studentVM.DepartmentId).Name} Added";
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
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(StudentAddEditVM studentVM)
        {
            if (ModelState.IsValid)
            {
                Student EditedSTudent = StuBl.GetById(studentVM.Id);
                EditedSTudent.Name = studentVM.Name;
                EditedSTudent.Age = studentVM.Age;
                EditedSTudent.DepartmentId = studentVM.DepartmentId;
                StuBl.Save();
                TempData["Success"] = $"Student {studentVM.Name} with Department {DepartmentBL.GetById(studentVM.DepartmentId).Name} Edited";
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
        [ValidateAntiForgeryToken]
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
