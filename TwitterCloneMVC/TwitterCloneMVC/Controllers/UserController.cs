using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Twitter.Entities;
using Twitter.Services;

namespace Twitter.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        // Constructor: Initialize UserService 
        public UserController()
        {
            _userService = new UserService(); // Instantiate the service
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync(); // Use the service to get all users
            if (users != null)
            {
                return View(users);
            }
            return View("Error"); // Return error view if request fails
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var user = await _userService.GetUserByIdAsync(id); // Use the service to get a user by ID
            if (user != null)
            {
                return View(user);
            }
            return HttpNotFound(); // Return HTTP Not Found if user not found
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View(); // Return view to create a new user
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var success = await _userService.CreateUserAsync(user); // Use the service to create a user
                if (success)
                {
                    return RedirectToAction("Index"); // Redirect to index view after successful creation
                }

                ModelState.AddModelError("", "Error creating user"); // Add error if API request fails
            }

            return View(user); // Return view with validation errors
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userService.GetUserByIdAsync(id); // Use the service to get the user for editing
            if (user != null)
            {
                return View(user);
            }
            return HttpNotFound(); // Return HTTP Not Found if user not found
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var success = await _userService.UpdateUserAsync(user); // Use the service to update the user
                if (success)
                {
                    return RedirectToAction("Index"); // Redirect to index view after successful update
                }

                ModelState.AddModelError("", "Error updating user"); // Add error if API request fails
            }

            return View(user); // Return view with validation errors
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userService.GetUserByIdAsync(id); // Use the service to get user for confirmation
            if (user != null)
            {
                return View(user);
            }
            return HttpNotFound(); // Return HTTP Not Found if user not found
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var success = await _userService.DeleteUserAsync(id); // Use the service to delete the user
            if (success)
            {
                return RedirectToAction("Index"); // Redirect to index view after successful deletion
            }

            return View("Error"); // Return error view if deletion fails
        }
    }
}
