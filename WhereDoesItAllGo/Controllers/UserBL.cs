using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereDoesItAllGo.Controllers
{
    public class UserBL
    {
        public int AddUser(string FirstName, string LastName, string Email, string Password, string InitialBalance)
        {
            return new UserDL().Insert(FirstName, LastName, Email, Password, InitialBalance);
        }

        public User GetUser(int userId)
        {
            return new UserDL().GetUser(userId);
        }
    }
}