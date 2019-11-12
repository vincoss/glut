using System;
using Android.App;
using Android.Util;


namespace HandlingCrashesSample
{
    public abstract class ActivityBase : Activity
    {
        ProgressDialog _progress;
        protected EventHandler<EventArgs> _initializedEventHandler;

        protected override void OnResume()
        {
            base.OnResume();

            Log.Debug(this.GetType().Name, "ActivityBase.OnCreate.");

            if (!App.Current.IsInitialized)
            {
                Log.Debug(this.GetType().Name, "ActivityBase.App is NOT initialized");

                // if we're to restart on the main activty, and this isn't it, do so
                if (App.Current.RestartMainActivityOnCrash && (this.GetType() != typeof(MainActivity)))
                {
                    Log.Debug(this.GetType().Name, "ActivityBase.Not MainActivity, heading home");
                    this.RestartApp();
                    return;
                }

                // show the loading overlay on the UI thread
                _progress = ProgressDialog.Show(this, "Loading", "Please Wait...", true);

                // when the app has initialized, hide the progress bar and call Finished Initialzing
                App.Current.Initialized += Progress;
            }
            else
            {
                Log.Debug(this.GetType().Name, "ActivityBase.App is initialized");
            }
        }

        private void Progress(object sender, EventArgs e)
        {
            // call finished initializing so that any derived activities have a chance to do work
            RunOnUiThread(() =>
            {
                this.FinishedInitializing();
                // hide the progress bar
                if (_progress != null)
                    _progress.Dismiss();
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

        protected void RestartApp()
        {
            Log.Debug(this.GetType().Name, "ActivityBase.RestartApp");
            // start our first activity
            StartActivity(typeof(MainActivity));
            // kill this activity
            base.Finish();
        }

        /// <summary>
        /// Override this method to perform tasks after the app class has fully initialized
        /// </summary>
        protected abstract void FinishedInitializing();
    }
}