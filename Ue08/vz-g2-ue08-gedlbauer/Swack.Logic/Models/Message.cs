using System;
using System.Collections.Generic;
using System.Text;

namespace Swack.Logic.Models
{
    public class Message
    {
        public Channel Channel { get; set; }

        public User User { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
