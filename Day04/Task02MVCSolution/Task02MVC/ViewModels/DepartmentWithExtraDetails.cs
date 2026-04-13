using Task02MVC.Models;

namespace Task02MVC.ViewModels
{
    public class DepartmentWithExtraDetails
    {
        public string DeptName { get; set; }
        public string State { get; set; }

        public List<Student> StudentsMoreThan25 { get; set; }   
    }
}
