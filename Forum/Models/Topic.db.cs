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
                Author = User.rowToUser(row)
            };
        }

        private static DataTable getTopicsByWhere(string where = "", Dictionary<string, object> parameters = default(Dictionary<string, object>))
        {
            return Database.GetData("SELECT TOPIC.*, USERS.* FROM TOPIC JOIN USERS ON TOPIC_AUTHOR_ID = USER_ID" + (where != "" ? " WHERE " + where : ""), parameters);
        }

        public static Topic GetTopic(int id)
        {
            foreach(DataRow row in getTopicsByWhere("TOPIC_ID = " + id).Rows){
                return rowToTopic(row);
            }

            return null;
        }

        public static List<Topic> GetTopicByCategory(Category category, int page = 0, int count = 30)
        {
            List<Topic> topics = new List<Topic>();

            foreach (DataRow row in getTopicsByWhere("TOPIC_CATEGORY_ID = " + category.Id).Rows)
            {
                topics.Add(rowToTopic(row));
            }

            return topics;
        }
    }
}