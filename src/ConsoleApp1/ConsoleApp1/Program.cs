using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = new DateTime(2019, 9, 24, 4, 0, 0);

            var series = new List<Sample>();
            series.Add(new Sample() { Timestamp = date.AddMilliseconds(3), Value = 1 });
            series.Add(new Sample() { Timestamp = date, Value = 1 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(1), Value = 1 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(3), Value = 1 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(14), Value = 2 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(5), Value = 4 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(3), Value = 9 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(7), Value = 6 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(15), Value = 8 });
            series.Add(new Sample() { Timestamp = date.AddSeconds(61), Value = 3 });

            var groups = series.GroupBy(x =>
            {
                var stamp = x.Timestamp;
                stamp = stamp.AddSeconds(-(stamp.Second % 1));
                stamp = stamp.AddMilliseconds(-stamp.Millisecond - 1000 * stamp.Second);
                return stamp;
            })
            .Select(g => new { TimeStamp = g.Key, Value = g.Sum(s => s.Value) })
            .ToList();

            var grouped = from s in series
                          orderby s.Timestamp
                          group s by new DateTime(s.Timestamp.Year, s.Timestamp.Month,
                                s.Timestamp.Day, s.Timestamp.Hour, s.Timestamp.Minute, s.Timestamp.Second).Second into g
                          select new { TimeStamp = g.Key, Value = g.Sum(s => s.Value) };

            foreach (var item in groups)
            {
                Console.WriteLine($"{item.TimeStamp}-{item.Value}");
            }

            Console.WriteLine("Next...");

            foreach (var item in grouped)
            {
                Console.WriteLine($"{item.TimeStamp}-{item.Value}");
            }

            Console.WriteLine("Done...");
            Console.ReadLine();
        }
    }

    public class Sample
    {
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }
    }

}
