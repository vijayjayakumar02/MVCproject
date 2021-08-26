using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Models
{
    public class EmpDetails
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(25, ErrorMessage = "Name can not exceed 25 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }

        [Range(1, 100000, ErrorMessage = "Enter valid Department Id")]
        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }

        public DateTime HireDate { get; set; }

        public bool Status { get; set; }
    }
}
