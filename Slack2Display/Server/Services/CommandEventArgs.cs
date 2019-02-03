using System;

namespace Slack2Display.Server.Services
{
    public class CommandEventArgs : EventArgs
    {
        public ICommandModel Command { get; private set; }

        public CommandEventArgs(ICommandModel command)
        {
            Command = command;
        }
    }
}