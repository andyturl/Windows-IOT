using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slack2Display.Server.Models;
using Slack2Display.Server.Services;

namespace Slack2Display.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public ICommandService Service { get; }

        public MessageController(ICommandService service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Command(
            string token,
            string team_id,
            string channel_id,
            string channel_name,
            string user_id,
            string user_name,
            string command,
            string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                ICommandModel commandModel;
                if (text.StartsWith("fblikes", StringComparison.OrdinalIgnoreCase))
                {
                    commandModel = new FbLikesCommandModel();
                }
                else
                {
                    commandModel = new MessageCommandModel()
                    {
                        Text = text,
                        UserName = user_name
                    };
                }
                await Service.AddCommandAsync(commandModel);
            }

            // add to command queue
            // raise an event
            return "OK";
        }

    }
}
