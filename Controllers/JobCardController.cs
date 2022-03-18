using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomingoRoofWork.Models;
using DomingoRoofWork.Repository;

namespace DomingoRoofWork.Controllers
{
    public class JobCardController : Controller
    {
        public ActionResult GetAllJobCards()
        {
            JobCardRepo jobCard = new JobCardRepo();
            ModelState.Clear();
            return View(jobCard.GetJobCards());
        }

        //Create new Job Card 
        public ActionResult CreateJobCard()
        {
            return View("");
        }

        [HttpPost]
        public ActionResult CreateJobCard(JobCardModel jobCard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JobCardRepo jobCardRepo = new JobCardRepo();
                    if (jobCardRepo.AddNewJobCard(jobCard))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }
                ModelState.Clear();
                return View();
            }
            catch
            {

                return View();
            }
        }
    }
}