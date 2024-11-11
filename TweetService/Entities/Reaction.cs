using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetService.Entities
{
    public class Reaction
    {
        string Content { get; set; }
        int UserId { get; set; }
        int CommentId { get; set; }
        int TweetId { get; set; }
    }
}
