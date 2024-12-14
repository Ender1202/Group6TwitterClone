using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.Repositories
{
    internal interface ILike
    {
        void AddLike(string userId, int tweetId);
        void RemoveLike(string userId, int tweetId);
        bool IsTweetLikedByUser(string userId, int tweetId);
        int GetLikeCount(int tweetId);
    }
}
