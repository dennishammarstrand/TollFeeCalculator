using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class DateMissmatchException : Exception
    {
        public DateMissmatchException(string message) : base(message)
        {

        }
    }
}
