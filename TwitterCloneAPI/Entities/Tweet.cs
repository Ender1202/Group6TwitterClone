﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TwitterClone.Entities
{
    public class Tweet
    {
        [Key]
        public int TweetId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        //[Required(ErrorMessage = "Message cannot be empty.")]
        //[StringLength(100, ErrorMessage = "Tweet message cannot exceed 100 characters.")]
        public string Message { get; set; }

        //[DisplayName("Posted on")]
        //[Required(ErrorMessage = "Creation date is required.")]
        public DateTime Created { get; set; }

        //Navigation Properties
        public User User { get; set; } 
    }
}