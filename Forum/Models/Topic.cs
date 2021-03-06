﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Forum
{
    public partial class Topic
    {
        private Account author;
        private Message lastmessage;
        private Category category;
        private int messagecount;

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

        public int MessageCount
        {
            get
            {
                if (!this.messagecount.Equals(default(int)))
                {
                    return this.messagecount;
                }
                return this.messagecount = (this.Messages.Count - 1);
            }
            set
            {
                this.messagecount = value;
            }
        }

        public bool IsRead
        {
            get
            {
                if (!Current.IsLoggedIn)
                {
                    return true;
                }
                else if (this.LastMessage.Date <= Forum.GetLastMarkAsRead())
                {
                    return true;
                }
                else if (this.LastMessage.Date <= Category.GetLastMarkAsRead(this.Category))
                {
                    return true;
                }
                else if (this.LastMessage.Date <= Topic.GetLastRead(this))
                {
                    return true;
                }

                return false;
            }
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

        public void Delete()
        {
            Topic.DeleteTopic(this);
        }
    }
}