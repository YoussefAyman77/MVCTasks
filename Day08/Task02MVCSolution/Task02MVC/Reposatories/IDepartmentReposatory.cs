using Task02MVC.Data.Contexts;
using Task02MVC.Models;

namespace Task02MVC.Reposatories
{
    public interface IDepartmentReposatory
    {
        public List<Department> GetAll();

        public Department GetById(int id);

        public void AddDept(Department department);
        public void Save();
    }
}
