using DomingoRoofWork.Models;
using DomingoRoofWork.Repository;
using System.Web.Mvc;

namespace DomingoRoofWork.Controllers
{
    public class EmployeeController : Controller
    {
        // Get: Employee/GetAllEmployees
        public ActionResult GetAllEmployees()
        {
            EmployeeRepo empRepo = new EmployeeRepo();
            ModelState.Clear();
            return View(empRepo.GetAllEmployees());
        }

        //Get: Employee/ReturnEmployeeView
        public ActionResult AddEmployee()
        {
            return View();
        }
        //Post: Employee/AddEmployee
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepo empRepo = new EmployeeRepo();
                    if (empRepo.AddNewEmployee(emp))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        //Get: Employee/Return EditEmployee View
        public ActionResult EditEmployee(string id)
        {
            EmployeeRepo empRepo = new EmployeeRepo();
            return View(empRepo.GetAllEmployees().Find(emp => emp.EmpNo == id));
        }
        //Post: Employee/EditEmployee
        [HttpPost]
        public ActionResult EditEmployee(string id, EmployeeModel obj)
        {
            try
            {
                EmployeeRepo empRepo = new EmployeeRepo();
                empRepo.UpdateEmployee(obj);
                return RedirectToAction("GetAllEmployees");
            }
            catch
            {
                return View();
            }
        }

        //Get: Employee/DeleteEmployee
        public ActionResult DeleteEmployee(string id)
        {
            try
            {
                EmployeeRepo empRepo = new EmployeeRepo();
                if (empRepo.DeleteEmployee(id))
                {
                    ViewBag.AlertMsg = "Employee detials deleted successfully";
                }
                return RedirectToAction("GetAllEmployees");
            }
            catch
            {

                return View();
            }
        }

        public ActionResult AddEmployeeToJobCard(string id)
        {
            EmployeeRepo empRepo = new EmployeeRepo();

            return View(empRepo.GetAllEmployees().Find(er => er.EmpNo == id));
        }

        [HttpPost]
        public ActionResult AddEmployeeToJobCard(string id, EmployeeModel obj)
        {
            try
            {
                EmployeeRepo empRepo = new EmployeeRepo();
                obj.EmpNo = id;
                empRepo.AddEmployeeToJobCard(obj);

                return RedirectToAction("GetAllEmployees");
            }
            catch
            {
                return View();
            }
        }

    }
}
