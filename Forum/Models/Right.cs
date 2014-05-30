using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public enum Right : int
    {
        Suspended = -1,
        Guest = 0,
        User = 1,
        Moderator = 2,
        Administrator = 3
    }
}