using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TwitterClone.Entities
{
    public class Like
    {
        public int LikeId { get; set; }
        public User UserNavigation { get; set; }

        [ForeignKey("UserNavigation")]
        public string UserId { get; set; }

        public Tweet TweetNavigation { get; set; }

        [ForeignKey("TweetNavigation")]
        public int TweetId { get; set; }
    }
}