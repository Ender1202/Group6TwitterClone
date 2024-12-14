using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterClone.DataAccess;
using TwitterClone.Entities;
using TwitterClone.Repositories;

namespace TwitterClone.Services
{
    public class RetweetService : IRetweet
    {
        TwitterContext context;
        public RetweetService()
        {
            context = new TwitterContext();
        }
        public Retweet Retweet(string userId, int tweetId) //used in TweetController
        {
            var original = context.Tweets.FirstOrDefault(x => x.TweetId == tweetId);
            if (original != null)
            {
                var retweet = new Retweet
                {
                    OriginalTweetId = original.TweetId,
                    UserId = userId,
                    Content = original.Message,
                    Created = DateTime.Now
                };
                context.Retweets.Add(retweet);
                context.SaveChanges();
                return retweet;
            }
            else
            {
                return null;
            }
        }
    }
}