using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public partial class MessageFile
    {
        public int Id
        {
            get;
            set;
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

        }

        public MessageFile(string name, string location)
        {

        }

        public void Delete()
        {

        }

    }
}