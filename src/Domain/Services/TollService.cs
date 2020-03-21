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

        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            Guard.CheckForNull(date, vehicle);
            Guard.ValidateDates(date);
            if (date.IsTollFreeDate() || vehicle.GetVehicleType().IsTollFreeVehicle()) return 0;

            return _tollFeeRepository.GetTollFee(date);
        }

        public List<(DateTime, int)> CalculateFeeForDates(DateTime[] dates, IVehicle vehicle)
        {
            Guard.CheckForNull(dates, vehicle);
            Guard.ValidateDates(dates);
            var timeFeeValues = new List<(DateTime, int)>();
            foreach (var time in dates)
            {
                var fee = GetTollFee(time, vehicle);
                timeFeeValues.Add((time, fee));
            }
            return timeFeeValues;
        }

        public int CalculateTotalTollFee(List<(DateTime date, int value)> timeFeeValues)
        {

            Guard.CheckForNull(timeFeeValues);
            Guard.ValidateDates(timeFeeValues.Select(s => s.date).ToArray());
            var fee = 0;
            var highestFees = new List<(DateTime time, int fee)>();
            for (int i = 0; i < timeFeeValues.Count; i++)
            {
                fee = 0;
                highestFees.Add(GetHighestFeePreviousHour(timeFeeValues, i));
                highestFees.Add(GetHighestFeeNextHour(timeFeeValues, i));
                fee += GetTotalTollFee(highestFees);
                if (fee >= 60)
                {
                    fee = 60;
                    break;
                }
            }
            return fee;
        }

        public (DateTime, int) GetHighestFeePreviousHour(List<(DateTime date, int value)> timeFeeValues, int index)
        {
            var highestFee = new List<(DateTime date, int fee)>();
            for (int i = index; i >= 0; i--)
            {
                if (DateTimeExtension.TimeIntervalLessThanAnHour(timeFeeValues[i].date, timeFeeValues[index].date))
                {
                    highestFee.Add(timeFeeValues[i]);
                }
                break;
            }
            return highestFee.OrderByDescending(s => s.fee).ThenBy(s => s.date).First();
        }

        public (DateTime, int) GetHighestFeeNextHour(List<(DateTime date, int value)> timeFeeValues, int index)
        {
            var highestFee = new List<(DateTime date, int fee)>();
            for (int i = index; i < timeFeeValues.Count; i++)
            {
                if (DateTimeExtension.TimeIntervalLessThanAnHour(timeFeeValues[i].date, timeFeeValues[index].date))
                {
                    highestFee.Add(timeFeeValues[i]);
                }
                break;
            }
            return highestFee.OrderByDescending(s => s.fee).ThenBy(s => s.date).First();
        }

        public int GetTotalTollFee(List<(DateTime time, int fee)> timeFeeValues)
        {
            var highestEveryHour = new List<(DateTime time, int fee)> { timeFeeValues.First() };
            var firstEveryHourPair = timeFeeValues.First();
            foreach (var pair in timeFeeValues)
            {
                if (!DateTimeExtension.TimeIntervalLessThanAnHour(pair.time, firstEveryHourPair.time))
                {
                    highestEveryHour.Add(pair);
                    firstEveryHourPair = pair;
                }
                if (firstEveryHourPair.fee < pair.fee)
                {
                    highestEveryHour.Remove(firstEveryHourPair);
                    highestEveryHour.Add(pair);
                    firstEveryHourPair = pair;
                }
            }
            return highestEveryHour.Sum(s => s.fee);
        }
    }
}