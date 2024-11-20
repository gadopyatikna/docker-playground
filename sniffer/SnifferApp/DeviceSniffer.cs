using PacketDotNet;
using SharpPcap;

namespace PacketSniffer
{
    class DeviceSniffer
    {
        public void Sniff()
        {
            Console.WriteLine("Starting to eavesdrop on HTTP traffic...");

            // Capture traffic on the first network device (you might need to change this)
            foreach (var dev in CaptureDeviceList.Instance)
            {
                try
                {
                    dev.OnPacketArrival += new PacketArrivalEventHandler(Device_OnPacketArrival);
                    dev.Open(DeviceModes.Promiscuous);
                    dev.StartCapture();
                    Console.WriteLine($"{dev.Name} capturing");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{dev.Name} threw {e.Message}");
                }
            }

            Console.ReadLine();  // Keep the program running
        }

        private static void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            var packet = Packet.ParsePacket(e.GetPacket().LinkLayerType, e.GetPacket().Data);

            // Check if the packet contains an HTTP request (basic example)
            if (packet is EthernetPacket ethPacket
                && ethPacket.PayloadPacket is IPPacket ipPacket
                && ipPacket.PayloadPacket is TcpPacket tcpPacket)
            {
                string packetData = System.Text.Encoding.ASCII.GetString(tcpPacket.PayloadData);
                if (packetData.Contains("HTTP"))
                {
                    Console.WriteLine("Captured HTTP Traffic:");
                    Console.WriteLine(packetData); // Prints raw HTTP request/response
                }
            }
        }
    }
}
