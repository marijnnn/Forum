using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class MessageFile
    {
        public static List<MessageFile> GetMessageFilesByMessage(Message message)
        {
            return (List<MessageFile>)null;
        }

        public static Dictionary<int, List<MessageFile>> GetMesssageFilesByMessages(List<Message> messages)
        {
            return (Dictionary<int, List<MessageFile>>)null;
        }
    }
}