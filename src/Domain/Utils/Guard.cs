using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Utils
{
    public static class Guard
    {
        public static void ValidateDatesOfSameDay(DateTime[] dates)
        {
            if (!dates.Any())
            {
                throw new EmptyDateException("Please enter valid date");
            }
            var day = dates[0].Date;
            if (!dates.All(d => d.Date == day))
            {
                throw new DateMissmatchException("Dates must be during the same day.");
            }
        }

        public static void CheckForNull(params object[] objs)
        {
            for (var i = 0; i < objs.Length; i++)
            {
                if (objs[i] == null)
                {
                    throw new ArgumentNullException();
                }
            }
        }

        public static void ValidateDates(params DateTime[] dates)
        {
            if (!dates.Any())
            {
                throw new EmptyDateException("Please enter valid date");
            }
        }
    }
}
