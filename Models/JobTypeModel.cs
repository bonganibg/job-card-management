using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DomingoRoofWork.Models
{
    public class JobTypeModel
    {
        [Display(Name = "Job Type ID")]
        public int JobTypeID { get; set; }

        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Display(Name = "Daily Rate")]
        [Required(ErrorMessage = "Please enter an amount")]
        public int DailyRate { get; set; }
    }
}