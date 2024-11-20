using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PacketSniffer
{
    class RawSocketSniffer
    {
        public void Sniff()
        {
            // Set up a raw socket to listen for HTTP packets (on the local machine)
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080)); // Bind to localhost and port 8080

            Console.WriteLine("Eavesdropping on port 8080...");
            byte[] buffer = new byte[4096]; // Buffer to capture data

            while (true)
            {
                int bytesRead = socket.Receive(buffer);
                string capturedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Print captured data (this could be any HTTP traffic)
                Console.WriteLine("Captured Data: ");
                Console.WriteLine(capturedData);
            }
        }
    }
}
