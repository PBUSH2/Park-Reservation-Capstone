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
    class CampgroundSqlDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        const string SQL_SearchCampgroundByPark = "Select * from Campground inner join park on park.park_id = campground.park_id where park.name = @parkname";
        const string SQL_SearchDateAvailabilityByCampground = "Select distinct top 5 site.* from site inner join campground on campground.campground_id = site.campground_id inner join reservation on reservation.site_id = site.site_id where from_date not between @fromdate and @enddate and to_date not between @fromdate and @enddate and campground.name = @campgroundname;";
        public List<Campground> SearchCampgroundByPark(Park park)
        {
            List<Campground> campgroundList = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_SearchCampgroundByPark, conn);
                    cmd.Parameters.AddWithValue("@parkname", park.Name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground()
                        {
                            Name = Convert.ToString(reader["name"]),
                            CampgroundId = Convert.ToInt32(reader["campground_id"]),
                            DailyFee = Convert.ToDouble(reader["daily_fee"]),
                            OpenFrom = Convert.ToInt32(reader["open_from_mm"]),
                            OpenTil = Convert.ToInt32(reader["open_to_mm"]),
                            ParkId = Convert.ToInt32(reader["park_id"])
                        };

                        campgroundList.Add(c);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return campgroundList;
        }

        public List<Site> SearchDateAvailabilityByCampground(Reservation r)
        {
            List<Site> siteList = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_SearchDateAvailabilityByCampground, conn);

                    cmd.Parameters.AddWithValue("@fromdate", r.FromDate);
                    cmd.Parameters.AddWithValue("@enddate", r.ToDate);
                    cmd.Parameters.AddWithValue("@campgroundname", r.CampgroundName);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site()
                        {
                            SiteId = Convert.ToInt32(reader["site_id"]),
                            SiteNumber = Convert.ToInt32(reader["site_number"]),
                            Accessible = Convert.ToBoolean(reader["accessible"]),
                            MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]),
                            MaxRvLength = Convert.ToInt32(reader["max_rv_length"]),
                            Utilities = Convert.ToBoolean(reader["utilities"])
                        };
                        siteList.Add(s);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return siteList;
        }
    }
}
