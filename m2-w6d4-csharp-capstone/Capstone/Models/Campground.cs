using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenFrom { get; set; }
        public int OpenTil { get; set; }
        public double DailyFee { get; set; }

        public override string ToString()
        {
            return CampgroundId.ToString().PadRight(5) + Name.PadRight(20) + OpenFrom.ToString().PadRight(10) + OpenTil.ToString().PadRight(10) + DailyFee.ToString("C2");

        }
    }
}