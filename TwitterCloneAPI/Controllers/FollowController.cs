using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwitterClone.Entities;
using TwitterClone.Repositories;
using TwitterClone.Services;

namespace TwitterClone.Controllers
{
    [RoutePrefix("Follow")]
    public class FollowController : ApiController
    {
        IFollowing followrep;
        IUser userrep;
        public FollowController()
        {
            followrep = new FollowService();
            userrep = new UserService();
        }
        [HttpPost, Route("follow/{userId}/{followerId}")]
        public IHttpActionResult Follow(string userId, string followerId)
        {
            if (userrep.GetById(userId) != null && userrep.GetById(followerId) != null)
            {
                followrep.Add(userId, followerId);
                return Ok("Followed");
            }
            else
                return Ok("Error");
        }

        [HttpDelete, Route("unfollow/{userId}/{followingId}")]
        public IHttpActionResult UnFollow(string userId, string followingId)
        {
            if (userrep.GetById(userId) != null || userrep.GetById(userId) != null)
            {
                followrep.Delete(userId, followingId);
                return Ok("Unfollowed!");
            }
            else
                return BadRequest();
        }
        [HttpGet, Route("Followers/{userId}")]
        public IHttpActionResult GetFollowers(string userId)
        {
            var followers = followrep.GetFollowers(userId);
            if (followers == null || !followers.Any())
            {
                return Ok("Null");
            }
            return Ok(followers);
        }

        [HttpGet, Route("Following/{userId}")]
        public IHttpActionResult GetFollowing(string userId)
        {
            var following = followrep.GetFollowingUsers(userId);
            if (following == null || !following.Any())
            {
                return Ok("Null");
            }
            return Ok(following);
        }

        [HttpGet, Route("FollowersCount/{userId}")]
        public IHttpActionResult GetFollowersCount(string userId)
        {
            try
            {
                var followers = followrep.GetFollowers(userId).Count();
                return Ok(followers);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("FollowingCount/{userId}")]
        public IHttpActionResult GetFollowingCount(string userId)
        {
            try
            {
                var following = followrep.GetFollowers(userId).Count();
                return Ok(following);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("{followingId}/isFollowing/{userId}")]
        public IHttpActionResult IsFollowing(string userId,string followingId)
        {
            var isfollowing = followrep.IsFollowing(userId,followingId);
            return Ok(new { isfollowing });
        }
    }
}
