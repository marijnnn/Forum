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
            private set;
        }

        private Account author;
        public Account Author
        {
            get
            {
                if (this.author != null)
                {
                    return this.author;
                }
                return this.author = Account.GetAccount(this.AuthorId);
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

        private Message lastmessage;
        public Message LastMessage
        {
            get
            {
                if (this.lastmessage != null)
                {
                    return this.lastmessage;
                }
                return this.lastmessage = Message.GetMessage(this.LastMessageId);
            }
            set
            {
                this.lastmessage = value;
            }
        }

        public int CategoryId
        {
            get;
            set;
        }

        private Category category;
        public Category Category
        {
            get
            {
                if (this.category != null)
                {
                    return this.category;
                }
                return this.category = Category.GetCategory(this.CategoryId);
            }
            set
            {
                this.category = value;
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
            return this.Category.HasAccess();
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
            else if (this.LastMessage.Date < Topic.GetLastRead(this))
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