using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class MessageTest
    {
        private TopicTest topictest = new TopicTest();
        private UserTest usertest = new UserTest();

        public Forum.Message Toegevoegd
        {
            get;
            set;
        }

        [TestMethod]
        [TestInitialize]
        public void AddMessage()
        {
            this.topictest.AddTopic();
            this.usertest.AddUser();

            this.Toegevoegd = new Forum.Message("Hier de reactie", DateTime.Now, this.usertest.Toegevoegd.Id);
            this.topictest.Toegevoegd.AddMessage(this.Toegevoegd);

            Forum.Message message = Forum.Message.GetMessage(this.Toegevoegd.Id);
            Assert.IsNotNull(message, "Toegevoegde message niet gevonden.");
        }

        [TestMethod]
        public void GetMessage()
        {
            Forum.Message message = Forum.Message.GetMessage(this.Toegevoegd.Id);
            Assert.IsNotNull(message, "Toegevoegde message niet gevonden.");
            Assert.AreEqual(this.Toegevoegd.Text, message.Text, "Text komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.AuthorId, message.AuthorId, "AuthorId komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.TopicId, message.TopicId, "TopicId komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.Date.ToString("yyyy-MM-dd HH:mm:ss"), message.Date.ToString("yyyy-MM-dd HH:mm:ss"), "Date komt niet overeen met toegevoegd");
        }

        [TestMethod]
        public void FindMessageInTopic()
        {
            Forum.Message message = this.topictest.Toegevoegd.Messages.Find(a => a.Id == this.Toegevoegd.Id);
            Assert.IsNotNull(message, "Message niet gevonden.");
        }

        [TestMethod]
        public void DeleteMessage()
        {
            this.Toegevoegd.Delete();
            Assert.IsNull(Forum.Message.GetMessage(this.Toegevoegd.Id), "Verwijderde Message gevonden.");
            this.topictest.DeleteTopic();
        }
    }
}
