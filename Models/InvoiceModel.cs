using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DomingoRoofWork.Models
{
    public class InvoiceModel
    {
        [Required(ErrorMessage ="You need to enter a Job Card Number")]
        public int JobCardNumber { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerSurname { get; set; }

        public string PhysicalAddress { get; set; }

        public string Province { get; set; }

        public string Code { get; set; }

        [Display (Name = "Job Type")]
        public string JobType { get; set; }

        [Display (Name = "Rate")]
        public string DailyRate { get; set; }

        [Display (Name = "No. of Days")]
        public int NoOfDays { get; set; }

        public string Subtotal { get; set; }

        [Display(Name = "VAT")]
        public string Vat { get; set; }

        public string Total { get; set; }

        [Display(Name = "Equipment/Materials")]
        public string Materials { get; set; }

        [Display(Name = "Employees Allocated")]
        public string Employees { get; set; }
    }
}