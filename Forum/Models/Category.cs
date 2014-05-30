using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public partial class Category
    {
        public int Id
        {
            get;
            set;
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
            set;
        }

        public int MessageCount
        {
            get;
            set;
        }

        public int LastMessageId
        {
            get;
            set;
        }

        public Right MinimumRight
        {
            get;
            set;
        }

        public int UnreadTopicCount
        {
            get;
            private set;
        }

        public Category(int id, string name, string description, int ordernumber, int topiccount, int messagecount, int lastmessageid, Right minimumright)
        {
            this.Id = id;
        }

        public Category(string name, string description, int ordernumber, int topiccount, int messagecount, int lastmessageid, Right minimumright)
        {
            this.Name = name;
            this.Description = description;
            this.OrderNumber = ordernumber;
            this.TopicCount = topiccount;
            this.MessageCount = messagecount;
            this.LastMessageId = lastmessageid;
            this.MinimumRight = minimumright;
        }

        public void MarkAsRead()
        {

        }

        public List<Topic> GetTopics(int page, int count = 30)
        {
            return Topic.GetTopicByCategory(this, page, count);
        }
    }
}