using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace ProgressDialogSample
{
    [Activity(Label = "ProgressDialogSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ProgressDialog _progress; 
        private EventHandler<EventArgs> _initializedEventHandler;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (App.Current.IsInitialized == false)
            {
                _progress = ProgressDialog.Show(this, "Loading", "Please Wait...", true);

                App.Current.Initialized += OnProgress;
            } 
        }

        private void OnProgress(object sender, EventArgs e)
        {
            // when the app has initialized, hide the progress bar and call Finished Initialzing

            // call finished initializing so that any derived activities have a chance to do work
            RunOnUiThread(() =>
            {
                // hide the progress bar
                if (_progress != null)
                {
                    _progress.Dismiss();
                }
            });
        }

        protected override void OnPause()
        {
            base.OnPause();

            // in the case of rotation before the app is fully intialized, we have
            // to remove our intialized event handler, and dismiss the progres. otherwise
            // we'll get multiple Initialized handler calls and a window leak.
            
            if (this._initializedEventHandler != null)
            {
                App.Current.Initialized -= _initializedEventHandler;
            }

            if (_progress != null)
            {
                _progress.Dismiss();
            }
        }
    }

    /// <summary>
    /// Singleton class for Application wide objects. 
    /// </summary>
    public class App
    {
        // declarations
        public event EventHandler<EventArgs> Initialized = delegate { };
        protected readonly string logTag = "!!!!!!! App";

        // properties
        public bool IsInitialized { get; set; }

        public static App Current
        {
            get { return current; }
        } private static App current;

        static App()
        {
            current = new App();
        }
        protected App()
        {
            // any work here is likely to be blocking (static constructors run on whatever thread that first 
            // access its instance members, which in our case is an activity doing an initialization check),
            // so we want to do it on a background thread
            new Task(() =>
            {

                // add a little wait time, to illustrate a loading event
                // TODO: Replace this with real work in your app, such as starting services,
                // database init, web calls, etc.
                Thread.Sleep(2500);

                // set our initialization flag so we know that we're all setup
                this.IsInitialized = true;
                // raise our intialized event
                this.Initialized(this, new EventArgs());
                Log.Debug(logTag, "App initialized, setting Initialized = true");
            }).Start();
        }
    }
}

