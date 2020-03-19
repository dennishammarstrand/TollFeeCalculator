using Domain.Interfaces.Repositories;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Infrastructure.Repositories
{
    public class TollFeeRepository : ITollFeeRepository
    {
        public int GetTollFee(DateTime date)
        {
            Guard.CheckForNull(date);
            Guard.ValidateDate(date);
            var dates = new Dictionary<(DateTime start, DateTime end), int>();
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = buildDir + @"\Database\TollValues.txt";
            var fileDates = File.ReadAllLines(filePath);
            var fee = 0;

            foreach (var line in fileDates)
            {
                var time1 = DateTime.ParseExact(line.Substring(0, 5), "H:mm", null, System.Globalization.DateTimeStyles.None);
                var time2 = DateTime.ParseExact(line.Substring(6, 5), "H:mm", null, System.Globalization.DateTimeStyles.None);
                var value = int.Parse(line.Substring(12));
                dates.Add((time1, time2), value);
            }

            foreach (KeyValuePair<(DateTime start, DateTime end), int> interval in dates)
            {
                var start = interval.Key.start.TimeOfDay;
                var end = interval.Key.end.TimeOfDay;
                var timeToCheck = date.TimeOfDay;
                if ((timeToCheck >= start) && (timeToCheck <= end))
                {
                    fee = interval.Value;
                }
            }
            return fee;
        }
    }
}
