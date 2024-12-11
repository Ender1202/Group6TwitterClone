using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterClone.DataAccess;
using TwitterClone.Models;

namespace TwitterClone.Business
{
    public class TweetsDomain
    {
        private TwitterCloneDbContext db = new TwitterCloneDbContext();
        public TweetsDomain()
        {

        }
        public void SetTweetId() { }
        public int GetTweetId()
        {
            return 0;
        }
        public void SetUser(User User) { }

        public User GetUser()
        {
            return new User();
        }
        public void SetMessage(Tweet obj)
        {
            db.Tweet.Add(obj);
            db.SaveChanges();
        }
        public string GetMessage()
        {
            return string.Empty;
        }
        public void SetCreated(DateTime createdate)
        {

        }
        public DateTime GetCreated()
        {
            return DateTime.Now;
        }

    }
}