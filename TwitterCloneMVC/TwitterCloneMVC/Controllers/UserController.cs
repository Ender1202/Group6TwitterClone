using System;
using System.Web.Mvc;
using Twitter.Entities;
using TwitterClone.Services;

namespace TwitterClone.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        // GET: User/Search
        public ActionResult Search(string username)
        {
            try
            {
                var user = _userService.SearchUser(username);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    ViewBag.ErrorMessage = "User not found.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: User/Login
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
                    ViewBag.Message = "Login successful!";
                    return RedirectToAction("Home"); // Redirect to some dashboard or home page
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
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
                bool isAdded = _userService.AddUser(user);
                if (isAdded)
                {
                    ViewBag.Message = "User added successfully!";
                    return RedirectToAction("Home"); // Redirect to home page after adding
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

        // POST: User/Edit
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                bool isEdited = _userService.EditUser(user);
                if (isEdited)
                {
                    ViewBag.Message = "User profile updated successfully!";
                    return RedirectToAction("Profile", "User", new { username = user.UserName });
                }
                else
                {
                    ViewBag.ErrorMessage = "Error updating user profile.";
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
