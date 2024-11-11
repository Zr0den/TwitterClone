using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetService.Entities
{
    public class Comment
    {
        string Content { get; set; }
        int UserId { get; set; }
        int TweetId { get; set; }
    }
}
