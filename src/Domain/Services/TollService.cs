using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class TollService : ITollService
    {
        private readonly ITollFeeRepository _tollFeeRepository;
        public TollService(ITollFeeRepository tollFeeRepository)
        {
            _tollFeeRepository = tollFeeRepository;
        }

        public int GetTotalTollFee(IVehicle vehicle, DateTime[] dates)
        {
            Guard.CheckForNull(vehicle, dates);
            Guard.ValidateDatesOfSameDay(dates);
            var orderedDates = dates.OrderBy(x => x.TimeOfDay).ToArray();
            return CalculateTotalTollFee(CalculateFeeForDates(orderedDates, vehicle));
        }

        public int GetTollFeeForDate(DateTime date, IVehicle vehicle)
        {
            Guard.CheckForNull(vehicle);
            Guard.ValidateDates(date);
            if (date.IsTollFreeDate() || vehicle.GetVehicleType().IsTollFreeVehicle()) return 0;

            return _tollFeeRepository.GetTollFee(date);
        }

        public List<(DateTime, int)> CalculateFeeForDates(DateTime[] dates, IVehicle vehicle)
        {
            Guard.CheckForNull(dates, vehicle);
            Guard.ValidateDates(dates);
            var dateFeeValues = new List<(DateTime, int)>();
            foreach (var time in dates)
            {
                var fee = GetTollFeeForDate(time, vehicle);
                dateFeeValues.Add((time, fee));
            }
            return dateFeeValues;
        }

        public int CalculateTotalTollFee(List<(DateTime date, int value)> dateFeeValues)
        {

            Guard.CheckForNull(dateFeeValues);
            Guard.ValidateDates(dateFeeValues.Select(s => s.date).ToArray());
            var fee = 0;
            var highestDateFees = new List<(DateTime time, int fee)>();
            foreach (var timeFee in dateFeeValues)
            {
                fee = 0;
                highestDateFees.Add(GetHighestFeeForGivenDate(dateFeeValues, timeFee));
                fee += highestDateFees.Distinct().Sum(s => s.fee);
                if (fee >= 60)
                {
                    fee = 60;
                    break;
                }
            }
            return fee;
        }

        private (DateTime date, int fee) GetHighestFeeForGivenDate(List<(DateTime date, int fee)> dateFeeValues, (DateTime date, int fee) currentDateFee)
        {
            var feesForInterval = dateFeeValues.Where(s => DateTimeExtension.TimeIntervalLessThanAnHour(s.date, currentDateFee.date)).ToList();
            return feesForInterval.OrderByDescending(s => s.fee).ThenByDescending(s => s.date).ToList().First();
        }
    }
}