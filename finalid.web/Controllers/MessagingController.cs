using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FinalId.Web.Components;
using StackExchange.Redis;

namespace FinalId.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagingController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis");
        
        public MessagingController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        
        // http://localhost:5000/Messaging/123
        [HttpGet("{recipient}")]
        public IEnumerable<Message> GetAllMessages(string recipient)
        {
            IDatabase db = redis.GetDatabase();

            var message = db.StringGet(recipient);

            if (message != RedisValue.Null)
            {
                return new List<Message>() { JsonConvert.DeserializeObject<Message>(db.StringGet(recipient)) };
            }
            else
            {
                return new List<Message>();
            }
        }

        // http://localhost:5000/Messaging/123
        [HttpPost("{recipient}")]
        public void SendMessage(string recipient, Message message)
        {
            IDatabase db = redis.GetDatabase();
            db.StringSet(recipient, message.ToString());
        }
    }
}
