using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;


namespace LocalizationSample
{
    /// <summary>
    /// This activity shows nothing; instead, it restarts the android process.
    /// </summary>
    [Activity(Label = "Restart Activity", NoHistory = true)]
    public class MagicAppRestart : Activity
    {
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Will close the activity when doen
            Finish();
        }

        protected override void OnResume()
        {
            base.OnResume();

            // NOTE: Start the main activity. possible whe can use here some application context to restart last activity.
            StartActivityForResult(new Intent(this, typeof (MainActivity)), 0);
        }
    }
}