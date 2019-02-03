namespace Slack2Display.Server.Models
{
    public class MessageCommandModel : ICommandModel
    {
        public string UserName { get; set; }
        public string Text { get; set; }

        public string CommandType => "Message";
    }
}
