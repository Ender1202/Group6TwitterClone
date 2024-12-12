using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Twitter.Entities;
using Twitter.Services;

namespace Twitter.Controllers
{
    public class TweetController : Controller
    {
        private readonly TweetService _tweetService;

        // Constructor: Initialize TweetService
        public TweetController()
        {
            _tweetService = new TweetService(); // Instantiate the service
        }

        // GET: Tweet
        public async Task<ActionResult> Index()
        {
            var tweets = await _tweetService.GetAllTweetsAsync(); // Use the service to get all tweets
            if (tweets != null)
            {
                return View(tweets);
            }
            return View("Error"); // Return error view if request fails
        }

        // GET: Tweet/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var tweet = await _tweetService.GetTweetByIdAsync(id); // Use the service to get a tweet by ID
            if (tweet != null)
            {
                return View(tweet);
            }
            return HttpNotFound(); // Return HTTP Not Found if tweet not found
        }

        // GET: Tweet/Create
        public ActionResult Create()
        {
            return View(); // Return view to create a new tweet
        }

        // POST: Tweet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                var success = await _tweetService.CreateTweetAsync(tweet); // Use the service to create a tweet
                if (success)
                {
                    return RedirectToAction("Index"); // Redirect to index view after successful creation
                }

                ModelState.AddModelError("", "Error creating tweet"); // Add error if API request fails
            }

            return View(tweet); // Return view with validation errors
        }

        // GET: Tweet/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var tweet = await _tweetService.GetTweetByIdAsync(id); // Use the service to get the tweet for editing
            if (tweet != null)
            {
                return View(tweet);
            }
            return HttpNotFound(); // Return HTTP Not Found if tweet not found
        }

        // POST: Tweet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                var success = await _tweetService.UpdateTweetAsync(tweet); // Use the service to update the tweet
                if (success)
                {
                    return RedirectToAction("Index"); // Redirect to index view after successful update
                }

                ModelState.AddModelError("", "Error updating tweet"); // Add error if API request fails
            }

            return View(tweet); // Return view with validation errors
        }

        // GET: Tweet/Delete/5
        public async Task<ActionResult> Delete(int tweetId)
        {
            
            var tweet = await _tweetService.GetTweetByIdAsync(tweetId); // Use the service to get tweet for confirmation
            if (tweet != null)
            {
                return View(tweet);
            }
            return HttpNotFound(); // Return HTTP Not Found if tweet not found
        }

        // POST: Tweet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int tweetId)
        {
            
            var success = await _tweetService.DeleteTweetAsync(tweetId); // Use the service to delete the tweet
            if (success)
            {
                return RedirectToAction("Index"); // Redirect to index view after successful deletion
            }

            return View("Error"); // Return error view if deletion fails
        }
    }
}
