using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterClone.DataAccess;
using TwitterClone.Entities;
using TwitterClone.Repositories;

namespace TwitterClone.Services
{
    public class FollowService : IFollowing
    {
        TwitterContext context;
        public FollowService()
        {
            context = new TwitterContext();
        }
        public void Add(string userId, string followingId)
        {
            if (!IsFollowing(userId, followingId))
            {
                Random random = new Random();
                var follower = new Following {Id = random.Next(1000,9999), FollowingId = followingId, UserId = userId };
                context.Followings.Add(follower);
                context.SaveChanges();
            }
        }

        public void Delete(string userId, string followingId)
        {
            var follower = context.Followings.FirstOrDefault(x => x.UserId == userId && x.FollowingId == followingId);
            if (follower != null)
            {
                context.Followings.Remove(follower);
                context.SaveChanges();
            }
        }

        public List<User> GetFollowers(string userId)
        {
            var followers = context.Followings.Where(x => x.UserId == userId).Select(x => x.Follower).ToList();
            return followers;
        }

        public List<User> GetFollowingUsers(string followingId)
        {
            var followingUsers = context.Followings.Where(x => x.FollowingId == followingId).Select(x => x.User).ToList();
            return followingUsers;
        }

        public bool IsFollowing(string userId, string followingId)
        {
            return context.Followings.Any(x => x.UserId == userId && x.FollowingId == followingId);
        }
    }
}