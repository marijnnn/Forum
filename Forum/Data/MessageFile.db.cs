﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
    public partial class MessageFile
    {
        public static MessageFile rowToMessageFile(DataRow row)
        {
            return new MessageFile(Convert.ToInt32(row["MESSAGEFILE_ID"]), row["MESSAGEFILE_NAME"].ToString(), row["MESSAGEFILE_LOCATION"].ToString());
        } 

        public static List<MessageFile> GetMessageFilesByMessage(Message message)
        {
            List<MessageFile> messagefiles = new List<MessageFile>();

            foreach (DataRow row in Database.GetData("SELECT * FROM MESSAGEFILE WHERE MESSAGEFILE_MESSAGE_ID = " + message.Id).Rows)
            {
                messagefiles.Add(rowToMessageFile(row));
            }

            return messagefiles;
        }

        public static Dictionary<int, List<MessageFile>> GetMesssageFilesByMessages(List<Message> messages)
        {
            List<int> messageids = messages.Select(m => m.Id).ToList();
            Dictionary<int, List<MessageFile>> messagefiles = new Dictionary<int, List<MessageFile>>();

            if (messageids.Count > 0)
            {
                foreach (DataRow row in Database.GetData("SELECT * FROM MESSAGEFILE WHERE MESSAGEFILE_MESSAGE_ID IN (@messageids)", new Dictionary<string, object>()
                {
                    {"@messageids", messageids.ConvertAll<string>(x => x.ToString())}
                }).Rows)
                {
                    int message_id = Convert.ToInt32(row["MESSAGEFILE_MESSAGE_ID"]);

                    if (messagefiles.ContainsKey(message_id))
                    {
                        messagefiles[message_id] = new List<MessageFile>();
                    }

                    messagefiles[message_id].Add(rowToMessageFile(row));
                }
            }

            return messagefiles;
        }
    }
}