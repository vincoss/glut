using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DatabindingSample.Services
{
    // TODO: make this end to end service with SQLite 
    public class DataService
    {
        public void CreateSite(SiteDto site)
        {
            // Validation use fluent validation
            // .OverridePropertyName("TheControlId")
        }
    }

    public class SiteDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}