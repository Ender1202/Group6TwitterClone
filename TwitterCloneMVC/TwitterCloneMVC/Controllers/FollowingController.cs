using System;
using System.Web.Mvc;
using Twitter.Models;
using TwitterClone.Services;

namespace TwitterClone.Controllers
{
    public class FollowingController : Controller
    {
        private readonly FollowService _followService;
        private readonly UserService _userService;

        public FollowingController()
        {
            _followService = new FollowService();
            _userService = new UserService();
        }

        // GET: Follow/Followers
        public ActionResult Followers(string userId)
        {
            try
            {
                var followers = _followService.GetFollowers(userId);
                if (followers != null && followers.Count > 0)
                {
                    return View(followers);
                }
                else
                {
                    ViewBag.Message = "No followers found.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Follow/Following
        public ActionResult FollowingUsers(string userId)
        {
            try
            {
                var following = _followService.GetFollowingUsers(userId);
                if (following != null && following.Count > 0)
                {
                    return View(following);
                }
                else
                {
                    ViewBag.Message = "Not Following Anyone.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Follow/FollowUser
        public ActionResult FollowUser(string userId, string followerId)
        {
            try
            {
                bool isFollowed = _followService.FollowUser(userId, followerId);
                if (isFollowed)
                {
                    ViewBag.Message = "Successfully followed!";
                    return RedirectToAction("Dashboard", "User");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error following user.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Follow/UnfollowUser
        public ActionResult UnfollowUser(string userId, string followingId)
        {
            try
            {
                bool isUnfollowed = _followService.UnfollowUser(userId, followingId);
                if (isUnfollowed)
                {
                    ViewBag.Message = "Successfully unfollowed!";
                    return RedirectToAction("Dashboard","User");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error unfollowing user.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Follow/FollowersCount
        public ActionResult FollowersCount(string userId)
        {
            try
            {
                int count = _followService.GetFollowersCount(userId);
                return View(count);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Follow/FollowingCount
        public ActionResult FollowingCount(string userId)
        {
            try
            {
                int count = _followService.GetFollowingCount(userId);
                return View(count);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Follow/IsFollowing
        public ActionResult IsFollowing(string userId, string followingId)
        {
            try
            {
                bool isFollowing = _followService.IsFollowing(userId, followingId);
                ViewBag.IsFollowing = isFollowing;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}
