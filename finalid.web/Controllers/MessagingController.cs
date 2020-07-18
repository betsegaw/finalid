using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalId.Web.Components;

namespace FinalId.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagingController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public MessagingController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        
        // http://localhost:5000/Messaging/123
        [HttpGet("{recipient}")]
        public IEnumerable<Message> GetAllMessages(string recipient)
        {
            return new List<Message>() { new Message() { EncryptedContent = "yo" , Signature = "ni" }};
        }

        // http://localhost:5000/Messaging/123
        [HttpPost("{recipient}")]
        public void SendMessage(string recipient, Message message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
