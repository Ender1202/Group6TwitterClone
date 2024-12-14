using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwitterClone.Repositories;
using TwitterClone.Services;

namespace TwitterClone.Controllers
{
    [RoutePrefix("Like")]
    public class LikeController : ApiController
    {
        ILike rep;
        public LikeController()
        {
            rep = new LikeService();
        }

        [HttpPost,Route("{userId}/Liked/{tweetId}")]
        public IHttpActionResult AddLike(string userId, int tweetId)
        {
            try
            {
                rep.AddLike(userId, tweetId);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost,Route("{userId}/Unliked/{tweetId}")]
        public IHttpActionResult RemoveLike(string userId, int tweetId)
        {
            try
            {
                rep.RemoveLike(userId, tweetId);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet,Route("CountLikes/{tweetId}")]
        public IHttpActionResult LikesCount(int tweetId) 
        {
            try
            {
                int count = rep.GetLikeCount(tweetId);
                return Ok(count);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet,Route("{tweetId}/IsLikedBy/{userId}")]
        public IHttpActionResult IsLikedByUser(string userId, int tweetId) 
        {
            try
            {
                bool liked = rep.IsTweetLikedByUser(userId, tweetId);
                return Ok(liked);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
