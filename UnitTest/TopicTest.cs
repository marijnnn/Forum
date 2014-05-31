using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forum;

namespace UnitTest
{
    [TestClass]
    public class TopicTest
    {
        private CategoryTest categorytest = new CategoryTest();
        private UserTest usertest = new UserTest();

        public Forum.Topic Toegevoegd
        {
            get;
            set;
        }

        [TestInitialize]
        [TestMethod]
        public void AddTopic()
        {
            this.categorytest.AddCategory();
            this.usertest.AddUser();

            this.Toegevoegd = new Forum.Topic("Titel", this.usertest.Toegevoegd.Id);
            Forum.Message message = new Forum.Message("Hier de inhoud", DateTime.Now, this.usertest.Toegevoegd.Id);
            this.categorytest.Toegevoegd.AddTopic(this.Toegevoegd, message);

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
            Forum.Topic topic = this.categorytest.Toegevoegd.Topics.Find(a => a.Id == this.Toegevoegd.Id);
            Assert.IsNotNull(topic, "Topic niet gevonden.");
        }

        [TestMethod]
        [TestCleanup]
        public void DeleteTopic()
        {
            this.Toegevoegd.Delete();
            Assert.IsNull(Forum.Topic.GetTopic(this.Toegevoegd.Id), "Verwijderde Topic gevonden.");
            this.categorytest.DeleteCategory();
        }
    }
}
