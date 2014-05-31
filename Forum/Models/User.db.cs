using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class User
    {
        public static User GetUser(int id)
        {
            return (User)null;
        }

        public static User GetUser(string username)
        {
            return (User)null;
        }

        public static void AddUser(User user)
        {
            int id = Database.GetSequence("SEQ_USERS");
            Database.Execute("INSERT INTO USERS (USER_ID, USER_NAME, USER_PASSWORD) VALUES (@id, @name, @password)", new Dictionary<string, object>()
            {
                {"@id", id},
                {"@name", user.Username},
                {"@password", user.password}
            });
            user.Id = id;
        }
    }
}