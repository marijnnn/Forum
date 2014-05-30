using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
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
            get;
            set;
        }
    }
}