using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class EmptyDateException : Exception
    {
        public EmptyDateException(string message) : base(message)
        {
        }
    }
}
