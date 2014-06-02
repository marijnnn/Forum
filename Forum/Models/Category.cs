using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class Category
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

        public string Description
        {
            get;
            set;
        }

        public int OrderNumber
        {
            get;
            set;
        }

        public int TopicCount
        {
            get;
            private set;
        }

        public int MessageCount
        {
            get;
            private set;
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

        public Right MinimumRight
        {
            get;
            set;
        }

        private int unreadtopiccount;
        public int UnreadTopicCount
        {
            get
            {
                if (this.unreadtopiccount != default(int))
                {
                    return this.unreadtopiccount;
                }
                return this.unreadtopiccount = Category.GetUnreadTopicCount(this);
            }
            set
            {
                this.unreadtopiccount = value;
            }
        }

        public List<Topic> Topics
        {
            get
            {
                return Topic.GetTopicByCategory(this);
            }
        }

        public Category(int id, string name, string description, int ordernumber, int topiccount, int messagecount, int lastmessageid, Right minimumright)
            : this(name, description, ordernumber, minimumright)
        {
            this.Id = id;
            this.OrderNumber = ordernumber;
            this.TopicCount = topiccount;
            this.MessageCount = messagecount;
            this.LastMessageId = lastmessageid;
        }

        public Category(string name, string description, int ordernumber, Right minimumright)
        {
            this.Name = name;
            this.Description = description;
            this.MinimumRight = minimumright;
        }

        public void MarkAsRead()
        {
            Category.MarkAsRead(this);
        }

        public bool HasAccess()
        {
            if (this.MinimumRight <= Current.Right)
            {
                return true;
            }

            return false;
        }

        public void AddTopic(Topic topic, Message message)
        {
            topic.CategoryId = this.Id;
            Topic.AddTopic(this, topic);
            topic.AddMessage(message);
        }

        public void Delete()
        {
            Category.DeleteCategory(this);
        }

        public void Save(MainCategory maincategory = null)
        {
            Category.ChangeCategory(this, maincategory);
        }
    }
}