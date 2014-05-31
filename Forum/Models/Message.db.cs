using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class Message
    {
        public static Message GetMessage(int id)
        {
            return (Message)null;
        }

        public static List<Message> GetMessagesByTopic(Topic topic)
        {
            return (List<Message>)null;
        }

        public static List<Message> SearchMessage(string keyword)
        {
            return (List<Message>)null;
        }

        public static void AddMessage(Message message)
        {
            int id = Database.GetSequence("SEQ_MESSAGE");
            Database.Execute("INSERT INTO MESSAGE (MESSAGE_ID, MESSAGE_TEXT, MESSAGE_DATE, MESSAGE_AUTHOR_ID, MESSAGE_TOPIC_ID) VALUES (@id, @text, TO_DATE(@date, 'SYYYY-MM-DD HH24:MI:SS'), @author_id, @topic_id)", new Dictionary<string, object>()
            {
                {"@id", id},
                {"@text", message.Text},
                {"@date", message.Date.ToString("yyyy-MM-dd HH:mm:ss")},
                {"@author_id", message.AuthorId},
                {"@topic_id", message.TopicId}
            });
            Database.Execute("UPDATE TOPIC SET TOPIC_LASTMESSAGE_ID = " + id + " WHERE TOPIC_ID = " + message.TopicId);
            Database.Execute("UPDATE CATEGORY SET CATEGORY_MESSAGECOUNT = CATEGORY_MESSAGECOUNT + 1, CATEGORY_LASTMESSAGE_ID = " + id + " WHERE CATEGORY_ID = " + message.Topic.CategoryId);
            message.Id = id;
        }

        public static void DeleteMessage(Message message)
        {
            Database.Execute("DELETE FROM MESSAGE WHERE MESSAGE_ID = " + message.Id);

            // Caching op CATEGORY/TOPIC bijwerken
            Database.Execute("UPDATE CATEGORY SET CATEGORY_TOPICCOUNT = (SELECT COUNT(1) FROM TOPIC WHERE TOPIC_CATEGORY_ID = CATEGORY_ID), CATEGORY_MESSAGECOUNT = (SELECT COUNT(1) FROM MESSAGE WHERE MESSAGE_TOPIC_ID IN (SELECT TOPIC_ID FROM TOPIC WHERE TOPIC_CATEGORY_ID = CATEGORY_ID)), CATEGORY_LASTMESSAGE_ID = (SELECT MAX(MESSAGE_ID) FROM MESSAGE WHERE MESSAGE_TOPIC_ID IN (SELECT TOPIC_ID FROM TOPIC WHERE TOPIC_CATEGORY_ID = CATEGORY_ID)) WHERE CATEGORY_ID = " + message.Topic.CategoryId);
            Database.Execute("UPDATE TOPIC SET TOPIC_LASTMESSAGE_ID = (SELECT MAX(MESSAGE_ID) FROM MESSAGE WHERE MESSAGE_TOPIC_ID = TOPIC_ID) WHERE TOPIC_ID = " + message.TopicId);
        }
    }
}