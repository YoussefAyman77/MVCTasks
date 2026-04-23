using Task02MVC.Data.Contexts;
using Task02MVC.Models;
using Task02MVC.ViewModels;

namespace Task02MVC.Reposatories
{
    public interface ICourseReposatory
    {

        public List<Course> GetAll();
        public Course GetById(int id);
        public void AddCourse(Course Course);
        public void UpdateCourse(Course Course);
        public void Save();
        public void Delete(Course Course);
        public STCRVM GetStudentResult(int studentId, int courseId);
    }
}
