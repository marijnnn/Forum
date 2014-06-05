using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
    public partial class Category
    {
        private static Category rowToCategory(DataRow row)
        {
            return new Category(
                Convert.ToInt32(row["CATEGORY_ID"]), 
                row["CATEGORY_NAME"].ToString(), 
                row["CATEGORY_DESCRIPTION"].ToString(), 
                Convert.ToInt32(row["CATEGORY_ORDERNUMBER"]), 
                Convert.ToInt32(row["CATEGORY_TOPICCOUNT"]), 
                Convert.ToInt32(row["CATEGORY_MESSAGECOUNT"]), 
                row["CATEGORY_LASTMESSAGE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["CATEGORY_LASTMESSAGE_ID"]), 
                (Right)Enum.Parse(typeof(Right), row["CATEGORY_MINIMUMRIGHT"].ToString())
            );
        }

        public static Dictionary<int, List<Category>> GetCategoriesByMainCategories()
        {
            Dictionary<int, List<Category>> categories = new Dictionary<int, List<Category>>();

            foreach (DataRow row in Database.GetData("SELECT * FROM CATEGORY").Rows)
            {
                int maincategory_id = Convert.ToInt32(row["CATEGORY_MAINCATEGORY_ID"]);

                if (!categories.ContainsKey(maincategory_id))
                {
                    categories.Add(maincategory_id, new List<Category>());
                }

                categories[maincategory_id].Add(rowToCategory(row));
            }

            return categories;
        }

        public static Category GetCategory(int id)
        {
            foreach (DataRow row in Database.GetData("SELECT * FROM CATEGORY WHERE CATEGORY_ID = " + id).Rows)
            {
                return rowToCategory(row);
            }

            return null;
        }

        public static List<Category> GetCategoriesByMainCategory(MainCategory maincategory)
        {
            List<Category> categories = new List<Category>();

            foreach (DataRow row in Database.GetData("SELECT * FROM CATEGORY WHERE CATEGORY_MAINCATEGORY_ID = " + maincategory.Id).Rows)
            {
                categories.Add(rowToCategory(row));
            }

            return categories;
        }

        public static void AddCategory(MainCategory maincategory, Category category)
        {
            int id = Database.GetSequence("SEQ_CATEGORY");
            Database.Execute("INSERT INTO CATEGORY (CATEGORY_ID, CATEGORY_NAME, CATEGORY_DESCRIPTION, CATEGORY_ORDERNUMBER, CATEGORY_MINIMUMRIGHT, CATEGORY_MAINCATEGORY_ID) VALUES (:id, :name, :description, :ordernumber, :minimumright, :maincategory_id)", new Dictionary<string, object>()
            {
                {"id", id},
                {"name", category.Name},
                {"description", category.Description},
                {"ordernumber", category.OrderNumber},
                {"minimumright", (int)category.MinimumRight},
                {"maincategory_id", maincategory.Id}
            });
            category.Id = id;
        }

        public static void ChangeCategory(Category category, MainCategory maincategory = null)
        {
            Database.Execute("UPDATE CATEGORY SET CATEGORY_NAME = :name, CATEGORY_DESCRIPTION = :description, CATEGORY_ORDERNUMBER = :ordernumber, CATEGORY_MINIMUMRIGHT = :minimumright WHERE CATEGORY_ID = " + category.Id, new Dictionary<string, object>()
            {
                {"name", category.Name},
                {"description", category.Description},
                {"ordernumber", category.OrderNumber},
                {"minimumright", (int)category.MinimumRight}
            });

            if (maincategory != null)
            {
                Database.Execute("UPDATE CATEGORY SET CATEGORY_MAINCATEGORY_ID = " + maincategory.Id + " WHERE CATEGORY_ID = " + category.Id);
            }
        }

        public static void DeleteCategory(Category category)
        {
            Database.Execute("DELETE FROM CATEGORY WHERE CATEGORY_ID = " + category.Id);
        }

        public static void MarkAsRead(Category category)
        {
            Database.Execute("DELETE FROM CATEGORY_READ WHERE CR_USER_ID = " + Current.AccountId + " AND CR_CATEGORY_ID = " + category.Id);
            Database.Execute("INSERT INTO CATEGORY_READ (CR_USER_ID, CR_CATEGORY_ID, CR_DATE) VALUES (:user_id, :category_id, sysdate)", new Dictionary<string, object>()
            {
                {"user_id", Current.AccountId},
                {"category_id", category.Id}
            });
        }

        public static DateTime GetLastMarkAsRead(Category category)
        {
            DataRow row = Database.GetData("SELECT MAX(CR_DATE) LAST FROM CATEGORY_READ WHERE CR_CATEGORY_ID = " + category.Id + " AND CR_USER_ID = " + Current.AccountId).Rows[0];

            return row["LAST"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LAST"]);
        }

        public static int GetUnreadTopicCount(Category category)
        {
            return GetUnreadTopicCounts(new List<Category>() { category })[category.Id];
        }

        public static Dictionary<int, int> GetUnreadTopicCounts(List<Category> categories)
        {
            List<int> category_ids = categories.Select(a => a.Id).ToList();
            Dictionary<int, int> lijst = new Dictionary<int, int>();

            if (Current.IsLoggedIn && category_ids.Count > 0)
            {
                foreach (DataRow row in Database.GetData("SELECT TOPIC_CATEGORY_ID AS CATEGORY_ID, COUNT(1) AS AANTAL FROM TOPIC JOIN MESSAGE ON MESSAGE_ID = TOPIC_LASTMESSAGE_ID LEFT JOIN TOPIC_READ ON TR_TOPIC_ID = TOPIC_ID AND TR_USER_ID = :truserid LEFT JOIN CATEGORY_READ ON CR_USER_ID = :cruserid AND CR_CATEGORY_ID = TOPIC_CATEGORY_ID WHERE (CR_DATE IS NULL OR CR_DATE < MESSAGE_DATE) AND (TR_DATE IS NULL OR TR_DATE < MESSAGE_DATE) AND TO_DATE(:forumread, 'SYYYY-MM-DD HH24:MI:SS') < MESSAGE_DATE AND TOPIC_CATEGORY_ID IN (SELECT CATEGORY_ID FROM CATEGORY) GROUP by TOPIC_CATEGORY_ID", new Dictionary<string, object>()
                {
                    {"truserid", Current.AccountId},
                    {"cruserid", Current.AccountId},
                    {"forumread", Forum.GetLastMarkAsRead().ToString("yyyy-MM-dd HH:mm:ss")}
                }).Rows)
                {
                    lijst.Add(Convert.ToInt32(row["CATEGORY_ID"]), Convert.ToInt32(row["AANTAL"]));
                }
            }

            foreach (int category_id in category_ids)
            {
                if (!lijst.ContainsKey(category_id))
                {
                    lijst.Add(category_id, 0);
                }
            }

            return lijst;
        }
    }
}