using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task02MVC.Data.Contexts;
using Task02MVC.Models;
using Task02MVC.Reposatories;
using Task02MVC.ViewModels;

namespace Task02MVC.Controllers
{
    public class CoursesController : Controller
    {
        //private readonly UniDbContext _context;

 

        //UniDbContext _context = new UniDbContext();
        ICourseReposatory CoursesBl;
        IDepartmentReposatory DepartmentBL;
        public CoursesController(IDepartmentReposatory DeptR,ICourseReposatory CrR)
        {
            CoursesBl = CrR;
            DepartmentBL = DeptR;

        }

        // GET: Courses
        public IActionResult Index()
        {
            List<Course> courses = CoursesBl.GetAll();
            return View("Index", courses);
        }

        // GET: Courses/Details/5
        public IActionResult Details(int id)
        {
            Course course = CoursesBl.GetById(id);

            if (course == null)
                return NotFound();

            return View("Details", course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            CoursesVM vm = new CoursesVM();

            List<Department> departments = DepartmentBL.GetAll();

            vm.Departments = new SelectList(departments, "Id", "Name");

            return View("Create", vm);
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SaveCreate(CoursesVM vm)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course()
                {
                    Name = vm.Name,
                    Degree = vm.Degree,
                    MinDegree = vm.MinDegree,
                    DepartmentId = vm.DepartmentId
                };

                CoursesBl.AddCourse(course);
                CoursesBl.Save();

                return RedirectToAction(nameof(Index));
            }

            List<Department> departments = DepartmentBL.GetAll();
            vm.Departments = new SelectList(departments, "Id", "Name");

            return View("Create", vm);
        }

        // GET: Courses/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course = CoursesBl.GetById(id);

            if (course == null)
                return NotFound();

            CoursesVM vm = new CoursesVM()
            {
                Id = course.Id,
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                DepartmentId = course.DepartmentId
            };

            List<Department> departments = DepartmentBL.GetAll();
            vm.Departments = new SelectList(departments, "Id", "Name", course.DepartmentId);

            return View("Edit", vm);
        }


        [HttpPost]
        public IActionResult SaveEdit(CoursesVM vm)
        {
            if (ModelState.IsValid)
            {
                Course course = CoursesBl.GetById(vm.Id);

                if (course == null)
                    return NotFound();

                course.Name = vm.Name;
                course.Degree = vm.Degree;
                course.MinDegree = vm.MinDegree;
                course.DepartmentId = vm.DepartmentId;

                CoursesBl.Save();

                return RedirectToAction(nameof(Index));
            }

            List<Department> departments = DepartmentBL.GetAll();
            vm.Departments = new SelectList(departments, "Id", "Name");

            return View("Edit", vm);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Course course = CoursesBl.GetById(id);

            if (course == null)
                return NotFound();

            return View("Delete", course);
        }

        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            Course course = CoursesBl.GetById(id);

            if (course != null)
            {
                CoursesBl.Delete(course);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult StudentCourseResult(int studentId, int courseId)
        {
            var result = CoursesBl.GetStudentResult(studentId, courseId);

            if (result == null)
                return NotFound();

            return View("StudentCourseResult", result);
        }


    }
}
