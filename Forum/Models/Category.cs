﻿using System;
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
            return false;
        }

        public void AddTopic(Topic topic, Message message)
        {
            topic.CategoryId = this.Id;
            Topic.AddTopic(this, topic);
            topic.AddMessage(message);
        }

        public bool Isread()
        {
            return false;
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