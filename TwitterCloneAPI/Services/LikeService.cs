using System;
using System.Linq;
using System.Web;
using TwitterClone.DataAccess;
using TwitterClone.Entities;
using TwitterClone.Repositories;

namespace TwitterClone.Services
{
    public class LikeService : ILike
    {
        TwitterContext context;
        public LikeService()
        {
            context = new TwitterContext();
        }
        public void AddLike(string userId, int tweetId)
        {
            if (!IsTweetLikedByUser(userId, tweetId))
            {
                Random random = new Random(); // creating an object for random class to generate likeId
                var like = new Like
                {
                    LikeId = random.Next(1000, 9999),
                    UserId = userId,
                    TweetId = tweetId
                };
                context.Likes.Add(like);
                context.SaveChanges();
            }
        }

        public int GetLikeCount(int tweetId)
        {
            return context.Likes.Count(x => x.TweetId == tweetId);
        }

        public bool IsTweetLikedByUser(string userId, int tweetId)
        {
            return context.Likes.Any(x => x.UserId == userId && x.TweetId == tweetId);
        }

        public void RemoveLike(string userId, int tweetId)
        {
            var like = context.Likes.FirstOrDefault(x => x.UserId == userId && x.TweetId == tweetId);
            if (like != null) 
            {
                context.Likes.Remove(like);
                context.SaveChanges();
            }
        }
    }
}