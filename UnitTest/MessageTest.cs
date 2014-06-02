using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class MessageTest
    {
        public TopicTest TopicTest = new TopicTest();
        public AccountTest AccountTest = new AccountTest();

        public Forum.Message Toegevoegd
        {
            get;
            set;
        }

        public MessageTest()
        {
            this.TopicTest.AddTopic();
            this.AccountTest.AddAccount();
        }

        [TestInitialize]
        [TestMethod]
        public void AddMessage()
        {
            this.Toegevoegd = new Forum.Message("Hier de reactie", DateTime.Now, this.AccountTest.Toegevoegd.Id);
            this.TopicTest.Toegevoegd.AddMessage(this.Toegevoegd);

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
            Forum.Message message = this.TopicTest.Toegevoegd.Messages.Find(a => a.Id == this.Toegevoegd.Id);
            Assert.IsNotNull(message, "Message niet gevonden.");
        }

        [TestMethod]
        public void DeleteMessage()
        {
            this.Toegevoegd.Delete();
            Assert.IsNull(Forum.Message.GetMessage(this.Toegevoegd.Id), "Verwijderde Message gevonden.");
            this.TopicTest.DeleteTopic();
        }
    }
}
