using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public partial class Message
    {
        public int Id
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }

        public int AuthorId
        {
            get;
            set;
        }

        public User Author
        {
            get
            {
                return User.GetUser(this.AuthorId);
            }
        }

        public DateTime Date
        {
            get;
            set;
        }

        public int TopicId
        {
            get;
            set;
        }

        public Topic Topic
        {
            get
            {
                return Topic.GetTopic(this.TopicId);
            }
        }

        public List<MessageFile> MessageFiles
        {
            get
            {
                return MessageFile.GetMessageFilesByMessage(this);
            }
        }

        public Message(int id, string text, DateTime date, int authorid, int topicid)
            : this(text, date, authorid, topicid)
        {
            this.Id = id;
        }

        public Message(string text, DateTime date, int authorid, int topicid)
        {
            this.Text = text;
            this.Date = date;
            this.AuthorId = authorid;
            this.TopicId = topicid;
        }

        public bool IsRead()
        {
            return false;
        }

        public void Delete()
        {
            
        }

        public void AddMessageFile(MessageFile messagefile)
        {

        }
    }
}