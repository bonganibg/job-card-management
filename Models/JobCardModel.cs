using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DomingoRoofWork.Models
{
    public class JobCardModel
	{

		public int JobCardNo { get; set; }

		[Required(ErrorMessage ="Customer Name needs to be entered")]
		[Display(Name = "Customer Name")]
		public string CustomerName { get; set; }

		[Required(ErrorMessage = "Customer Surname needs to be entered")]
		[Display(Name = "Customer surname")]
		public string CustomerSurname { get; set; }

		[Required(ErrorMessage = "Physical Address needs to be entered")]
		[Display(Name = "Physical Address")]
		public string PhysicalAddress { get; set; }

		[Required(ErrorMessage = "Province needs to be entered")]
		public string Province { get; set; }

		[Required(ErrorMessage = "Code needs to be entered")]
		public string Code { get; set; }

		[Required(ErrorMessage = "A Job Card needs to be selected")]
		[Display(Name = "Job Type")]
		public int JobTypeID { get; set; }

		[Display (Name = "Job Type")]
		public string JobTypeName { get; set; }

		[Required(ErrorMessage = "Number of days needs to be entered")]
		[Display(Name = "Number of Days")]
		public int NumOfDays { get; set; }

		public string MaterialsUsed { get; set; } = "";

		[Display(Name = "Standard Floor Boarding")]
		public int StandardFB { get; set; } = 0;

		[Display(Name = "Power Points")]
		public int PowerPoints { get; set; } = 0;

		[Display(Name = "Standard Stair Pack")]
		public int StandardSP { get; set; } = 0;

		[Display(Name = "Standard Electric Wire")]
		public int StandardEW { get; set; } = 0;








	}
}