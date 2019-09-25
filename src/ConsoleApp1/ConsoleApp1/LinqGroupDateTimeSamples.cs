using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class LinqGroupDateTimeSamples
    {
        private static DateTime _date = new DateTime(2019, 9, 24, 4, 0, 0);

        public static void SampleOne()
        {
            var series = SampleData();

            var groups = series.GroupBy(x =>
            {
                var stamp = x.Timestamp;
                stamp = stamp.AddSeconds(-(stamp.Second % 1));
                stamp = stamp.AddMilliseconds(-stamp.Millisecond - 1000 * stamp.Second);
                return stamp;
            })
          .Select(g => new { TimeStamp = g.Key, Value = g.Sum(s => s.Value) })
          .ToList();

            foreach (var item in groups)
            {
                Console.WriteLine($"{item.TimeStamp}-{item.Value}");
            }
        }

        public static void SampleTwo()
        {
            var series = SampleData();

            var groups = from s in series
                          orderby s.Timestamp
                          group s by new DateTime(s.Timestamp.Year, s.Timestamp.Month,
                                s.Timestamp.Day, s.Timestamp.Hour, s.Timestamp.Minute, s.Timestamp.Second) into g
                          select new { TimeStamp = g.Key, Value = g.Sum(s => s.Value) };

            foreach (var item in groups)
            {
                Console.WriteLine($"{item.TimeStamp}-{item.Value}");
            }
        }

        public static void SampleThree()
        {
            var series = SampleData();

            var groups = from s in series
                          orderby s.Timestamp
                          group s by TimeSpan.FromTicks(s.Timestamp.Ticks / TimeSpan.FromSeconds(1).Ticks) into g
                          select new { TimeStamp = g.Key, Value = g.Sum(s => s.Value) };


            foreach (var item in groups)
            {
                Console.WriteLine($"{item.TimeStamp}-{item.Value}");
            }
        }

        // Group by second
        public static void SampleFour()
        {
            var series = SampleData();

            var min = series.Min(x => x.Timestamp);
            var max = series.Max(x => x.Timestamp);
            var diff = TimeSpan.FromTicks(max.Ticks - min.Ticks);

            Console.WriteLine($"{min}-{max}-diff-{max - min}-{diff.TotalSeconds}");

            var groups = from s in series
                     let res = s.Timestamp - _date
                     orderby s.Timestamp
                     group s by res into g
                     select new { TimeStamp = g.Key, Value = g.Sum(s => s.Value) };

            foreach (var item in groups)
            {
                Console.WriteLine($"Time-{item.TimeStamp}-Seconds-{(int)Math.Round(item.TimeStamp.TotalSeconds)}-Value-{item.Value}");
            }
        }

        private static IEnumerable<Sample> SampleData()
        {
            var series = new List<Sample>();
            series.Add(new Sample() { Timestamp = _date.AddMilliseconds(3), Value = 1 });
            series.Add(new Sample() { Timestamp = _date, Value = 1 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(1), Value = 21 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(3), Value = 1 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(14), Value = 2 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(5), Value = 4 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(3), Value = 9 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(7), Value = 6 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(15), Value = 8 });
            series.Add(new Sample() { Timestamp = _date.AddSeconds(61.5), Value = 3 });

            return series;
        }

        public class Sample
        {
            public DateTime Timestamp { get; set; }
            public int Value { get; set; }
        }
    }
}
