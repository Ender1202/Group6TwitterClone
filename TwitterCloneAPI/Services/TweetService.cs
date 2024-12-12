using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterClone.DataAccess;
using TwitterClone.Entities;
using TwitterClone.Repositories;

namespace TwitterClone.Services
{
    public class TweetService : ITweet
    {
        TwitterContext context;
        public TweetService()
        {
            context = new TwitterContext();
        }
        public void Add(Tweet tweet)
        {
            context.Tweets.Add(tweet);
            context.SaveChanges();
        }

        public void Delete(int tid)
        {
            var tweet = context.Tweets.Find(tid);
            context.Tweets.Remove(tweet);
            context.SaveChanges();
        }

        public void Edit(Tweet tweet)
        {
            var et = context.Tweets.SingleOrDefault(x => x.TweetId == tweet.TweetId);
            if (et != null)
            {
                et.Message = tweet.Message;
                context.SaveChanges();
            }
        }

        public List<Tweet> GetAllFeed(string userId)
        {
            var followingId = context.Followings.Where(x => x.FollowingId == userId).Select(x => x.UserId);

            var feed = context.Tweets.Where(x=>followingId.Contains(x.UserId)||x.UserId==userId).OrderByDescending(t=>t.Created).ToList();
            return feed;
        }

        public List<Tweet> GetAllTweets(string userId)
        {
            var tweets = context.Tweets.Where(x => x.UserId == userId).OrderByDescending(t => t.Created).ToList();
            return tweets;
        }
    }
}