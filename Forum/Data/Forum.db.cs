using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public static partial class Forum
    {
        public static void MarkAsRead()
        {
            Database.Execute("INSERT INTO FORUM_READ (FR_USER_ID, FR_DATE) VALUES (@user_id, ysdate)", new Dictionary<string, object>()
            {
                {"@user_id", Current.User.Id}
            });
        }
    }
}