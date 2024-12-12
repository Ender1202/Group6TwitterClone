using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Entities;

namespace TwitterClone.Repositories
{
    internal interface IUser
    {
        void Add(User user);
        void Edit(User user);
        void Delete(string id);
        User Get(string username);
        User GetById(string id);
        User Validate(string username, string password);
    }
}
