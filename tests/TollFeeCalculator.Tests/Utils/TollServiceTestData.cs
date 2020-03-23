using System;
using System.Collections;
using System.Collections.Generic;

namespace TollFeeCalculator.Tests.Utils
{
    public class TollServiceTestData : IEnumerable<object[]>
    {
        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            var car = MockedModels.Car;
            yield return new object[] { new DateTime(2020, 3, 3, 06, 00, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 06, 20, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 06, 29, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 06, 30, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 06, 40, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 06, 59, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 07, 00, 0), car, 18 };
            yield return new object[] { new DateTime(2020, 3, 3, 07, 20, 0), car, 18 };
            yield return new object[] { new DateTime(2020, 3, 3, 07, 59, 0), car, 18 };
            yield return new object[] { new DateTime(2020, 3, 3, 08, 00, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 08, 15, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 08, 29, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 08, 30, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 12, 20, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 14, 59, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 15, 00, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 15, 10, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 15, 29, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 15, 30, 0), car, 18 };
            yield return new object[] { new DateTime(2020, 3, 3, 16, 00, 0), car, 18 };
            yield return new object[] { new DateTime(2020, 3, 3, 16, 59, 0), car, 18 };
            yield return new object[] { new DateTime(2020, 3, 3, 17, 00, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 17, 20, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 17, 59, 0), car, 13 };
            yield return new object[] { new DateTime(2020, 3, 3, 18, 00, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 18, 20, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 18, 29, 0), car, 8 };
            yield return new object[] { new DateTime(2020, 3, 3, 18, 30, 0), car, 0 };
            yield return new object[] { new DateTime(2020, 3, 3, 20, 00, 0), car, 0 };
            yield return new object[] { new DateTime(2020, 3, 3, 23, 20, 0), car, 0 };
            yield return new object[] { new DateTime(2020, 3, 3, 01, 40, 0), car, 0 };
            yield return new object[] { new DateTime(2020, 3, 3, 04, 50, 0), car, 0 };
            yield return new object[] { new DateTime(2020, 3, 3, 05, 59, 0), car, 0 };
        }

        public IEnumerator GetEnumerator() => GetEnumerator();
    }

    public class TotalTollFeeTestData : IEnumerable<object[]>
    {
        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            var car = MockedModels.Car;
            var dates = new DateTime[]
            {
                new DateTime(2020,3,3,06,00,0),
                new DateTime(2020,3,3,06,20,0),
                new DateTime(2020,3,3,07,37,0),
                new DateTime(2020,3,3,16,15,0),
                new DateTime(2020,3,3,02,00,0)
            };
            var dates2 = new DateTime[]
            {
                new DateTime(2020,3,3,06,00,0),
                new DateTime(2020,3,3,06,31,0),
                new DateTime(2020,3,3,07,30,0),
                new DateTime(2020,3,3,08,15,0)
            };
            yield return new object[] { dates, car, 44 };
            yield return new object[] { dates2, car, 26 };
        }

        public IEnumerator GetEnumerator() => GetEnumerator();
    }

    public class CalculateTotalTollFeeTestData : IEnumerable<object[]>
    {
        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            var times1 = new List<(DateTime, int)>
            {
                (new DateTime(2020,3,3,08,00,0), 10),
                (new DateTime(2020,3,3,09,00,0), 5),
                (new DateTime(2020,3,3,09,30,0), 10),
                (new DateTime(2020,3,3,10,00,0), 15)
            };
            var times2 = new List<(DateTime, int)>
            {
                (new DateTime(2020,3,3,08,30,0), 10),
                (new DateTime(2020,3,3,09,00,0), 5),
                (new DateTime(2020,3,3,10,00,0), 8),
                (new DateTime(2020,3,3,11,00,0), 10),
                (new DateTime(2020,3,3,14,15,0), 10)
            };
            var times3 = new List<(DateTime, int)>
            {
                (new DateTime(2020,3,3,08,30,0), 20),
                (new DateTime(2020,3,3,09,00,0), 8),
                (new DateTime(2020,3,3,14,00,0), 8),
                (new DateTime(2020,3,3,14,15,0), 10)
            };
            var times4 = new List<(DateTime, int)>
            {
                (new DateTime(2020,3,3,08,30,0), 15),
                (new DateTime(2020,3,3,08,35,0), 10),
                (new DateTime(2020,3,3,08,40,0), 10),
                (new DateTime(2020,3,3,08,50,0), 10),
                (new DateTime(2020,3,3,08,56,0), 10),
                (new DateTime(2020,3,3,08,59,0), 10),
                (new DateTime(2020,3,3,09,00,0), 8),
                (new DateTime(2020,3,3,09,20,0), 8),
                (new DateTime(2020,3,3,09,31,0), 10),
                (new DateTime(2020,3,3,09,40,0), 8),
                (new DateTime(2020,3,3,10,00,0), 10)
            };
            yield return new object[] { times1, 25 };
            yield return new object[] { times2, 30 };
            yield return new object[] { times3, 30 };
            yield return new object[] { times4, 25 };
        }

        public IEnumerator GetEnumerator() => GetEnumerator();
    }
}
