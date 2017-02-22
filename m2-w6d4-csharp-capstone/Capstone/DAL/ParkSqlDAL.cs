using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{


    class ParkSqlDAL
    {
        
        private const string commandString = "Select * from Park";
        string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private const string SQL_GetParkInfo = "Select * from Park where park.name = @parkname";

        public Park GetParkInfo(string parkName)
        {
            Park park = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetParkInfo, conn);
                    cmd.Parameters.AddWithValue("@parkname", parkName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        park = CreatePark(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return park;
        }

        private Park CreatePark(SqlDataReader reader)
        {
            return new Park()
            {
                Name = Convert.ToString(reader["name"]),
                Area = Convert.ToInt32(reader["area"]),
                Location = Convert.ToString(reader["location"]),
                Description = Convert.ToString(reader["description"]),
                EstablishDate = Convert.ToDateTime(reader["establish_date"]),
                ParkId = Convert.ToInt32(reader["park_id"]),
                Visitors = Convert.ToInt32(reader["visitors"])
            };
        }

        public List<Park> ViewAllParks()
        {
            List<Park> parkList = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(commandString, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        parkList.Add(CreatePark(reader));
                    }
                }
            }
            catch (SqlException e)
            {

                throw;
            }
            return parkList;
        }
    }
}
