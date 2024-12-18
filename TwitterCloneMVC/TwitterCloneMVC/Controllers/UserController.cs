using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Models;
using TwitterClone.Services;
using TwitterCloneMVC.Models;

namespace TwitterClone.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly TweetService _tweetService;
        private readonly FollowService _followService;

        public UserController()
        {
            _userService = new UserService();
            _tweetService = new TweetService();
            _followService = new FollowService();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                var user = _userService.ValidateUser(username, password);
                if (user != null)
                {
                    // Store user information in session
                    Session["User"] = user; // Store user object in session
                    ViewBag.Message = "Login successful!";
                    return RedirectToAction("Dashboard"); // Redirect to dashboard after login
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            try
            {
                // Ensure that the user is logged in
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "User"); // Redirect to login if not logged in
                }

                // Get the logged-in user from the session
                var loggedInUser = (User)Session["User"];

                // Fetch the user feed (assuming you have a method in TweetService to fetch the feed)
                var userFeed = _tweetService.GetUserFeed(loggedInUser.UserId);
                if (userFeed != null && userFeed.Count > 0)
                {
                    return View(userFeed);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }


        // GET: User/Search
        public ActionResult Search(string username)
        {
            try
            {
                var loginUser = (User)Session["User"];
                var users = _userService.SearchUser(username);
                var mod = users.Where(x => x.UserId != loginUser.UserId).ToList();

                if (mod != null && mod.Count > 0)
                {
                    List<UserDTO> usersList = new List<UserDTO>();
                    foreach (var user in users)
                    {
                        //if (user.UserId == loginUser.UserId)
                        //{
                        //    ViewBag.ErrorMessage = "You are looking for yourself in the wrong place!";
                        //    return View("Error");
                        //}
                        bool IsFollowing = _followService.IsFollowing(loginUser.UserId, user.UserId);
                        usersList.Add(new UserDTO()
                        {
                            Fullname = user.Fullname,
                            UserName = user.UserName,
                            Pic = user.Pic,
                            IsFollowing = IsFollowing,
                            UserId = user.UserId
                        });

                    }
                    return View(usersList);
                }
                else
                {
                    ViewBag.ErrorMessage = "User Not Found!";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }



        // GET: User/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: User/Add
        [HttpPost]
        public ActionResult Add(User user)
        {
            try
            {
                user.UserId = "U" + new Random().Next(1000, 9999);
                user.JoiningDate = DateTime.Now;
                bool isAdded = _userService.AddUser(user);
                if (isAdded)
                {
                    _userService.NotifyRegistration(user.Email);
                    ViewBag.Message = "User added successfully!";
                    return RedirectToAction("Login"); // Redirect to login after adding user
                }
                else
                {
                    ViewBag.ErrorMessage = "Error adding user.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // POST: User/Delete
        [HttpPost]
        public ActionResult Delete(string userId)
        {
            try
            {
                bool isDeleted = _userService.DeleteUser(userId);
                if (isDeleted)
                {
                    ViewBag.Message = "User deleted successfully!";
                    return RedirectToAction("Index", "Home"); // Redirect after deletion
                }
                else
                {
                    ViewBag.ErrorMessage = "Error deleting user.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        public ActionResult UserProfile()
        {
            try
            {
                var user = (User)Session["User"];
                if (user == null)
                    return View("Login");
                else
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }
        public ActionResult EditProfile()
        {
            try
            {
                var user = (User)Session["User"];
                if (user == null)
                    return View("Login");
                else
                    return View(user);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public ActionResult ProfilePic(HttpPostedFileBase file)
        {
            try
            {
                UserController userController = new UserController();

                string picUrl = userController.UploadPic(file);
                Session["Pic"] = picUrl;
                return RedirectToAction("EditProfile","User");
            }
            catch (Exception)
            {

                return View("Error");
            }
            
        }

        // POST: User/Edit
        [HttpPost]
        public ActionResult Edit(User user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var picUrl = (string)Session["Pic"];
                    if(picUrl != null)
                    {
                        user.Pic = picUrl;
                    }
                    bool isEdited = _userService.EditUser(user);
                    if (isEdited)
                    {
                        Session["User"] = user;
                        ViewBag.Message = "User profile updated successfully!";
                        return RedirectToAction("UserProfile");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error updating user profile.";
                        return View("Error");
                    }
                }
                ViewBag.ErrorMessage = "All fields should be filled before Saving the Changes.";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }

        }

        public ActionResult ViewProfile(string username)
        {
            var users = _userService.SearchUser(username);
            var viewuser = new User();
            foreach (var user in users)
            {
                if (user.UserName == username)
                {
                    viewuser = user;
                }
            }
            return View(viewuser);
        }

        // Logout Action to clear session when user logs out
        public ActionResult Logout()
        {
            // Clear session data when logging out
            Session.Clear();
            return RedirectToAction("Login");
        }

        private string UploadPic(HttpPostedFileBase file)
        {
            PicUpload model = new PicUpload();
            //var extensions = new string[] { ".jpeg", ".jpg", ".png", ".gif" };
            //var fileExtension = Path.GetExtension(file.FileName).ToLower();

            //if (!Array.Exists(extensions, x => x == fileExtension))
            //{
            //    throw new Exception("Invalid File Type Uploaded.");
            //}
            if (file.ContentLength > 4 * 1024 * 1024)
            {
                throw new Exception("Size Exceeded.");
            }

            if (file != null)
            {
                var Extension = Path.GetExtension(file.FileName);
                var fileName = "my-file-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                string path = Path.Combine(Server.MapPath("~/Assets/Uploads"), fileName);
                
                model.FileUrl = Url.Content(Path.Combine("~/Assets/Uploads/", fileName));
                model.FileName = fileName;

                file.SaveAs(path);
                return path;
            }

            throw new Exception("No file uploaded.");


            
        }
    }

}
