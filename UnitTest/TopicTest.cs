using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forum;

namespace UnitTest
{
    [TestClass]
    public class TopicTest
    {
        public CategoryTest CategoryTest = new CategoryTest();
        public AccountTest AccountTest = new AccountTest();

        public Forum.Topic Toegevoegd
        {
            get;
            set;
        }

        [TestInitialize]
        [TestMethod]
        public void AddTopic()
        {
            this.CategoryTest.AddCategory();
            this.AccountTest.AddAccount();

            this.Toegevoegd = new Forum.Topic("Titel", this.AccountTest.Toegevoegd.Id);
            Forum.Message message = new Forum.Message("Hier de inhoud", DateTime.Now, this.AccountTest.Toegevoegd.Id);
            this.CategoryTest.Toegevoegd.AddTopic(this.Toegevoegd, message);

            Forum.Topic topic = Forum.Topic.GetTopic(this.Toegevoegd.Id);
            Assert.IsNotNull(topic, "Toegevoegde topic niet gevonden.");
        }

        [TestMethod]
        public void GetTopic()
        {
            Forum.Topic topic = Forum.Topic.GetTopic(this.Toegevoegd.Id);
            Assert.IsNotNull(topic, "Toegevoegde topic niet gevonden.");
            Assert.AreEqual(this.Toegevoegd.Name, topic.Name, "Naam komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.AuthorId, topic.AuthorId, "AuthorId komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.CategoryId, topic.CategoryId, "CategoryId komt niet overeen met toegevoegd");
        }

        [TestMethod]
        public void FindTopicInCategory()
        {
            Forum.Topic topic = this.CategoryTest.Toegevoegd.Topics.Find(a => a.Id == this.Toegevoegd.Id);
            Assert.IsNotNull(topic, "Topic niet gevonden.");
        }

        [TestMethod]
        [TestCleanup]
        public void DeleteTopic()
        {
            this.Toegevoegd.Delete();
            Assert.IsNull(Forum.Topic.GetTopic(this.Toegevoegd.Id), "Verwijderde Topic gevonden.");
            this.CategoryTest.DeleteCategory();
        }
    }
}
