using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ReadTest
    {
        private AccountTest usertest = new AccountTest();

        [TestInitialize]
        public void CreateAccount()
        {
            usertest.AddAccount();
            Forum.Current.Account = usertest.Toegevoegd;
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
