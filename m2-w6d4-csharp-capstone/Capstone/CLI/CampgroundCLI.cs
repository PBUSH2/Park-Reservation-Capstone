using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
  
    class CampgroundCLI
    {
        ReservationCLI reservationCLI = new ReservationCLI();
        Reservation reservation = new Reservation();
        const string Command_ViewCampgrounds = "1";
        const string Command_ReturnToPreviousScreen = "2";
   

        public void RunCampgroundCLI(Park park)
        {          
            while (true)
            {   CampgroundMenu();
                string command = Console.ReadLine();

                switch (command)
                {
                    case Command_ViewCampgrounds:

                        SearchCampgroundsByPark(park);
                  
                        reservationCLI.RunReservationCLI();
                        break;

                    case Command_ReturnToPreviousScreen:                    
                        return;



                    default:
                        break;
                }
            }
        }
        public void CampgroundMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Return to Previous Screen");
        }

        public void SearchCampgroundsByPark(Park park)
        {      
            CampgroundSqlDAL dal = new CampgroundSqlDAL();
            List<Campground> campgroundList = dal.SearchCampgroundByPark(park);
            if (campgroundList.Count < 1)
            { Console.WriteLine("No campgrounds exist with this park name. Please enter a valid park name.");
                return;
            }
            else
            {
                Console.WriteLine("     Name".PadRight(40) + "Open".PadRight(12) + "Close".PadRight(12) + "Daily Fee");
                campgroundList.ForEach(camp =>
                Console.WriteLine(camp)
            );
            }
        }
        //public Reservation SearchDateAvailabilityByCampground()
        //{

        //    string campground = CLIHelper.GetString("Please enter campground name: ");
        //    DateTime startDate = CLIHelper.GetDateTime("Please enter a start date: ");
        //    DateTime endDate = CLIHelper.GetDateTime("Please enter an end date: ");
        //    Reservation reservation = new Reservation()
        //    {
        //        CampgroundName = campground,
        //        FromDate = startDate,
        //        ToDate = endDate
        //    };

        //    CampgroundSqlDAL dal = new CampgroundSqlDAL();
        //    List<Site> siteList = dal.SearchDateAvailabilityByCampground(reservation);
        //    if (siteList.Count < 1)
        //    {
        //        Console.WriteLine("No available sites for this date range.");
        //        return null;
        //    }
        //    else
        //    {
        //        siteList.ForEach(site => Console.WriteLine(site));
        //    }

        //    return reservation;
        //}
    }

}