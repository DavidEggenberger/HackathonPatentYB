using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime SentTime { get; set; }
        public string Content { get; set; }
    }
}
