using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class MessageFile
    {
        public int Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Location
        {
            get;
            private set;
        }

        public MessageFile(int id, string name, string location)
            : this(name, location)
        {
            this.Id = id;
        }

        public MessageFile(string name, string location)
        {
            this.Name = name;
            this.Location = location;
        }

        public void Delete()
        {
            MessageFile.DeleteMessageFile(this);
        }
    }
}