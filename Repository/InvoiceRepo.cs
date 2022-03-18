using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DomingoRoofWork.Models;

namespace DomingoRoofWork.Repository
{
    public class InvoiceRepo
    {
        private SqlConnection conn;

        /// <summary>
        /// Connect the application to the database
        /// </summary>
        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DoDB"].ToString();
            conn = new SqlConnection(constr);
        }

        /// <summary>
        /// get the invoices for all of the job cards that have been done
        /// </summary>
        /// <returns> list of job card numbers and customers who have a job card  </returns>
        public List<InvoiceModel> GetAllInvoices()
        {
            Connection();
            SqlCommand GetInvoice = new SqlCommand("GetAllInvoices", conn);
            GetInvoice.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(GetInvoice);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();

            List<InvoiceModel> InvoiceList = new List<InvoiceModel>();

            foreach (DataRow dr in dt.Rows)
            {
                InvoiceList.Add(
                    new InvoiceModel
                    {
                        JobCardNumber = Convert.ToInt32(dr["JobCardNo"]),
                        CustomerFirstName = Convert.ToString(dr["CustFirstName"]),
                        CustomerSurname = Convert.ToString(dr["CustSurname"]),
                    }
                    );
            }
            return InvoiceList;
        }

        /// <summary>
        /// gets the job card number and finds the corresponding invoice details 
        /// </summary>
        /// <param name="JobCardNum"> the job card number for the invoice </param>
        /// <returns> a list of all of the information needed to create an invoice </returns>
        public List<InvoiceModel> GetInvoiceDetails(int JobCardNum)
        {
            Connection();
            SqlCommand GetInvoice = new SqlCommand("GetInvoice", conn);
            GetInvoice.CommandType = CommandType.StoredProcedure;
            GetInvoice.Parameters.AddWithValue("@JobCardNum", JobCardNum);
            SqlDataAdapter da = new SqlDataAdapter(GetInvoice);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();

            List<InvoiceModel> InvoiceList = new List<InvoiceModel>();

            foreach (DataRow dr in dt.Rows)
            {
                InvoiceList.Add(
                    new InvoiceModel
                    {
                        JobCardNumber = Convert.ToInt32(dr["JobCardNo"]),
                        CustomerFirstName = Convert.ToString(dr["CustFirstName"]),
                        CustomerSurname = Convert.ToString(dr["CustSurname"]),
                        PhysicalAddress = Convert.ToString(dr["PhysicalAddress"]),
                        Province = Convert.ToString(dr["Province"]),
                        Code = Convert.ToString(dr["Code"]),
                        JobType = Convert.ToString(dr["JobType"]),
                        DailyRate = String.Format("{0:C}",dr["DailyRate"]),
                        NoOfDays = Convert.ToInt32(dr["NoOfDays"]),
                        Subtotal = String.Format("{0:C}", dr["Subtotal"]),
                        Vat = String.Format("{0:C}", dr["VAT"]),
                        Total = String.Format("{0:C}", dr["Total"]),
                        Materials = Convert.ToString(dr["Materials"]),
                        Employees = Convert.ToString(dr["Employees"])
                    }
                    );
            }
            return InvoiceList;
        }
    }
}