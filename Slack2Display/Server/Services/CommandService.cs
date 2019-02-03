using System;
using System.Threading.Tasks;
using Slack2Display.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Slack2Display.Server.Services
{
    public class CommandService : ICommandService
    {
        private IHubContext<CommandHub, ICommandClient> HubContext { get; }

        public event EventHandler<CommandEventArgs> CommandAdded;

        public CommandService(IHubContext<CommandHub, ICommandClient> hubContext)
        {
            HubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public void AddCommand(ICommandModel command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            HubContext.Clients.All.ReceiveCommand(command);

            if (CommandAdded != null)
            {
                CommandAdded.Invoke(this, new CommandEventArgs(command));
            }
        }

        public async Task AddCommandAsync(ICommandModel command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await HubContext.Clients.All.ReceiveCommand(command);

            if (CommandAdded != null)
            {
                CommandAdded.Invoke(this, new CommandEventArgs(command));
            }
        }
    }
}
