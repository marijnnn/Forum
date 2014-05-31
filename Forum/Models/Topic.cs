using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class Topic
    {
        public int Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }

        public int AuthorId
        {
            get;
            set;
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

        public int LastMessageId
        {
            get;
            private set;
        }

        public Message LastMessage
        {
            get
            {
                return Message.GetMessage(this.LastMessageId);
            }
        }

        public int CategoryId
        {
            get;
            set;
        }

        public Category Category
        {
            get
            {
                return Category.GetCategory(this.CategoryId);
            }
        }

        public List<Message> Messages
        {
            get
            {
                return Message.GetMessagesByTopic(this);
            }
        }

        public Topic(int id, string name, int authorid, int lastmessageid, int categoryid)
            : this(name, authorid)
        {
            this.Id = id;
            this.LastMessageId = lastmessageid;
            this.CategoryId = categoryid;
        }

        public Topic(string name, int authorid)
        {
            this.Name = name;
            this.AuthorId = authorid;
        }

        public bool HasAccess()
        {
            return false;
        }

        public void AddMessage(Message message)
        {
            message.TopicId = this.Id;
            Message.AddMessage(message);
        }

        public void Read()
        {
            Topic.MarkAsRead(this);
        }

        public bool IsRead()
        {
            if (this.LastMessage.Date < Forum.GetLastMarkAsRead())
            {
                return true;
            }
            else if (this.LastMessage.Date < Category.GetLastMarkAsRead(this.Category))
            {
                return true;
            }
            else if (this.LastMessage.Date < Topic.GetLastMarkAsRead(this))
            {
                return true;
            }

            return false;
        }

        public void Delete()
        {
            Topic.DeleteTopic(this);
        }
    }
}