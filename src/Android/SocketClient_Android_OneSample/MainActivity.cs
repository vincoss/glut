using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Java.IO;
using System.Threading.Tasks;
using SocketClient_Android_OneSample;

namespace Socket_Samples
{
    // NOTE: Run this sample both devices Android and PC must be on same network. Possible port needs to be open but that will be requested by server.

    [Activity(Label = "Socket_Samples", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private TextView _textView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            _textView = FindViewById<TextView>(Resource.Id.textView1);

            button.Click += button_Click;
        }

        private async void button_Click(object sender, EventArgs e)
        {
            await NetCodeRun();
        }

        private Task JavaCodeRun()
        {
            // NOTE: To call sockets needs to be async code.

            var task = Task.Factory.StartNew(() =>
            {
                try
                {
                    new JavaSocketClient().Run();
                }
                catch(Exception e)
                {
                    System.Console.WriteLine(e);
                }
            });

            return task;
        }

        private Task NetCodeRun()
        {
            // NOTE: To call sockets needs to be async code.

            var task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var client = new NetSocketClient();
                    client.LogAction = (value) =>
                        {
                            this.RunOnUiThread(() =>
                                {
                                    _textView.Text = string.Format("{0}{1}{2}", _textView.Text, System.Environment.NewLine, value);
                                });
                        };
                    client.Run();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e);
                }
            });

            return task;
        }
    }
}