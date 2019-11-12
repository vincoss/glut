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
using Java.Net;
using Java.IO;

namespace SocketClient_Android_OneSample
{
    // http://stackoverflow.com/questions/19291467/how-to-connect-android-to-pc-using-wifi
    // http://stackoverflow.com/questions/10388250/how-to-send-string-from-android-to-pc-over-wifi
    // http://stackoverflow.com/questions/8443245/wifi-connection-via-android

    public class JavaSocketClient
    {
        public void Run()
        {
            Socket socket = new Socket("10.0.0.25", 1800);
            DataOutputStream outputStream = new DataOutputStream(socket.OutputStream);
            outputStream.WriteUTF("Hello World! Java...");
            socket.Close();
        }
    }
}