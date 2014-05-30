using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class Topic
    {
        public static Topic GetTopic(int id)
        {
            return (Topic)null;
        }

        public static List<Topic> GetTopicByCategory(Category category, int page = 0, int count = 30)
        {
            return (List<Topic>)null;
        }
    }
}