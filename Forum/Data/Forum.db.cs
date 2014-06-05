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
            Database.Execute("DELETE FROM FORUM_READ WHERE FR_USER_ID = " + Current.AccountId);
            Database.Execute("INSERT INTO FORUM_READ (FR_USER_ID, FR_DATE) VALUES (:user_id, sysdate)", new Dictionary<string, object>()
            {
                {"user_id", Current.AccountId}
            });
        }

        public static DateTime GetLastMarkAsRead()
        {
            DataRow row = Database.GetData("SELECT MAX(FR_DATE) LAST FROM FORUM_READ WHERE FR_USER_ID = " + Current.AccountId).Rows[0];

            return row["LAST"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LAST"]);
        }
    }
}