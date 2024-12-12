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
    [RoutePrefix("Tweet")]
    public class TweetController : ApiController
    {
        ITweet rep;
        public TweetController()
        {
            rep = new TweetService();
        }
        [HttpPost, Route("PostTweet")]
        public IHttpActionResult CreateTweet([FromBody] Tweet tweet)
        {
            try
            {
                tweet.Created = DateTime.Now;
                rep.Add(tweet);
                return Ok(tweet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut, Route("EditTweet")]
        public IHttpActionResult EditTweet([FromBody] Tweet tweet)
        {
            try
            {
                tweet.Created = DateTime.Now;
                rep.Edit(tweet);
                return Ok(tweet);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete, Route("DeleteTweet/{id}")]
        public IHttpActionResult DeleteTweet(int id)
        {
            try
            {
                rep.Delete(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet,Route("{userId}/tweets")]
        public IHttpActionResult GetUserTweet(string userId)
        {
            try
            {
                var tweets = rep.GetAllTweets(userId);
                return Ok(tweets);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("{userId}/feed")]
        public IHttpActionResult GetUserFeed(string userId)
        {
            try
            {
                var tweets = rep.GetAllFeed(userId);
                return Ok(tweets);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
