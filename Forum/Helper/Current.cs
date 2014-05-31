using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public static class Current
    {
        public static User User
        {
            get;
            set;
        }

        public static Right Right
        {
            get
            {
                if (User == null)
                {
                    return Right.Guest;
                }
                else
                {
                    return User.Right;
                }
            }
        }

        public static void Logout()
        {
            User = null;
        }
    }
}