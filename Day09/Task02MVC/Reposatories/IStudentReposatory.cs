using Microsoft.EntityFrameworkCore;
using Task02MVC.Data.Contexts;
using Task02MVC.Models;

namespace Task02MVC.Reposatories
{
    public interface IStudentReposatory
    {
        public List<Student> GetAll();
        public Student GetById(int id);
        public void AddStudent(Student student);
        public void UpdateStudent(Student student);
        public void Save();
        public void Delete(Student student);
    }
}
