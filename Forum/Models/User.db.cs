using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
    public partial class User
    {
        public static User rowToUser(DataRow row)
        {
            return new User(Convert.ToInt32(row["USER_ID"]), row["USER_NAME"].ToString(), row["USER_PASSWORD"].ToString(), (Right)Enum.Parse(typeof(Right), row["USER_RIGHT"].ToString()));
        }

        public static User GetUser(int id)
        {
            foreach (DataRow row in Database.GetData("SELECT * FROM USERS").Rows)
            {
                return rowToUser(row);
            }

            return null;
        }

        public static User GetUser(string username)
        {
            foreach (DataRow row in Database.GetData("SELECT * FROM USERS WHERE USER_NAME = @name", new Dictionary<string, object>()
            {
                {"@name", username}
            }).Rows)
            {
                return rowToUser(row);
            }

            return null;
        }

        public static void AddUser(User user)
        {
            int id = Database.GetSequence("SEQ_USERS");
            Database.Execute("INSERT INTO USERS (USER_ID, USER_NAME, USER_PASSWORD) VALUES (@id, @name, @password)", new Dictionary<string, object>()
            {
                {"@id", id},
                {"@name", user.Username},
                {"@password", user.Password}
            });
            user.Id = id;
        }
    }
}