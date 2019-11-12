using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionsHandlingSample
{
    /// <summary>
    /// http://androidapi.xamarin.com/?link=E%3aAndroid.Runtime.AndroidEnvironment.UnhandledExceptionRaiser
    /// </summary>
    [Activity(Label = "ExceptionsHandlingSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public MainActivity()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            Java.Lang.Thread.CurrentThread().UncaughtExceptionHandler = new JavaErrorCatcher();
        }

        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            //Emulate sending our exception to remote statistics server. 
            for (int i = 0; i < 100000; i++)
            {
                //Console.Write(i + " ");
            }
            Console.WriteLine("AndroidEnvironment_UnhandledExceptionRaiser: Exception handling completed.");
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Emulate sending our exception to remote statistics server. 
            for (int i = 0; i < 100000; i++)
            {
                //Console.Write(i + " ");
            }
            Console.WriteLine("CurrentDomain_UnhandledException: Exception handling completed.");
        }

        class JavaErrorCatcher : Java.Lang.Object, Java.Lang.Thread.IUncaughtExceptionHandler
        {

            public void UncaughtException(Java.Lang.Thread thread, Java.Lang.Throwable ex)
            {
                for (int i = 0; i < 100000; i++)
                {
                   // Console.Write(i + " ");
                }
                Console.WriteLine("Java: Exception handling completed.");
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ForegroundButton.Click += ForegroundButton_Click;
            BackgroundButton.Click += BackgroundButton_Click;
        }

        #region UI controls implementation

        private void ForegroundButton_Click(object sender, EventArgs e)
        {
            int i = 0;
            int result = 10 / i;
        }

        private void BackgroundButton_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(unused =>
                {
                    int i = 0;
                    int result = 10 / i;
                });
        }

        private Button _foregroundButton;
        public Button ForegroundButton
        {
            get { return _foregroundButton ?? (_foregroundButton = FindViewById<Button>(Resource.Id.ButtonForeground)); }
        }

        private Button _backgroundButton;
        public Button BackgroundButton
        {
            get { return _backgroundButton ?? (_backgroundButton = FindViewById<Button>(Resource.Id.ButtonBackground)); }
        } 

        #endregion
    }
}