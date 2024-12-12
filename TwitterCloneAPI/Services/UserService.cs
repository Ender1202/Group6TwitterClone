using System;
using System.Collections.Generic;
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
                use.Mobile = user.Mobile;
                use.Password = user.Password;
                use.Email = user.Email;
                use.Bio = user.Bio;
                use.Pic = user.Pic;
                context.SaveChanges();
            }
        }

        public User Get(string username)
        {
            return context.Users.FirstOrDefault(x => x.UserName == username);
        }

        public User GetById(string id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
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