﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure.Inbox
{
    public class InboxMessage
    {
        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public InboxMessage(DateTime occurredOn, string type, string data)
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = occurredOn;
            this.Type = type;
            this.Data = data;
        }

        private InboxMessage()
        {
        }
    }
}
