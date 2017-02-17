using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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
            return CampgroundId.ToString().PadRight(5) + Name.PadRight(35) + DateTimeFormatInfo.CurrentInfo.GetMonthName(OpenFrom).PadRight(12) + DateTimeFormatInfo.CurrentInfo.GetMonthName(OpenTil).ToString().PadRight(12) + DailyFee.ToString("C2");

        }
    }
}