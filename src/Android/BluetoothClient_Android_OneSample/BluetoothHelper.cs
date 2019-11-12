using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Bluetooth;
using Android.Content;
using System.IO;
using Java.Util;

namespace BluetoothClient_Android_OneSample
{
    // http://stackoverflow.com/questions/10929767/send-text-through-bluetooth-from-java-server-to-android-client

    public class BluetoothHelper
    {
        public BluetoothHelper()
        {
            this.ProgressAction = Console.WriteLine;
        }

        public void Connect(Action startBluetoothAction)
        {
            if (startBluetoothAction == null)
            {
                throw new ArgumentNullException("startBluetoothAction");
            }

            string AdapterAddress, AdapterName, AdapterBoundDevices = String.Empty;
            var defaultAdapter = BluetoothAdapter.DefaultAdapter;
            Android.Bluetooth.State AdapterState;
            if (defaultAdapter.IsEnabled)
            {
                AdapterAddress = defaultAdapter.Address;
                AdapterName = defaultAdapter.Name;
                var bd = defaultAdapter.BondedDevices;
                foreach (var dev in bd)
                {
                    if (!String.IsNullOrEmpty(AdapterBoundDevices))
                    {
                        AdapterBoundDevices += ",";
                    }
                    AdapterBoundDevices += dev.Name;
                }
                AdapterState = defaultAdapter.State;

                var message = string.Format("AdapterName: {0}, AdapterAddress: {1}, AdapterBoundDevices: {2}, State: {3}", AdapterName, AdapterAddress, AdapterBoundDevices, AdapterState);

                ProgressAction(message);
            }
            else
            {
                ProgressAction("Attempting to connect Bluetooth.");
                startBluetoothAction();
                ProgressAction("Bluetooth connected.");
            }
        }

        public void Disconnect()
        {
            BluetoothAdapter defaultAdapter = BluetoothAdapter.DefaultAdapter;
            if (defaultAdapter.IsEnabled)
            {
                ProgressAction("Bluetooth disconnecting");
                defaultAdapter.Disable();
                ProgressAction("Bluetooth disconnected");
            }
        }

        public Action<string> ProgressAction;



        private BluetoothSocket btSocket = null;
        private Stream outStream = null;

        // Well known SPP UUID
        private static UUID MY_UUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        // Insert your server's MAC address // 18:CF:5E:37:D5-25
        private static String address = "18:CF:5E:37:D5-25";

        private void ConnectToServer()
        {
            var defaultAdapter = BluetoothAdapter.DefaultAdapter;

            // Set up a pointer to the remote node using it's address.
            BluetoothDevice device = defaultAdapter.GetRemoteDevice(address);

            // Two things are needed to make a connection:
            //   A MAC address, which we got above.
            //   A Service ID or UUID.  In this case we are using the
            //     UUID for SPP.
            try
            {
                btSocket = device.CreateRfcommSocketToServiceRecord(MY_UUID);
            }
            catch (IOException e)
            {
                ProgressAction(string.Format("Fatal Error", "In onResume() and socket create failed: {0}", e.Message));
            }

            // Discovery is resource intensive.  Make sure it isn't going on
            // when you attempt to connect and pass your message.
            defaultAdapter.CancelDiscovery();

            // Establish the connection.  This will block until it connects.
            try
            {
                btSocket.Connect();
                ProgressAction("\n...Connection established and data link opened...");
            }
            catch (IOException e)
            {
                try
                {
                    btSocket.Close();
                }
                catch (IOException e2)
                {
                    ProgressAction(string.Format("Fatal Error", "In onResume() and unable to close socket during connection failure. : {0}", e2.Message));
                }
            }

            // Create a data stream so we can talk to server.
            ProgressAction("\n...Sending message to server...");
            string message = "Hello from Android.\n";
            ProgressAction("\n\n...The message that we will send to the server is: " + message);

            try
            {
                outStream = btSocket.OutputStream;
            }
            catch (IOException e)
            {
                ProgressAction(string.Format("Fatal Error", "In onResume() and output stream creation failed: {0}", e.Message));
            }

            byte[] msgBuffer = System.Text.Encoding.ASCII.GetBytes(message);
            try
            {
                outStream.Write(msgBuffer, 0, msgBuffer.Length);
            }
            catch (IOException e)
            {
                string msg = "In onResume() and an exception occurred during write: " + e.Message;
                if (address.Equals("00:00:00:00:00:00"))
                    msg = msg + ".\n\nUpdate your server address from 00:00:00:00:00:00 to the correct address on line 37 in the java code";
                msg = msg + ".\n\nCheck that the SPP UUID: " + MY_UUID.ToString() + " exists on server.\n\n";

                ProgressAction(string.Format("Fatal Error: {0}", msg));
            }
        }
    }
}