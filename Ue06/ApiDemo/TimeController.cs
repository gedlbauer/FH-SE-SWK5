using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo
{
    [ApiController]
    [Route("time2")]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public object Get()
        {
            return new { Time = DateTime.UtcNow.ToString("O") };
        }
    }
}
