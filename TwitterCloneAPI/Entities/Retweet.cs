using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TwitterClone.Entities
{
    public class Retweet
    {
        public int RetweetId {  get; set; }
        public Tweet Tweet { get; set; }

        [ForeignKey("Tweet")]
        public int OriginalTweetId {  get; set; }
        public User User { get; set; }

        [ForeignKey("User")]
        public string UserId {  get; set; }
        public string Content {  get; set; }
        public DateTime Created { get; set; }
    }
}