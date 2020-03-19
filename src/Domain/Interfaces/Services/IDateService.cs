using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Services
{
    public interface IDateService
    {
        bool TimeIntervalLessThanAnHour(DateTime firstTime, DateTime secondTime);
    }
}
