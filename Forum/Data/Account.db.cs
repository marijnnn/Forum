using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
    public partial class Account
    {
        public static Account rowToAccount(DataRow row)
        {
            return new Account(Convert.ToInt32(row["USER_ID"]), row["USER_NAME"].ToString(), row["USER_PASSWORD"].ToString(), (Right)Enum.Parse(typeof(Right), row["USER_RIGHT"].ToString()));
        }

        public static Account GetAccount(int id)
        {
            foreach (DataRow row in Database.GetData("SELECT * FROM USERS").Rows)
            {
                return rowToAccount(row);
            }

            return null;
        }

        public static Account GetAccount(string username)
        {
            foreach (DataRow row in Database.GetData("SELECT * FROM USERS WHERE USER_NAME = @name", new Dictionary<string, object>()
            {
                {"@name", username}
            }).Rows)
            {
                return rowToAccount(row);
            }

            return null;
        }

        public static void AddAccount(Account account)
        {
            int id = Database.GetSequence("SEQ_USERS");
            Database.Execute("INSERT INTO USERS (USER_ID, USER_NAME, USER_PASSWORD) VALUES (@id, @name, @password)", new Dictionary<string, object>()
            {
                {"@id", id},
                {"@name", account.Username},
                {"@password", account.Password}
            });
            account.Id = id;
        }
    }
}