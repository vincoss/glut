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

namespace Android.Framework
{
    public interface IApplicationContext
    {
        
    }

    public class ApplicationContext : IApplicationContext
    {
        private static IApplicationContext _instance;

        public static IApplicationContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationContext();
                }
                return _instance;
            }
        }
    }
}