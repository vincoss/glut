using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BluetoothClient_Android_OneSample
{
    [Activity(Label = "BluetoothClient_Android_OneSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private BluetoothHelper _helper = new BluetoothHelper();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var connectButton = FindViewById<Button>(Resource.Id.ConnectBluetoothButton);
            var disconnectButton = FindViewById<Button>(Resource.Id.DisconnectBluetoothButton);
            var sendButton = FindViewById<Button>(Resource.Id.SendButton);
            var outputTextView = FindViewById<TextView>(Resource.Id.OutputTextView);

            _helper.ProgressAction = value => outputTextView.Text = string.Format("{0}{1}{2}", outputTextView.Text, System.Environment.NewLine, value);

            connectButton.Click += connectButton_Click;
            disconnectButton.Click += disconnectButton_Click;
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            _helper.Disconnect();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            _helper.Connect(() =>
                {
                    StartActivityForResult(new Intent(Android.Bluetooth.BluetoothAdapter.ActionRequestEnable), 0);
                });
        }
    }
}

