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
        private readonly IDateService _dateService;
        private readonly ITollFeeRepository _tollFeeRepository;
        public TollService(ITollFeeRepository tollFeeRepository, IDateService dateService)
        {
            _tollFeeRepository = tollFeeRepository;
            _dateService = dateService;
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
            for (int i = 0; i < timeFeeValues.Count; i++)
            {
                var intervalFee = new List<int>();
                for (int j = i; j < timeFeeValues.Count; j++)
                {
                    if (_dateService.TimeIntervalLessThanAnHour(timeFeeValues[j].date, timeFeeValues[i].date))
                    {
                        intervalFee.Add(timeFeeValues[j].value);
                    }
                    else
                    {
                        i = j - 1;
                        break;
                    }
                }
                fee += intervalFee.Max();
                intervalFee.Clear();
                if (fee >= 60)
                {
                    fee = 60;
                    break;
                }
            }
            return fee;
        }
    }
}