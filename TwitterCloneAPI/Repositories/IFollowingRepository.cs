using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Entities;

namespace TwitterClone.Repositories
{
    internal interface IFollowing
    {
        void Add(string userId, string followingId);
        List<User> GetFollowers(string userId);
        int GetFollowersCount(string userId);
        List<User> GetFollowingUsers(string followingId);
        int GetFollowingUsersCount(string userId);
        void Delete(string userId, string followingId);
        bool IsFollowing(string userId,string followingId);
    }
}
