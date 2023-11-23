﻿using AaaS.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AaaS.Api.Dtos.Telemetry
{
    public class CounterInsertDto
    {
        [NotInFuture]
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public Guid CreatorId { get; set; }
    }
}
