using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterCloneMVC.Models
{
    public class UserDTO
    {
        public string Fullname { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Pic { get; set; }
        public bool IsFollowing { get; set; }
    }
}