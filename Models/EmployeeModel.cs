using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DomingoRoofWork.Models
{
    public class EmployeeModel
    {
        [Display(Name="Employee Number")]
        [StringLength(maximumLength:6)]
        public string EmpNo { get; set; }

        [Display (Name="First Name")]
        [Required(ErrorMessage = "First Name needs to be entered")]
        public string EmpFirstName { get; set; }

        [Display (Name="Surname")]
        [Required(ErrorMessage = "Surname needs to be entered")]
        public string EmpSurname { get; set; }

        [Display(Name = "Job Card No")]
        [Required(ErrorMessage = "Please enter a job card number")]
        public int JobCardNo { get; set; } = 0;
    }
}