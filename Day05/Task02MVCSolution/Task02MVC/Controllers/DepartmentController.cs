using Microsoft.AspNetCore.Mvc;
using Task02MVC.Models;
using Task02MVC.ViewModels;

namespace Task02MVC.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentBL DeptBl = new DepartmentBL();
        [HttpGet]
        public IActionResult ShowAll()
        {
            List<Department> deptList = DeptBl.GetAll();
            return View("ShowAll", deptList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult ActualAdd(Department dept)
        {
            if (dept.Name != null && dept.MgrName != null)
            {
                DeptBl.AddDept(dept);
                return RedirectToAction(nameof(ShowAll));
            }
            return View("Add", dept);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Department Edept = DeptBl.GetById(id);
            return View("Edit", Edept);
        }
        [HttpPost]
        public IActionResult SaveEdit(Department Edept)
        {
            if (Edept.Name != null && Edept.MgrName != null)
            {
                Department newDept = DeptBl.GetById(Edept.Id);
                newDept.Name = Edept.Name;
                newDept.MgrName = Edept.MgrName;
                DeptBl.Save();
                return RedirectToAction(nameof(ShowAll));
            }
            return View(nameof(Add),Edept);

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Department dept = DeptBl.GetById(id);
            if (dept == null) return NotFound();
            DepartmentWithExtraDetails deptDet = new DepartmentWithExtraDetails();
            deptDet.DeptName = dept.Name;
            deptDet.State = dept.Students.Count > 50 ? "Main" : "Branch";
            deptDet.StudentsMoreThan25 = dept.Students.Where(s => s.Age > 25).ToList();
            return View("Details", deptDet);
        }


    }
}
