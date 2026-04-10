using Task02MVC.Data.Contexts;

namespace Task02MVC.Models
{
    public class StudentBL : Student
    {
        UniDbContext context = new UniDbContext();
        public List<Student> GetAll()
        {
            return context.Students.ToList();
        }
        public Student GetById(int id)
        {
            return context.Students.FirstOrDefault(s => s.Id == id);
        }
    }
}
