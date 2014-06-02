using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class Account
    {
        public int Id
        {
            get;
            private set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public Right Right
        {
            get;
            set;
        }

        public Account(int id, string username, string password, Right right)
            : this(username, password, right)
        {
            this.Id = id;
        }

        public Account(string username, string password, Right right = Right.User)
        {
            this.Username = username;
            this.Password = password;
            this.Right = right;
        }

        public static bool Login(string username, string password)
        {
            Account account = Account.GetAccount(username);

            if (account != null && account.Password == password)
            {
                Current.Account = account;
                return true;
            }

            return false;
        }

        public static bool Register(Account account)
        {
            if (Account.GetAccount(account.Username) != null)
            {
                return false;
            } 

            Account.AddAccount(account);

            return true;
        }
    }
}