using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Task02MVC.ViewModels
{
    public class CoursesVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        [Range(50, 100, ErrorMessage = "Degree must be between 50 and 100")]
        public int Degree { get; set; }

        [Required(ErrorMessage = "Min Degree is required")]
        [Range(50, 100, ErrorMessage = "Min Degree must be between 50 and 100")]
        public int MinDegree { get; set; }


        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        [ValidateNever]
        public SelectList Departments { get; set; }
    }
}
