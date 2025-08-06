using Microsoft.AspNetCore.Mvc;
using static WorldCountriesCatalogue.Api.Messages.ApiMessages;

namespace WorldCountriesCatalogue.Api.Controllers
{
    [Route("api")]
    [ApiController]

    public class RootController : ControllerBase
    {
        [HttpGet]
        public StringMessage Root()
        {
            return new StringMessage(Message: "Server is Running.");
        }

        [HttpGet("ping")]
        public StringMessage Ping()
        {
            return new StringMessage(Message: "pong");
        }

    }
}
