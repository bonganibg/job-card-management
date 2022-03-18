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
    public class JobCardRepo
    {
        SqlConnection conn;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DoDB"].ToString();
            conn = new SqlConnection(constr);
        }

        /// <summary>
        /// Creates a new job card depending on the information given by the user
        /// </summary>
        /// <param name="obj"> the model that holds all of the information that will be added </param>
        /// <returns> true or false depending on whether the changes were successful </returns>
        public bool AddNewJobCard(JobCardModel obj)
        {
            Connection();
            SqlCommand AddCommand = new SqlCommand("CreateJobCard", conn);
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.Parameters.AddWithValue("@PhysicalAddress", obj.PhysicalAddress);
            AddCommand.Parameters.AddWithValue("@Province", obj.Province);
            AddCommand.Parameters.AddWithValue("@Code", obj.Code);
            AddCommand.Parameters.AddWithValue("@CustomerName", obj.CustomerName);
            AddCommand.Parameters.AddWithValue("@CustomerSurname", obj.CustomerSurname);
            AddCommand.Parameters.AddWithValue("@JobTypeID", obj.JobTypeID);
            AddCommand.Parameters.AddWithValue("@NumOfDays", obj.NumOfDays);                

            conn.Open();
            int i = AddCommand.ExecuteNonQuery();;
            conn.Close();
            if (obj.StandardFB > 0)
                 AddMaterials(10,obj.StandardFB);

            if (obj.PowerPoints > 0)
                AddMaterials(20, obj.PowerPoints);

            if (obj.StandardEW > 0)
                AddMaterials(30, obj.StandardEW);

            if (obj.StandardSP > 0)
                AddMaterials(40, obj.StandardSP);
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
        /// adds the materials that were used on a job card to the database
        /// </summary>
        /// <param name="MaterialID"> the id for the material that has been used </param>
        /// <param name="Quantity"> the amount of the material that was used </param>
        public void AddMaterials(int MaterialID,int Quantity)
        {
            Connection();
            SqlCommand AddMaterial = new SqlCommand("MaterialsForJob", conn);
            AddMaterial.CommandType = CommandType.StoredProcedure;

            AddMaterial.Parameters.AddWithValue("@Material", MaterialID);
            AddMaterial.Parameters.AddWithValue("@Quantity", Quantity);

            conn.Open();
            int i = AddMaterial.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// gets all of the existing job cards
        /// </summary>
        /// <returns> a list of all of the job card information </returns>
        public List<JobCardModel> GetJobCards()
        {
            Connection();
            List<JobCardModel> jobCard = new List<JobCardModel>();

            SqlCommand GetCommand = new SqlCommand("GetJobCard", conn);
            GetCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(GetCommand);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                jobCard.Add(
                    new JobCardModel
                    {
                        JobCardNo = Convert.ToInt32(dr["JobCardNo"]),
                        PhysicalAddress = Convert.ToString(dr["PhysicalAddress"]),
                        Province = Convert.ToString(dr["Province"]),
                        Code = Convert.ToString(dr["Code"]),
                        CustomerName = Convert.ToString(dr["CustFirstName"]),
                        CustomerSurname = Convert.ToString(dr["CustSurname"]),
                        JobTypeID = Convert.ToInt32(dr["JobCardNo"]),
                        NumOfDays = Convert.ToInt32(dr["NoOfDays"]),
                        MaterialsUsed = Convert.ToString(dr["Materials"]),
                        JobTypeName = Convert.ToString(dr["JobType"])
                    }
                    );
            }
            return jobCard;
        }
    }
}