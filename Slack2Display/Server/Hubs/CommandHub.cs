using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Slack2Display.Server.Hubs
{
    public class CommandHub : Hub<ICommandClient>
    {
         public async Task SendCommand(ICommandModel command)
         {
             await Clients.All.ReceiveCommand(command);
         }
    }
}
