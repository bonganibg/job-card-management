using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomingoRoofWork.Models;
using DomingoRoofWork.Repository;

namespace DomingoRoofWork.Controllers
{
    public class InvoiceController : Controller
    {
        //View all of the available invoices
        public ActionResult GetAllInvoices()
        {
            InvoiceRepo invoices = new InvoiceRepo();
            ModelState.Clear();
            return View(invoices.GetAllInvoices());
        }

        public ActionResult InvoiceDetails(int id)
        {
            InvoiceRepo invoice = new InvoiceRepo();

            return View(invoice.GetInvoiceDetails(id).Find(inv => inv.JobCardNumber == id));
        }
    }
}