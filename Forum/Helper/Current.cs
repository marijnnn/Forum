using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum
{
    public static class Current
    {
        public static int AccountId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["AccountID"]);
            }
        }

        public static Account Account
        {
            get
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    return Account.GetAccount(Convert.ToInt32(HttpContext.Current.Session["AccountID"]));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["AccountID"] = ((Account)value).Id;
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
            HttpContext.Current.Session["AccountID"] = null;
        }
    }
}