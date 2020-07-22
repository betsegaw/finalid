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
        const int MaximumMessagePerRecipient = 4;
        const int MessageRetentionInMinutes = 10;
        private readonly ILogger<MessagingController> _logger;
        private ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis.database.finalid");
        
        public MessagingController(ILogger<MessagingController> logger)
        {
            _logger = logger;
        }

        
        // http://localhost:5000/Messaging/123
        [HttpGet("{recipient}")]
        public IEnumerable<Message> GetAllMessages(string recipient)
        {
            IDatabase db = redis.GetDatabase();

            var messages = db.ListRange(recipient).Select(x =>  JsonConvert.DeserializeObject<Message>(x));

            return messages;
        }

        
        // http://localhost:5000/Messaging/123/456
        [HttpGet("{recipient}/{user}")]
        public IEnumerable<Message> GetMessageFromUser(string recipient, string user)
        {
            IDatabase db = redis.GetDatabase();

            var messages = db.ListRange(recipient).Select(x =>  JsonConvert.DeserializeObject<Message>(x));

            return messages.Where(x => x.SenderPublicKey == user);
        }

        // http://localhost:5000/Messaging/123
        [HttpPost("{recipient}")]
        public void SendMessage(string recipient, Message message)
        {
            IDatabase db = redis.GetDatabase();
            
            db.ListRightPush(recipient, message.ToString());
            // Expire key after 10 minutes
            db.KeyExpire(recipient, new TimeSpan(0, MessageRetentionInMinutes, 0));

            // Trim down number of messages to a maximum of 100 latest messages
            var count = db.ListRange(recipient).Count();

            if (count > MaximumMessagePerRecipient)
            {
                db.ListTrim(recipient, count - MaximumMessagePerRecipient, - 1);
            }
        }
    }
}
