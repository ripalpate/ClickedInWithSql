using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Data
{
    public class UserRepository
    {
        static List<User> _users = new List<User>();
        public User AddUser(string name, DateTime releaseDate, int age, bool isPrisioner)
        {
            var newUser = new User(name, releaseDate, age, isPrisioner);

            newUser.Id = _users.Count + 1;

            _users.Add(newUser);

            return newUser;
        }

    }
}
