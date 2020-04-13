using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebApplication1
{
    [HubName("chat2DeskHub")]
    public class LogHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.Caller.addLog(name, message);
        }
    }
}