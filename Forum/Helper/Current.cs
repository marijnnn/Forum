using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum
{
    public static class Current
    {
        private static Account account;
        public static Account Account
        {
            get
            {
                if (account == null && HttpContext.Current.Session["AccountID"] != null)
                {
                    return Account.GetAccount(Convert.ToInt32(HttpContext.Current.Session["AccountID"]));
                }
                else
                {
                    return account;
                }
            }
            set
            {
                HttpContext.Current.Session["AccountID"] = value.Id;
                account = value;
            }
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

        public static bool IsLoggedIn
        {
            get
            {
                return Right >= Right.User;
            }
        }

        public static void Logout()
        {
            account = null;
            HttpContext.Current.Session["AccountID"] = null;
        }
    }
}