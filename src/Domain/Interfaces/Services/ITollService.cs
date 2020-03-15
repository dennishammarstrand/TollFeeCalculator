using System;

namespace Domain.Interfaces.Services
{
    public interface ITollService
    {
        int GetTollFee(DateTime date, IVehicle vehicle);
        int GetTotalTollFee(IVehicle vehicle, DateTime[] dates);
    }
}