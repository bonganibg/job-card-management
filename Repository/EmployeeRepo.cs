using DomingoRoofWork.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace DomingoRoofWork.Repository
{
    public class EmployeeRepo
    {
        private SqlConnection conn;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DoDB"].ToString(); 
            conn = new SqlConnection(constr);
        }

        /// <summary>
        /// Get all of the employees that are currently available 
        /// </summary>
        /// <returns> a list of employee names and employee numbers </returns>
        public List<EmployeeModel> GetAllEmployees()
        {
            Connection();
            List<EmployeeModel> EmpList = new List<EmployeeModel>();

            SqlCommand GetCommand = new SqlCommand("GetAllEmployees", conn);
            GetCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(GetCommand);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();

            //Bind EmployeeModel generic list using a data row
            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new EmployeeModel
                    {
                        EmpNo = Convert.ToString(dr["EmpNo"]),
                        EmpFirstName = Convert.ToString(dr["EmpFirstName"]),
                        EmpSurname = Convert.ToString(dr["EmpSurname"])
                    }
                    );
            }
            return EmpList;
        }

        /// <summary>
        /// add details for a new employee
        /// </summary>
        /// <param name="obj"> the employee model that took the new data </param>
        /// <returns> true or false depending on whether the information was added or not </returns>
        public bool AddNewEmployee(EmployeeModel obj)
        {
            Connection();
            SqlCommand AddCommand = new SqlCommand("CreateEmployee", conn);
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.Parameters.AddWithValue("@EmpNo", obj.EmpNo);
            AddCommand.Parameters.AddWithValue("@EmpFirstName", obj.EmpFirstName);
            AddCommand.Parameters.AddWithValue("@EmpSurname", obj.EmpSurname);

            conn.Open();
            int i = AddCommand.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// update the information for an existing employee
        /// </summary>
        /// <param name="obj"> the model that holds the edited employee information </param>
        /// <returns> true or false depending on whether the changes were successful or not  </returns>
        public bool UpdateEmployee(EmployeeModel obj)
        {
            Connection();
            SqlCommand UpdateCommand = new SqlCommand("UpdateEmployee", conn);

            UpdateCommand.CommandType = CommandType.StoredProcedure;
            UpdateCommand.Parameters.AddWithValue("@EmpNum", obj.EmpNo);
            UpdateCommand.Parameters.AddWithValue("@EmpFirstName", obj.EmpFirstName);
            UpdateCommand.Parameters.AddWithValue("@EmpSurname", obj.EmpSurname);

            conn.Open();
            int i = UpdateCommand.ExecuteNonQuery();
            conn.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// deletes an employee and their association to any jobcards
        /// </summary>
        /// <param name="empNo"> the emloyee number for the employee being deleted </param>
        /// <returns> true or false depending on whether the employee was successfully deleted </returns>
        public bool DeleteEmployee(string empNo)
        {
            Connection();
            SqlCommand DeleteCommand = new SqlCommand("DeleteEmployee", conn);
            DeleteCommand.CommandType = CommandType.StoredProcedure;
            DeleteCommand.Parameters.AddWithValue("@EmpNo", empNo);

            conn.Open();
            int i = DeleteCommand.ExecuteNonQuery();
            conn.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// assign an employee to a job card
        /// </summary>
        /// <param name="emp"> the model that holds the information </param>
        /// <returns> true or false depending on whether the changes were successful </returns>
        public bool AddEmployeeToJobCard(EmployeeModel emp)
        {
            Connection();
            SqlCommand AddCommand = new SqlCommand("AssignEmployees", conn);
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.Parameters.AddWithValue("@EmpID", emp.EmpNo);
            AddCommand.Parameters.AddWithValue("JobCardNum",emp.JobCardNo);

            conn.Open();
            int i = AddCommand.ExecuteNonQuery();
            conn.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}