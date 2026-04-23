using Microsoft.EntityFrameworkCore;
using Task02MVC.Data.Contexts;
using Task02MVC.Models;

namespace Task02MVC.Reposatories
{
    public class StudentReposatory : IStudentReposatory
    {
        UniDbContext _context;
        public StudentReposatory(UniDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }
        public Student GetById(int id)
        {
            return _context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
        }
        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
