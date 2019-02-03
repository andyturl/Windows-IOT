using System;
using System.Threading.Tasks;

namespace Slack2Display.Server.Services
{
    public interface ICommandService
    {
        void AddCommand(ICommandModel command);
        Task AddCommandAsync(ICommandModel command);

        event EventHandler<CommandEventArgs> CommandAdded;
    }
}