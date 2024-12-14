using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Entities;

namespace TwitterClone.Repositories
{
    internal interface IRetweet
    {
        Retweet Retweet(string userId, int tweetId);
    }
}
