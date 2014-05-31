using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forum;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class MessageFileTest
    {
        private MessageTest messagetest = new MessageTest();

        public Forum.MessageFile Toegevoegd
        {
            get;
            set;
        }

        [TestInitialize]
        [TestMethod]
        public void AddMessageFile()
        {
            this.messagetest.AddMessage();

            this.Toegevoegd = new Forum.MessageFile("Name", "Location");
            this.messagetest.Toegevoegd.AddMessageFile(this.Toegevoegd);

            List<Forum.MessageFile> messagefiles = Forum.MessageFile.GetMessageFilesByMessage(this.messagetest.Toegevoegd);
            Assert.AreEqual(messagefiles.Count, 1, "Toegevoegde messagefile niet gevonden.");
        }

        [TestMethod]
        [TestCleanup]
        public void DeleteMessage()
        {
            this.Toegevoegd.Delete();
            List<Forum.MessageFile> messagefiles = Forum.MessageFile.GetMessageFilesByMessage(this.messagetest.Toegevoegd);
            Assert.AreEqual(messagefiles.Count, 0, "Verwijderde messagefile gevonden.");
            this.messagetest.DeleteMessage();
        }
    }
}
