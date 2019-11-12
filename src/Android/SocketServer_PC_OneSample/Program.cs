using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketServer_PC_OneSample
{
    // NOTE: Run this on the PC. Ensure that right address is selected.

    class Program
    {
        static void Main(string[] args)
        {
            new ServerOne().Start();

            Console.WriteLine("Waiting....");
            Console.Read();
        }
    }

    public class ServerOne
    {
        public void Start()
        {
            var permission = new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, "", SocketPermission.AllPorts);
            var sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var host = Dns.GetHostName();
            var ip = Dns.GetHostEntry(host);

            foreach (var i in ip.AddressList)
            {
                Console.WriteLine(i);
            }

            IPAddress ipAddress = Dns.GetHostEntry(host).AddressList[4];

            int port = 1800;
            Console.WriteLine("{0} - {1} - {2}", host, ipAddress, port);

            var ipEndPoint = new IPEndPoint(ipAddress, port);

            sListener.Bind(ipEndPoint);
            sListener.Listen(10); //Place a socket in a listening state and specify how many client sockets could connect to it:

            AsyncCallback aCallback = new AsyncCallback(OnBeginAcceptCompleted);
            sListener.BeginAccept(aCallback, sListener);

        }

        private void OnBeginAcceptCompleted(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket serverSocket = listener.EndAccept(ar);

            StateObject stateObject = new StateObject(1000, serverSocket);

            // this call passes the StateObject because it 
            // needs to pass the buffer as well as the socket
            IAsyncResult asyncReceive = serverSocket.BeginReceive(
                stateObject.sBuffer,
                0,
                stateObject.sBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(OnBeginReceiveCompleted),
                stateObject);

            Console.Write("Receiving data.");
        }

        private void OnBeginReceiveCompleted(IAsyncResult asyncReceive)
        {
            StateObject stateObject =
              (StateObject)asyncReceive.AsyncState;
            int bytesReceived =
              stateObject.sSocket.EndReceive(asyncReceive);

            Console.WriteLine(
              ".{0} bytes received: {1}",
              bytesReceived.ToString(),
              Encoding.ASCII.GetString(stateObject.sBuffer));

            byte[] sendBuffer =
              Encoding.ASCII.GetBytes("Goodbye");
            IAsyncResult asyncSend =
              stateObject.sSocket.BeginSend(
                sendBuffer,
                0,
                sendBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(sendCallback),
                stateObject.sSocket);

            Console.Write("Sending response.");
        }

        public static void sendCallback(IAsyncResult asyncSend)
        {
            Socket serverSocket = (Socket)asyncSend.AsyncState;
            int bytesSent = serverSocket.EndSend(asyncSend);
            Console.WriteLine(
              ".{0} bytes sent.{1}{1}Shutting down.",
              bytesSent.ToString(),
              Environment.NewLine);

            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }

        // used to pass state information to delegate
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
