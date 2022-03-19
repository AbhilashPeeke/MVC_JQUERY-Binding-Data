using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_CRUD_jqry_Azax.ViewModel
{
    public class EmployeeViewModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [DisplayName("First Name : ")]
        [Required(ErrorMessage ="First Name Is Required....!")]
        public string FirstName { get; set; }
        [DisplayName("Last Name : ")]
        [Required(ErrorMessage = "Last Name Is Required....!")]
        public string LastName { get; set; }
        [DisplayName("Department : ")]
        [Required(ErrorMessage = "Department Is Required....!")]
        public string Department { get; set; }
        [DisplayName("Job Type : ")]
        [Required(ErrorMessage = "Job Type Is Required....!")]
        public string JobType { get; set; }
        [DisplayName("Salary : ")]
        [Required(ErrorMessage = "Salary Is Required....!")]
        public decimal Salary { get; set; }
        public int CityId { get; set; }
    }
}