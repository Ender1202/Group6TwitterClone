using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TwitterClone.Entities;

namespace TwitterClone.DataAccess
{
    public class TwitterContext : DbContext
    {
        public TwitterContext() : base("X") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Following> Followings { get; set; }
    }
}