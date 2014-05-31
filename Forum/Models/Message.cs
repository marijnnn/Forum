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