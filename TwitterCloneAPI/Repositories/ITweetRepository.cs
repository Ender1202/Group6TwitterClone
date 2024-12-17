using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterClone.Entities;

namespace TwitterClone.Repositories
{
    internal interface ITweet
    {
        void Add(Tweet tweet);
        void Edit(Tweet tweet);
        void Delete(int tid);
        List<Tweet> GetAllTweets(string userId);
        List<Tweet> GetAllFeed(string userId);
        int CountTweets(string userId);

        Tweet GetTweet(int tid);
    }
}