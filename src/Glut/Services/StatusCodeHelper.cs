﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace Glut.Services
{
    public class StatusCodeHelper
    {
        private static IList<StatusCodeHelper> Cache = new List<StatusCodeHelper>();
        public const string TotalRequests = "#ADD8E6";
        public const string Information = "#00FFFF";
        public const string Successful = "#008000";
        public const string Redirection = "#808080";
        public const string ClientError = "#FFA500";
        public const string ServerError = "#FF0000";

        static StatusCodeHelper()
        {
            Cache.Add(new StatusCodeHelper { From = 100, To = 199, Colour = Information });
            Cache.Add(new StatusCodeHelper { From = 200, To = 299, Colour = Successful });
            Cache.Add(new StatusCodeHelper { From = 300, To = 399, Colour = Redirection });
            Cache.Add(new StatusCodeHelper { From = 400, To = 499, Colour = ClientError });
            Cache.Add(new StatusCodeHelper { From = 500, To = 599, Colour = ServerError });
        }

        public override string ToString()
        {
            return $"{ShortStatusCode}-{From}-{To}-{Colour}";
        }

        public static string GetColour(int statusCode)
        {
            if (statusCode <= 0)
            {
                throw new ArgumentException(nameof(statusCode));
            }
            var item = Cache.SingleOrDefault(x => statusCode >= x.From && statusCode <= x.To || statusCode == x.ShortStatusCode);
            if (item == null)
            {
                return "#000000";
            }
            return item.Colour;
        }

        public static string GetStatusCodeString(int code)
        {
            switch (code)
            {
                case 1:
                    return "Information";
                case 2:
                    return "Successful";
                case 3:
                    return "Redirection";
                case 4:
                    return "Client Error";
                case 5:
                    return "Server Error";
                default:
                    return code.ToString();
            }
        }

        public int ShortStatusCode
        {
            get { return From / 100; }
        }

        public int From { get; set; }

        public int To { get; set; }

        public string Colour { get; set; }
    }
}
