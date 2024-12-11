using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Filters;
using TwitterClone.Models;
using TwitterClone.Business;
using TwitterClone.DataAccess;

namespace TwitterClone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private TwitterCloneDbContext db = new TwitterCloneDbContext();
        [HttpGet]
        public ActionResult Index(User User)
        {
            //TweetsDomain td = new TweetsDomain();
            var viewModel = new UserTweetModel
            {
                TweetsList = db.Tweet.ToList(),
                Tweet = new Tweet(),
                User = new User()

            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult UpdateTweet(UserTweetModel domainObject)
        {
            var user = User.Identity.Name;
            domainObject.Tweet.UserId = user;
            domainObject.Tweet.Created = DateTime.Now;
            var viewModel = new UserTweetModel
            {
                TweetsList = domainObject.TweetsList,
                Tweet = domainObject.Tweet,
                User = domainObject.User

            };

            TweetsDomain td = new TweetsDomain();
            td.SetMessage(domainObject.Tweet);
            viewModel.TweetsList = db.Tweet.ToList();
            return RedirectToAction("Index","Home");
        }


        public ActionResult NewTweet(UserTweetModel domainObject)
        {
            var user = User.Identity.Name;
            domainObject.Tweet.UserId = user;
            domainObject.Tweet.Created = DateTime.Now;
            var viewModel = new UserTweetModel
            {
                TweetsList = domainObject.TweetsList,
                Tweet = domainObject.Tweet,
                User = domainObject.User

            };

            TweetsDomain td = new TweetsDomain();
            td.SetMessage(domainObject.Tweet);
            return View(viewModel);
        }
    }
}