using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Services
{
    public class StatusCodeColour
    {
        private static IList<StatusCodeColour> Cache = new List<StatusCodeColour>();

        static StatusCodeColour()
        {
            //ADD8E6
            Cache.Add(new StatusCodeColour { From = 100, To = 199, Colour = "#00FFFF" });   // Informational
            Cache.Add(new StatusCodeColour { From = 200, To = 299, Colour = "#008000" });   // Successful
            Cache.Add(new StatusCodeColour { From = 300, To = 399, Colour = "#808080" });   // Redirects
            Cache.Add(new StatusCodeColour { From = 400, To = 499, Colour = "#FFA500" });   // Client errors
            Cache.Add(new StatusCodeColour { From = 500, To = 599, Colour = "#FF0000" });   // Server errors
        }

        public int From { get; set; }
        public int To { get; set; }
        public string Colour { get; set; }

        public override string ToString()
        {
            return $"{From}-{To}-{Colour}";
        }

        public static string GetColour(int statusCode)
        {
            var item = Cache.SingleOrDefault(x => statusCode >= x.From && statusCode <= x.To);
            if (item == null)
            {
                return "#000000";
            }
            return item.Colour;
        }
    }
}
