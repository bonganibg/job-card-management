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
    public class JobTypeRepo
    {
        SqlConnection conn;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DoDB"].ToString();
            conn = new SqlConnection(constr);
        }


        /// <summary>
        /// show all of the job cards that are available and their costs
        /// </summary>
        /// <returns> a list of job cards and the daily rate for each </returns>
        public List<JobTypeModel> ViewJobTypes()
        {
            Connection();
            List<JobTypeModel> JobTypeList = new List<JobTypeModel>();

            //SqlCommand GetCommand = new SqlCommand("GetJobTypes", conn);
            //GetCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand GetCommand = new SqlCommand("select * from JobType", conn);
            GetCommand.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(GetCommand);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();

            //Bind EmployeeModel generic list using a data row
            foreach (DataRow dr in dt.Rows)
            {
                JobTypeList.Add(new JobTypeModel
                {
                    JobTypeID = Convert.ToInt32(dr["JobTypeID"]),
                    JobType = Convert.ToString(dr["JobType"]),
                    DailyRate = Convert.ToInt32(dr["DailyRate"])
                });
            }
            return JobTypeList;
        }


        /// <summary>
        /// updates the daily rate for a selected job card
        /// </summary>
        /// <param name="obj"> the model that holds the job id and the new daily rate </param>
        /// <returns> true or false depending the outcome of the query </returns>
        public bool UpdateJobType(JobTypeModel obj)
        {
            Connection();
            SqlCommand UpdateCommand = new SqlCommand("UpdateDailyRate", conn);

            UpdateCommand.CommandType = CommandType.StoredProcedure;
            UpdateCommand.Parameters.AddWithValue("@Amount", obj.DailyRate);
            UpdateCommand.Parameters.AddWithValue("@JobType", obj.JobTypeID);

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
    }
}