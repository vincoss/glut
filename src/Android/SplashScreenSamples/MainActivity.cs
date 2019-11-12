using Android.App;
using Android.OS;
using System.Threading;


namespace SplashScreenSamples
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Simulate a long loading process on app startup.

            Thread.Sleep(2500);

            // Open the activity, like home or other.

            StartActivity(typeof(OneActivity));
        }
    }
}

