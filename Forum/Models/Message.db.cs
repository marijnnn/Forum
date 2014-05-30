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

        public static List<Message> GetMessagesByTopic(Topic topic, int page, int count = 10)
        {
            return (List<Message>)null;
        }

        public static List<Message> SearchMessage(string keyword)
        {
            return (List<Message>)null;
        }
    }
}