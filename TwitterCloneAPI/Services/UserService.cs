using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using TwitterClone.DataAccess;
using TwitterClone.Entities;
using TwitterClone.Repositories;

namespace TwitterClone.Services
{
    public class UserService : IUser
    {

        TwitterContext context;
        public UserService()
        {
            context = new TwitterContext();
        }
        public void Add(User user)
        {
            if (user != null)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            var user = context.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public void Edit(User user)
        {
            var use = context.Users.SingleOrDefault(x => x.UserId == user.UserId);
            if (use != null)
            {
                use.UserId = user.UserId;
                use.JoiningDate = user.JoiningDate;
                use.Fullname = user.Fullname;
                use.Mobile = user.Mobile;
                use.Password = user.Password;
                use.ConfirmPassword = user.ConfirmPassword;
                use.Email = user.Email;
                use.Bio = user.Bio;
                use.Pic = user.Pic;
                context.SaveChanges();
            }
        }

        public List<User> Get(string username)
        {
            var users = context.Users
                           .Where(u => u.UserName.Contains(username)) // Partial match
                           .ToList();
            return users;
        }

        public User GetById(string id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }

        public string UploadPic(string userId, HttpPostedFile profilePicture)
        {
            if (profilePicture == null || profilePicture.ContentLength == 0)
            {
                throw new Exception("No file uploaded.");
            }

            var extensions = new string[] { ".jpeg", ".jpg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(profilePicture.FileName).ToLower();

            if (!Array.Exists(extensions, x => x == fileExtension))
            {
                throw new Exception("Invalid File Type Uploaded.");
            }

            if (profilePicture.ContentLength > 4 * 1024 * 1024)
            {
                throw new Exception("Size Exceeded.");
            }
            var uploadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Uploads");
            var FileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadDirectory, FileName);
            var directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            profilePicture.SaveAs(filePath);

            var user = context.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("User Not Found.");
            }

            user.Pic = "/Assets/Uploads/" + FileName;
            context.SaveChanges();
            return user.Pic;
        }

        public User Validate(string username, string password)
        {
            var user = context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}