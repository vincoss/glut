using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace SocketClient_Android_OneSample
{
    public class NetSocketClient
    {
        public NetSocketClient()
        {
            this.LogAction = Console.WriteLine;
        }

        public void Run()
        {
            IPAddress ipAddress = IPAddress.Parse("10.0.0.25");

            IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 1800);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IAsyncResult asyncConnect = clientSocket.BeginConnect(ipEndpoint, new AsyncCallback(OnBeginConnectCompleted), clientSocket);
        }

        public void OnBeginConnectCompleted(IAsyncResult asyncConnect)
        {
            Socket clientSocket = (Socket)asyncConnect.AsyncState;
            
            clientSocket.EndConnect(asyncConnect);

            // arriving here means the operation completed
            // (asyncConnect.IsCompleted = true) but not
            // necessarily successfully
            if (clientSocket.Connected == false)
            {
                LogAction(".client is not connected.");
                return;
            }
            else
            {
                LogAction(".client is connected.");
            }

            byte[] sendBuffer = Encoding.ASCII.GetBytes("Hello World!!! Net.");

            IAsyncResult asyncSend = clientSocket.BeginSend(
              sendBuffer,
              0,
              sendBuffer.Length,
              SocketFlags.None,
              new AsyncCallback(OnBeginSendCompleted),
              clientSocket);

            LogAction("Sending data.");
        }

        public void OnBeginSendCompleted(IAsyncResult asyncSend)
        {
            Socket clientSocket = (Socket)asyncSend.AsyncState;
            int bytesSent = clientSocket.EndSend(asyncSend);

            LogAction(string.Format(".{0} bytes sent.", bytesSent.ToString()));

            StateObject stateObject = new StateObject(16, clientSocket);

            // this call passes the StateObject because it
            // needs to pass the buffer as well as the socket
            IAsyncResult asyncReceive =
              clientSocket.BeginReceive(
                stateObject.sBuffer,
                0,
                stateObject.sBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(OnBeginReceiveCompleted),
                stateObject);

            LogAction("Receiving response.");
        }

        public void OnBeginReceiveCompleted(IAsyncResult asyncReceive)
        {
            StateObject stateObject = (StateObject)asyncReceive.AsyncState;

            int bytesReceived = stateObject.sSocket.EndReceive(asyncReceive);

            var message = string.Format(".{0} bytes received: {1}{2}{2}Shutting down.",
              bytesReceived.ToString(),
              Encoding.ASCII.GetString(stateObject.sBuffer),
              Environment.NewLine);

            LogAction(message);

            stateObject.sSocket.Shutdown(SocketShutdown.Both);
            stateObject.sSocket.Close();
        }

        public Action<string> LogAction;

        class StateObject
        {
            internal byte[] sBuffer;
            internal Socket sSocket;
            internal StateObject(int size, Socket sock)
            {
                sBuffer = new byte[size];
                sSocket = sock;
            }
        }
    }
}