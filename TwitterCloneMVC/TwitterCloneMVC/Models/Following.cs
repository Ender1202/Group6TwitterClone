using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class Following
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Follower")]
        public string FollowingId { get; set; }
        //Navigation Property
        public User User { get; set; }
        //Navigation Property
        public User Follower { get; set; }
    }
}