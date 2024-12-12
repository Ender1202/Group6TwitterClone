using System;
using System.Linq;
using System.Web.Mvc;
using Twitter.Entities;
using TwitterClone.Services;

namespace TwitterClone.Controllers
{
    public class TweetController : Controller
    {
        private readonly TweetService _tweetService;

        public TweetController()
        {
            _tweetService = new TweetService();
        }

        // GET: Tweet/UserTweets
        public ActionResult UserTweets(string userId)
        {
            try
            {
                var tweets = _tweetService.GetAllTweets(userId);
                if (tweets != null)
                {
                    return View(tweets);
                }
                else
                {
                    ViewBag.ErrorMessage = "No tweets found for this user.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Tweet/UserFeed
        public ActionResult UserFeed(string userId)
        {
            try
            {
                var tweets = _tweetService.GetUserFeed(userId);
                if (tweets != null && tweets.Count > 0)
                {
                    return View(tweets);
                }
                else
                {
                    ViewBag.ErrorMessage = "No feed found for this user.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Tweet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tweet/Create
        [HttpPost]
        public ActionResult Create(Tweet tweet)
        {
            try
            {
                bool isCreated = _tweetService.CreateTweet(tweet);
                if (isCreated)
                {
                    ViewBag.Message = "Tweet posted successfully!";
                    return RedirectToAction("UserTweets", new { userId = tweet.UserId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Error posting tweet.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Tweet/Edit
        public ActionResult Edit(int id)
        {
            // Fetch the tweet by ID (for editing purpose) from the service or database
            // (You may need to add a method for fetching individual tweet in your TweetService)
            var tweet = _tweetService.GetAllTweets("userId").OrderByDescending(x=>x.Created);  // Adjust logic accordingly
            return View(tweet);
        }

        // POST: Tweet/Edit
        [HttpPost]
        public ActionResult Edit(Tweet tweet)
        {
            try
            {
                bool isEdited = _tweetService.EditTweet(tweet);
                if (isEdited)
                {
                    ViewBag.Message = "Tweet edited successfully!";
                    return RedirectToAction("UserTweets", new { userId = tweet.UserId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Error editing tweet.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // POST: Tweet/Delete
        [HttpPost]
        public ActionResult Delete(int id, string userId)
        {
            try
            {
                bool isDeleted = _tweetService.DeleteTweet(id);
                if (isDeleted)
                {
                    ViewBag.Message = "Tweet deleted successfully!";
                    return RedirectToAction("UserTweets", new { userId = userId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Error deleting tweet.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}
