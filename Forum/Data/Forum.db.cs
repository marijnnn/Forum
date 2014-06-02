using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
    public static partial class Forum
    {
        public static void MarkAsRead()
        {
            Database.Execute("INSERT INTO FORUM_READ (FR_USER_ID, FR_DATE) VALUES (@user_id, ysdate)", new Dictionary<string, object>()
            {
                {"@user_id", Current.Account.Id}
            });
        }

        public static DateTime GetLastMarkAsRead()
        {
            Database.Execute("DELETE FROM FORUM_READ WHERE FR_USER_ID = " + Current.Account.Id);
            DataRow row = Database.GetData("SELECT MAX(FR_DATE) LAST FROM FORUM_READ WHERE FR_USER_ID = " + Current.Account.Id).Rows[0];

            return row["LAST"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LAST"]);
        }
    }
}