using Capstone.Exceptions;
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
    public class ReservationSqlDAL
    {
        int reservationId;
        string SQL_FindSiteID = "Select site_id from site join campground on campground.campground_id = site.campground_id where campground.name = @campgroundname; SELECT cast(scope_identity() as int);";
        string SQL_BookReservation = $"Insert into reservation Values(@siteid, @reservationname, @fromdate, @enddate, @createdate); SELECT cast(scope_identity() as int);";
        string SQL_CheckMaxOccupancy = "Select max_occupancy from site inner join campground on site.campground_id = campground.campground_id where campground.name = @campgroundname and site.site_number = @sitenumber; Select cast(scope_identity() as int);";
        const string SQL_SearchForConflicts = "Select Count (*) from site inner join campground on campground.campground_id = site.campground_id inner join reservation on reservation.site_id = site.site_id where from_date between @fromdate and @enddate and to_date between @fromdate and @enddate and campground.name = @campgroundname and site.site_number = @sitenumber; Select cast(scope_identity() as int);";
        string ConnectionString = @"Data Source= .\SQLEXPRESS;Initial Catalog = campground; User ID = te_student;Password = sqlserver1";
        //string ConnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        public int BookReservation(Reservation r)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd;
                    cmd = new SqlCommand(SQL_CheckMaxOccupancy, conn);
                    cmd.Parameters.AddWithValue("@campgroundname", r.CampgroundName);
                    cmd.Parameters.AddWithValue("@sitenumber", r.SiteNumber);

                    int maxOccupancy = (int)cmd.ExecuteScalar();
                    if (maxOccupancy < r.NumberCampers)
                    {
                        throw new OverbookException($"The maximum occupancy of this campsite is {maxOccupancy}");
                    }

                    cmd = new SqlCommand(SQL_SearchForConflicts, conn);
                    cmd.Parameters.AddWithValue("@fromdate", r.FromDate);
                    cmd.Parameters.AddWithValue("@enddate", r.ToDate);
                    cmd.Parameters.AddWithValue("@campgroundname", r.CampgroundName);
                    cmd.Parameters.AddWithValue("@sitenumber", r.SiteNumber);

                    int numberOfConflicts = (int)cmd.ExecuteScalar();
                    if (numberOfConflicts > 0)
                    {
                        throw new BookingConflictException("This site is already booked for one or more of the dates in this range.");
                    }

                    cmd = new SqlCommand(SQL_BookReservation, conn);
                    cmd.Parameters.AddWithValue("@reservationname", r.Name);
                    cmd.Parameters.AddWithValue("@siteid", r.SiteId);
                    cmd.Parameters.AddWithValue("@fromdate", r.FromDate);
                    cmd.Parameters.AddWithValue("@enddate", r.ToDate);
                    cmd.Parameters.AddWithValue("@createdate", Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
                    reservationId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return reservationId;
        }
    }
}
