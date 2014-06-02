using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public static class Current
    {
        public static Account Account
        {
            get;
            set;
        }

        public static Right Right
        {
            get
            {
                if (Account == null)
                {
                    return Right.Guest;
                }
                else
                {
                    return Account.Right;
                }
            }
        }

        public static void Logout()
        {
            Account = null;
        }
    }
}