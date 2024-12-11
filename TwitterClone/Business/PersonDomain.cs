using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterClone.Models;

namespace TwitterClone.Business
{
    public class UserDomain
    {
        public void SetUserId()
        {

        }
        public string GetUserId()
        {
            return string.Empty;
        }
        public void SetFullName()
        {

        }
        public string GetFullName()
        {
            return string.Empty;
        }
        public string GetEmail()
        {
            return string.Empty;
        }
        public void SetEmail(string email)
        {

        }
        public void SetJoined(DateTime JoinedDate)
        {

        }

        public DateTime GetJoined()
        {
            return DateTime.Now;
        }
        public void SetActive(Boolean value)
        {

        }
        public bool GetActive()
        {
            return false;
        }
        public void SetFollowing()
        {
        }
        public List<User> GetFollowing()
        {
            return new List<User>();
        }
        public List<User> GetFollowers()
        {
            return new List<User>();
        }
        public void SetFollowers(List<User> User)
        {

        }
        public void SetTweets(List<Tweet> tweets)
        {

        }
        public List<Tweet> GetTweets(List<Tweet> tweets)
        {
            return new List<Tweet>();
        }
    }
}