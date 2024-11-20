namespace PacketSniffer;

public class Program
{
    public static void Main(string[] args)
    {
        var t = args.FirstOrDefault();
        if (t == null || t == "RawSocket")
            new RawSocketSniffer().Sniff();
        else
            new DeviceSniffer().Sniff();
    }
}