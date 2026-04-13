using System.Reflection.Metadata.Ecma335;
using Task02MVC.Models;

namespace Task02MVC.ViewModels
{
    public class StudentAddEditVM
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }

        public int DepartmentId { get; set; }

        public List<Department> Departments { get; set; }
    }
}
