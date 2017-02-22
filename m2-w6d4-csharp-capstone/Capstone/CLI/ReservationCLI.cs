using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    class ReservationCLI
    {
        

        public void RunReservationCLI()
        {
            ReservationMenu();
            while (true)
            {
                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Reservation reservation = SearchDateAvailabilityByCampground();
                        if (reservation != null)
                        {
                            BookReservation(reservation);                            
                        }
                      
                        return;
                    case "2":
                        return;

                    default:
                        Console.WriteLine("Command not valid.");
                        break;
                }
            }
        }
        public void ReservationMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("1)Search for Available Reservation");
            Console.WriteLine("2)Return to Previous Screen");
        }

        public Reservation SearchDateAvailabilityByCampground()
        {

            string campground = CLIHelper.GetString("Please enter campground name: ");
            DateTime startDate = CLIHelper.GetDateTime("Please enter a start date: ");
            DateTime endDate = CLIHelper.GetDateTime("Please enter an end date: ");
            Reservation reservation = new Reservation()
            {
                CampgroundName = campground,
                FromDate = startDate,
                ToDate = endDate
            };

            CampgroundSqlDAL dal = new CampgroundSqlDAL();
            List<Site> siteList = dal.SearchDateAvailabilityByCampground(reservation);
            if (siteList.Count < 1)
            {
                Console.WriteLine("No available sites for this date range.");
                return null;
            }
            else
            {
                siteList.ForEach(site => Console.WriteLine(site));
            }

            return reservation;
        }

        public void BookReservation(Reservation r)
        {

            string name = CLIHelper.GetString("Please enter first and last name:");
            int siteNumber = CLIHelper.GetInteger("Please enter the site number: ");
            int numberCampers = CLIHelper.GetInteger("Please enter the number of campers:");

            int reservationID = 0;


            r.Name = name;
            r.SiteNumber = siteNumber;
            r.NumberCampers = numberCampers;


            ReservationSqlDAL dal = new ReservationSqlDAL();
            try
            {
                reservationID = dal.BookReservation(r);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (reservationID > 0)
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
