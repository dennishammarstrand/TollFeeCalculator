using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Services
{
    public interface ITollService
    {
        int GetTollFee(DateTime date, IVehicle vehicle);
        int GetTotalTollFee(IVehicle vehicle, DateTime[] dates);
        List<(DateTime, int)> CalculateFeeForDates(DateTime[] dates, IVehicle vehicle);
        int CalculateTotalTollFee(List<(DateTime date, int value)> timeFeeValues);
    }
}