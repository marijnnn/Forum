﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public partial class User
    {
        public int Id
        {
            get;
            set;
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

        public User(int id, string username, string password, Right right)
            : this(username, password, right)
        {
            this.Id = id;
        }

        public User(string username, string password, Right right)
        {
            this.Username = username;
            this.Password = password;
            this.Right = right;
        }

        public static bool Login(string username, string password)
        {

            return false;
        }

        public static void Register(User user)
        {
            User.AddUser(user);
        }
    }
}