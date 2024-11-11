using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetService.Entities
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string Hashtags { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
