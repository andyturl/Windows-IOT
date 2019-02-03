using System.Threading.Tasks;

namespace Slack2Display.Server.Hubs
{
    public interface ICommandClient
    {
        Task ReceiveCommand(ICommandModel command);
    }
}
