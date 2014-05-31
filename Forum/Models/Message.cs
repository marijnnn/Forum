using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class Message
    {
        public int Id
        {
            get;
            private set;
        }
        public string Text
        {
            get;
            private set;
        }

        public int AuthorId
        {
            get;
            private set;
        }

        private User author;
        public User Author
        {
            get
            {
                if (this.author != null)
                {
                    return this.author;
                }
                return this.author = User.GetUser(this.AuthorId);
            }
            set
            {
                this.author = value;
            }
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public int TopicId
        {
            get;
            set;
        }

        private Topic topic;
        public Topic Topic
        {
            get
            {
                if (this.topic != null)
                {
                    return this.topic;
                }
                return this.topic = Topic.GetTopic(this.TopicId);
            }
            set
            {
                this.topic = value;
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
            : this(text, date, authorid)
        {
            this.Id = id;
            this.TopicId = topicid;
        }

        public Message(string text, DateTime date, int authorid)
        {
            this.Text = text;
            this.Date = date;
            this.AuthorId = authorid;
        }

        public bool IsRead()
        {
            if (this.Date < Forum.GetLastMarkAsRead())
            {
                return true;
            }
            else if (this.Date < Category.GetLastMarkAsRead(this.topic.Category))
            {
                return true;
            }
            else if (this.Date < Topic.GetLastMarkAsRead(this.topic))
            {
                return true;
            }

            return false;
        }

        public void Delete()
        {
            Message.DeleteMessage(this);
        }

        public void AddMessageFile(MessageFile messagefile)
        {

        }
    }
}