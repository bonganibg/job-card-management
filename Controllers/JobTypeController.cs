using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomingoRoofWork.Models;
using DomingoRoofWork.Repository;

namespace DomingoRoofWork.Controllers
{
    public class JobTypeController : Controller
    {
        // GET: JobType
        public ActionResult ViewJobTypes()
        {
            JobTypeRepo jtRepo = new JobTypeRepo();
            ModelState.Clear();
            return View(jtRepo.ViewJobTypes());
        }

        // GET: JobType/Edit/5
        public ActionResult UpdateDailyRate(int id)
        {
            JobTypeRepo jtRepo = new JobTypeRepo();

            return View(jtRepo.ViewJobTypes().Find(jt => jt.JobTypeID == id));
        }

        // POST: JobType/Edit/5
        [HttpPost]
        public ActionResult UpdateDailyRate(int id, JobTypeModel obj)
        {
            try
            {
                JobTypeRepo jtRepo = new JobTypeRepo();
                obj.JobTypeID = id;
                jtRepo.UpdateJobType(obj);

                return RedirectToAction("ViewJobTypes");

            }
            catch
            {
                return View();
            }
        }
    }
}
