using Microsoft.EntityFrameworkCore;
using Task02MVC.Data.Contexts;
using Task02MVC.Models;

namespace Task02MVC.Reposatories
{
    public class DepartmentReposatory : IDepartmentReposatory
    {
        UniDbContext context;
        public DepartmentReposatory(UniDbContext _context)
        {
            context = _context;
        }
        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return context.Departments.Include(d => d.Students).FirstOrDefault(d => d.Id == id);
        }

        public void AddDept(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
