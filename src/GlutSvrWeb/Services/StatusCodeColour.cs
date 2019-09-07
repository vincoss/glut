using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Services
{
    public class StatusCodeColour
    {
        private static IList<StatusCodeColour> Cache = new List<StatusCodeColour>();
        public const string TotalRequests = "#ADD8E6";
        public const string Information = "#00FFFF";
        public const string Successful = "#008000";
        public const string Redirection = "#808080";
        public const string ClientError = "#FFA500";
        public const string ServerError = "#FF0000";

        static StatusCodeColour()
        {
            Cache.Add(new StatusCodeColour { From = 100, To = 199, Colour = Information }); 
            Cache.Add(new StatusCodeColour { From = 200, To = 299, Colour = Successful }); 
            Cache.Add(new StatusCodeColour { From = 300, To = 399, Colour = Redirection });
            Cache.Add(new StatusCodeColour { From = 400, To = 499, Colour = ClientError });
            Cache.Add(new StatusCodeColour { From = 500, To = 599, Colour = ServerError });
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
