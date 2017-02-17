using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    class MainCLI

    {
        CampgroundCLI campgroundCLI = new CampgroundCLI();
        private const string Command_ViewAllParks = "1";
        private const string Command_ViewAvailableCampgroundsByPark = "2";
        private const string Command_DateAvailabilityByCampground = "3";
        private const string Command_BookReservation = "4";
        private const string Command_Quit = "q";
        Park park = new Park();
        string[] validParkNames = new string[3] { "ACADIA", "ARCHES", "CUYAHOGA VALLEY" };


        public void RunCLI()
        {
           
           

            while (true)
            {   ViewAllParks();
                string parkName = CLIHelper.GetString("Please enter a park name: ");

                Console.WriteLine();
                if (parkName.ToLower() == "q")
                {
                    break;
                }

                if(!validParkNames.Contains(parkName.ToUpper()))
                {
                    Console.WriteLine("No campsites found for given Park Names. Please enter a valid park name.");
                    continue;
                }
             
               
                park = GetParkInfo(parkName);
                
                campgroundCLI.RunCampgroundCLI(park);
               
            }
            Console.WriteLine("Thank you for using the National Park Campsite Reservation System");
        }

        public Park GetParkInfo(string parkName)
        {

            ParkSqlDAL dal = new ParkSqlDAL();
            park = dal.GetParkInfo(parkName);

            Console.WriteLine(park);
            return park;

        }
        public void ViewAllParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL();
            List<Park> parkList = dal.ViewAllParks();

            Console.WriteLine("Select a park for further details");
            for (int i = 0; i < parkList.Count; i++)
            {
                Console.WriteLine($"{i + 1})  {parkList[i].Name}");
            }

            Console.WriteLine("Q)  quit");
        }

        public void SearchDateAvailabilityByCampground()
        {

            string campground = CLIHelper.GetString("Please enter campground name: ");
            DateTime startDate = CLIHelper.GetDateTime("Please enter a start date: ");
            DateTime endDate = CLIHelper.GetDateTime("Please enter an end date: ");
            Reservation reservation = new Reservation()
            {
                Name = campground,
                FromDate = startDate,
                ToDate = endDate
            };

            CampgroundSqlDAL dal = new CampgroundSqlDAL();
            List<Site> siteList = dal.SearchDateAvailabilityByCampground(reservation);
            if (siteList.Count < 1)
            {
                Console.WriteLine("No available sites for this date range.");
            }
            else
            {
                siteList.ForEach(site => Console.WriteLine(site));
            }


        }
        public void BookReservation()
        {
            
            string name = CLIHelper.GetString("Please enter first and last name:");
            string campgroundName = CLIHelper.GetString("Please enter the campground name:");
            int siteNumber = CLIHelper.GetInteger("Please enter the site number: ");
            int numberCampers = CLIHelper.GetInteger("Please enter the number of campers:");
            DateTime startDate = CLIHelper.GetDateTime("Please enter a start date:");
            DateTime endDate = CLIHelper.GetDateTime("Please enter an end date:");
            int reservationID = 0;

                Reservation r = new Reservation()
                {
                    Name = name,
                    FromDate = startDate,
                    ToDate = endDate,
                    SiteNumber = siteNumber,
                    NumberCampers= numberCampers,
                    CampgroundName = campgroundName         
                };
            ReservationSqlDAL dal = new ReservationSqlDAL();
            try
            {
                reservationID = dal.BookReservation(r);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if(reservationID >0)
            {
                Console.WriteLine($"Your reservation was successfully created. Reservation id: {reservationID}");
            }
            else
            {
                Console.WriteLine("Request unsuccessful. Reservation NOT created.");
            }
            

            
        }


    }
}
