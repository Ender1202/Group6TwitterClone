using System;
using System.Linq;
using System.Web.Mvc;
using Twitter.Models;
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
        public ActionResult UserTweets()
        {
            try
            {
                // Ensure that the user is logged in
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "User"); // Redirect to login if not logged in
                }
                var user = (User)Session["User"];
                var tweets = _tweetService.GetAllTweets(user.UserId);
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
                // Ensure that the user is logged in
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "User"); // Redirect to login if not logged in
                }

                var tweets = _tweetService.GetUserFeed(userId);
                if (tweets != null && tweets.Count > 0)
                {
                    return View(tweets);
                }
                else
                {
                    ViewBag.ErrorMessage = $"No feed found for {userId}.";
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
            // Ensure that the user is logged in before creating a tweet
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            return View();
        }

        // POST: Tweet/Create
        [HttpPost]
        public ActionResult Create(Tweet tweet)
        {
            try
            {
                // Ensure that the user is logged in before creating a tweet
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "User"); // Redirect to login if not logged in
                }

                // Get the logged-in user from the session
                var loggedInUser = (User)Session["User"];
                tweet.UserId = loggedInUser.UserId; // Assign logged-in user’s ID to the tweet

                bool isCreated = _tweetService.CreateTweet(tweet);
                if (isCreated)
                {
                    ViewBag.Message = "Tweet posted successfully!";
                    return RedirectToAction("Dashboard","User", new { userId = tweet.UserId });
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
            // Ensure that the user is logged in
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            // Fetch the tweet by ID (for editing purpose) from the service or database
            var tweet = _tweetService.GetTweet(id); // You may need to implement GetTweetById in TweetService
            if (tweet == null)
            {
                ViewBag.ErrorMessage = "Tweet not found.";
                return View("Error");
            }

            return View(tweet);
        }

        // POST: Tweet/Edit
        [HttpPost]
        public ActionResult Edit(Tweet tweet)
        {
            try
            {
                // Ensure that the user is logged in
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "User"); // Redirect to login if not logged in
                }

                // Get the logged-in user from the session and ensure they are editing their own tweet
                var loggedInUser = (User)Session["User"];
                if (tweet.UserId != loggedInUser.UserId)
                {
                    ViewBag.ErrorMessage = "You can only edit your own tweets.";
                    return View("Error");
                }

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
                // Ensure that the user is logged in
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "User"); // Redirect to login if not logged in
                }

                // Get the logged-in user from the session and ensure they are deleting their own tweet
                var loggedInUser = (User)Session["User"];
                if (userId != loggedInUser.UserId)
                {
                    ViewBag.ErrorMessage = "You can only delete your own tweets.";
                    return View("Error");
                }

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
