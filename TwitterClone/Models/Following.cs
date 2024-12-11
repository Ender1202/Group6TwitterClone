using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Models
{
    public class Following
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Follower")]
        public string FollowingId { get; set; }

        public User User { get; set; }

        public User Follower { get; set; }
    }
}