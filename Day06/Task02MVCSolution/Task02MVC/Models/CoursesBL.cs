using Microsoft.EntityFrameworkCore;
using Task02MVC.Data.Contexts;
using Task02MVC.ViewModels;

namespace Task02MVC.Models
{
    public class CoursesBL
    {
        UniDbContext context = new UniDbContext();

        public List<Course> GetAll()
        {
            return context.Courses.Include(c => c.Department).ToList();
        }
        public Course GetById(int id)
        {
            return context.Courses.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }
        public void AddCourse(Course Course)
        {
            context.Courses.Add(Course);
        }
        public void UpdateCourse(Course Course)
        {
            context.Courses.Update(Course);
            context.SaveChanges();
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Delete(Course Course)
        {
            context.Courses.Remove(Course);
            context.SaveChanges();
        }
        public STCRVM GetStudentResult(int studentId, int courseId)
        {
            var result = context.StudCourseResults
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefault(x => x.StudentId == studentId && x.CourseId == courseId);

            if (result == null)
            {
                throw new Exception("No record found in StuCrsRes");
            }

            return new STCRVM
            {
                StudentName = result.Student.Name,
                CourseName = result.Course.Name,
                Degree = result.Grade,
                StatusColor = result.Grade >= result.Course.MinDegree ? "green" : "red"
            };
        }
    }
}
