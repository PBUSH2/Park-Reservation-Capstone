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
            //return $"{Name.ToString()} National Park \n Park ID: {ParkId.ToString().PadRight(20)}   \n Location: {Location.ToString().PadRight(20)} \n Established: {EstablishDate.ToShortDateString().PadRight(20)} \n Area: {Area.ToString().PadRight(20)} \n Annual Visitors: {Visitors.ToString().PadRight(20)}";
            return Name.ToString() +  "National Park \n Location:".PadRight(20) + Location.ToString() + " \n Established:".PadRight(20) + EstablishDate.ToShortDateString() + " \n Area:".PadRight(20) + Area.ToString() + " \n Annual Visitors:".PadRight(20)  + Visitors.ToString() + "\n Description: \n" + Description;
        }
    }
}