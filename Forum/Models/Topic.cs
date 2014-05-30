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
            set;
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

        public User Author
        {
            get
            {
                return User.GetUser(this.AuthorId);
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

        public Topic(int id, string name, int authorid, int lastmessageid, int categoryid)
            : this(name, authorid, lastmessageid, categoryid)
        {
            this.Id = id;
        }

        public Topic(string name, int authorid, int lastmessageid, int categoryid)
        {
            this.Name = name;
            this.AuthorId = authorid;
            this.LastMessageId = lastmessageid;
            this.CategoryId = categoryid;
        }

        public List<Message> GetMessages(int page, int count = 10)
        {
            return Message.GetMessagesByTopic(this, page, count);
        }

        public bool HasAccess()
        {
            return false;
        }

        public void AddMessage(Message message)
        {

        }

        public void Read()
        {

        }

        public bool IsRead()
        {
            return false;
        }

        public void Delete()
        {

        }
    }
}