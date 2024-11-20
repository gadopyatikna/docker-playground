using PacketDotNet;
using SharpPcap;

namespace PacketSniffer
{
    class DeviceSniffer
    {
        public void Sniff()
        {
            Console.WriteLine("Starting to eavesdrop on HTTP traffic...");

            var dev = CaptureDeviceList.Instance.FirstOrDefault(x => x.Name == "lo0");
            if (dev == null)
                throw new Exception("Device not acquired");

            try
            {
                dev.OnPacketArrival += new PacketArrivalEventHandler(Device_OnPacketArrival);
                dev.Open(DeviceModes.Promiscuous);
                dev.Filter = "tcp port 8080";
                dev.StartCapture();

                // Console.WriteLine($"{dev.Name} capturing");
                // dev.OnCaptureStopped += (object sender, CaptureStoppedEventStatus status) =>
                //     Console.WriteLine($"Stop capturing {dev.Name}");

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{dev.Name} threw {e.Message}");
            }
            finally
            {
                dev.StopCapture();
                dev.Close();
            }
        }

        private static void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            var packet = Packet.ParsePacket(e.GetPacket().LinkLayerType, e.GetPacket().Data);

            // Display basic packet info
            Console.WriteLine($"Packet captured at {e.GetPacket().Timeval.Date}: {e.GetPacket().Data.Length} bytes");

            if (packet.PayloadPacket is IPPacket ipPacket)
            {
                Console.WriteLine($"IP packet: {ipPacket.SourceAddress} -> {ipPacket.DestinationAddress}");

                if (ipPacket.PayloadPacket is TcpPacket tcpPacket)
                {
                    Console.WriteLine($"TCP packet: {tcpPacket.SourcePort} -> {tcpPacket.DestinationPort}");
                    Console.WriteLine($"Payload: {System.Text.Encoding.UTF8.GetString(tcpPacket.PayloadData)}");
                }
            }
        }
    }
}
