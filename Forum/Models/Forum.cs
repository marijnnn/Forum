using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public static class Forum
    {
        public static List<MainCategory> MainCategories
        {
            get
            {
                return MainCategory.GetMainCategories();
            }
        }

        public static DateTime LastMarkAsRead
        {
            get;
            set;
        }

        public static void MarkAsRead()
        {

        }

        public static void AddMainCategory(MainCategory maincategory)
        {
            MainCategory.AddMainCategory(maincategory);
        }
    }
}