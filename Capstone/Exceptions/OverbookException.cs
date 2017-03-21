using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Exceptions
{
    public class OverbookException: Exception
    {
        public OverbookException(string message)
            :base(message)
        {
           
        }
    }
}
