using System;

namespace Slack2Display.Client.Models
{
    public sealed class MessageModel
    {
        public string UserName { get; set; }

        public string Text { get; set; }

        public string CommandType { get; set; }

        public override string ToString()
        {
            return "CommandType: " + CommandType + Environment.NewLine +
                "Text: " + Text + Environment.NewLine +
                "UserName: " + UserName;
        }
    }
}
