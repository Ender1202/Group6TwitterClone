using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class UserTweetModel
    {
        public IEnumerable<TwitterClone.Models.Tweet> TweetsList { get; set; }
        public TwitterClone.Models.Tweet Tweet { get; set; }
        public TwitterClone.Models.User User { get; set; }

    }
}