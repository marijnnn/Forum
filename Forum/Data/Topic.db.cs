using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
    public partial class Topic
    {
        private static Topic rowToTopic(DataRow row)
        {
            return new Topic(
                Convert.ToInt32(row["TOPIC_ID"]),
                row["TOPIC_NAME"].ToString(),
                Convert.ToInt32(row["TOPIC_AUTHOR_ID"]),
                row["TOPIC_LASTMESSAGE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TOPIC_LASTMESSAGE_ID"]),
                Convert.ToInt32(row["TOPIC_CATEGORY_ID"])
            )
            {
                Author = Account.rowToAccount(row)
            };
        }

        private static DataTable getTopicsByWhere(string where = "", Dictionary<string, object> parameters = default(Dictionary<string, object>))
        {
            return Database.GetData("SELECT TOPIC.*, USERS.* FROM TOPIC JOIN USERS ON TOPIC_AUTHOR_ID = USER_ID" + (where != "" ? " WHERE " + where : "") + " ORDER BY TOPIC_LASTMESSAGE_ID DESC", parameters);
        }

        public static Topic GetTopic(int id)
        {
            foreach(DataRow row in getTopicsByWhere("TOPIC_ID = " + id).Rows){
                return rowToTopic(row);
            }

            return null;
        }

        public static List<Topic> GetTopicByCategory(Category category)
        {
            List<Topic> topics = new List<Topic>();

            foreach (DataRow row in getTopicsByWhere("TOPIC_CATEGORY_ID = " + category.Id).Rows)
            {
                topics.Add(rowToTopic(row));
            }

            return topics;
        }

        public static void AddTopic(Category category, Topic topic)
        {
            int id = Database.GetSequence("SEQ_TOPIC");
            Database.Execute("INSERT INTO TOPIC (TOPIC_ID, TOPIC_NAME, TOPIC_AUTHOR_ID, TOPIC_CATEGORY_ID) VALUES (@id, @name, @author_id, @category_id)", new Dictionary<string, object>()
            {
                {"@id", id},
                {"@name", topic.Name},
                {"@author_id", topic.AuthorId},
                {"@category_id", category.Id}
            });
            Database.Execute("UPDATE CATEGORY SET CATEGORY_TOPICCOUNT = CATEGORY_TOPICCOUNT + 1, CATEGORY_MESSAGECOUNT = CATEGORY_MESSAGECOUNT - 1 WHERE CATEGORY_ID = " + category.Id);
            topic.Id = id;
        }

        public static void DeleteTopic(Topic topic)
        {
            Database.Execute("DELETE FROM TOPIC WHERE TOPIC_ID = " + topic.Id);

            // Caching op CATEGORY/TOPIC bijwerken
            Database.Execute("UPDATE CATEGORY SET CATEGORY_TOPICCOUNT = (SELECT COUNT(1) FROM TOPIC WHERE TOPIC_CATEGORY_ID = CATEGORY_ID), CATEGORY_MESSAGECOUNT = (SELECT COUNT(1) FROM MESSAGE WHERE MESSAGE_TOPIC_ID IN (SELECT TOPIC_ID FROM TOPIC WHERE TOPIC_CATEGORY_ID = CATEGORY_ID)) - CATEGORY_TOPICCOUNT, CATEGORY_LASTMESSAGE_ID = (SELECT MAX(MESSAGE_ID) FROM MESSAGE WHERE MESSAGE_TOPIC_ID IN (SELECT TOPIC_ID FROM TOPIC WHERE TOPIC_CATEGORY_ID = CATEGORY_ID)) WHERE CATEGORY_ID = " + topic.CategoryId);
            Database.Execute("UPDATE TOPIC SET TOPIC_LASTMESSAGE_ID = (SELECT MAX(MESSAGE_ID) FROM MESSAGE WHERE MESSAGE_TOPIC_ID = TOPIC_ID) WHERE TOPIC_ID = " + topic.Id);
        }

        public static void MarkAsRead(Topic topic)
        {
            if (Current.IsLoggedIn)
            {
                Database.Execute("DELETE FROM TOPIC_READ WHERE TR_USER_ID = " + Current.Account.Id + " AND TR_TOPIC_ID = " + topic.Id);
                Database.Execute("INSERT INTO TOPIC_READ (TR_USER_ID, TR_TOPIC_ID, TR_DATE) VALUES (@user_id, @topic_id, sysdate)", new Dictionary<string, object>()
                {
                    {"@user_id", Current.Account.Id},
                    {"@topic_id", topic.Id}
                });
            }
        }

        public static DateTime GetLastRead(Topic topic)
        {
            if (!Current.IsLoggedIn)
            {
                return DateTime.MaxValue;
            }

            DataRow row = Database.GetData("SELECT MAX(TR_DATE) LAST FROM TOPIC_READ WHERE TR_TOPIC_ID = " + topic.Id + " AND TR_USER_ID = " + Current.Account.Id).Rows[0];

            return row["LAST"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LAST"]);
        }
    }
}