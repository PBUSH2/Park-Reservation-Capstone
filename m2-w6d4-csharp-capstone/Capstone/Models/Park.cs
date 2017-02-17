using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime EstablishDate { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Description { get; set; }


        public override string ToString()
        {           
            return Name.ToString() + " National Park\n".PadRight(10) + "\nPark ID:".PadRight(25) + ParkId + "\nLocation:".PadRight(25) + Location.ToString() + "\nEstablished:".PadRight(25) + EstablishDate.ToShortDateString() + "\nArea:".PadRight(25) + Area.ToString("N0") + " sq km" + "\nAnnual Visitors:".PadRight(25)  + Visitors.ToString("N0") + "\n\n Description: \n \n" + Description;
        }
    }
}