﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Exceptions
{
    public class BookingConflictException: Exception
    {
        public BookingConflictException(string message)
            :base(message)
        {

        }
    }
}
