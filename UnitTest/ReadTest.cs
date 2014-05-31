using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ReadTest
    {
        private UserTest usertest = new UserTest();

        [TestInitialize]
        public void CreateUser()
        {
            usertest.AddUser();
            Forum.Current.User = usertest.Toegevoegd;
        }

        [TestMethod]
        public void ReadCategory()
        {
            MessageTest messsagetest = new MessageTest();
            messsagetest.AddMessage();

            Assert.AreEqual(messsagetest.TopicTest.Toegevoegd.Category.IsRead(), false, "Category is read");
            messsagetest.TopicTest.Toegevoegd.Category.MarkAsRead();
            Assert.AreEqual(messsagetest.TopicTest.Toegevoegd.Category.IsRead(), true, "Category is unread");
        }
    }
}
