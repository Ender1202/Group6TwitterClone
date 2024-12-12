using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Twitter.Entities;
using Twitter.Services;

namespace Twitter.Controllers
{
    public class FollowingController : Controller
    {
        private readonly FollowingService _followingService;

        // Constructor: Initialize FollowingService
        public FollowingController()
        {
            _followingService = new FollowingService(); // Instantiate the service
        }

        // GET: Following
        public async Task<ActionResult> Index(string userId)
        {
            var followings = await _followingService.GetAllFollowingsAsync(userId); // Use the service to get all followings for a user
            if (followings != null)
            {
                return View(followings);
            }
            return View("Error"); // Return error view if request fails
        }

        // GET: Following/Details/5
        public async Task<ActionResult> Details(string userId, string followingId)
        {
            var following = await _followingService.GetFollowingByIdAsync(userId, followingId); // Use the service to get a specific following
            if (following != null)
            {
                return View(following);
            }
            return HttpNotFound(); // Return HTTP Not Found if following not found
        }

        // GET: Following/Create
        public ActionResult Create()
        {
            return View(); // Return view to create a new following
        }

        // POST: Following/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Following following)
        {
            if (ModelState.IsValid)
            {
                var success = await _followingService.CreateFollowingAsync(following); // Use the service to create a following
                if (success)
                {
                    return RedirectToAction("Index"); // Redirect to index view after successful creation
                }

                ModelState.AddModelError("", "Error creating following"); // Add error if API request fails
            }

            return View(following); // Return view with validation errors
        }

        // GET: Following/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var following = await _followingService.GetFollowingByIdAsync(id.ToString(), null); // Use the service to get following for confirmation
            if (following != null)
            {
                return View(following);
            }
            return HttpNotFound(); // Return HTTP Not Found if following not found
        }

        // POST: Following/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var success = await _followingService.DeleteFollowingAsync(id); // Use the service to delete the following
            if (success)
            {
                return RedirectToAction("Index"); // Redirect to index view after successful deletion
            }

            return View("Error"); // Return error view if deletion fails
        }
    }
}
