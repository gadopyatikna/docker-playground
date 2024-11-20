using System.Net;
using System.Net.NetworkInformation;

namespace RedisCounterApp;

internal class PingCommand
{
    public bool Exec(string host)
    {
        Ping ping = new Ping();
        PingReply pingStatus = ping.Send(host);

        return pingStatus.Status == IPStatus.Success;
    }
}