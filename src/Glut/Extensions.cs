using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Glut
{
    public static class Extensions
    {
        // TODO: Not used remove
        public static void PrintConfiguration(this IConfiguration configuration, Action<string> print)
        {
            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (print == null)
            {
                throw new ArgumentNullException(nameof(print));
            }

            foreach (var pair in configuration.AsEnumerable())
            {
                print($"{pair.Key}: {pair.Value}");
            }
        }

        public static void AddRange<T>(this HashSet<T> destination, IEnumerable<T> soure)
        {
            if(destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            if (soure == null)
            {
                throw new ArgumentNullException(nameof(soure));
            }
            foreach (T item in soure)
            {
                destination.Add(item);
            }
        }
    }
}
