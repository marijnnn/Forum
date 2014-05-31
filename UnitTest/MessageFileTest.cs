using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forum;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class MessageFileTest
    {
        public MessageTest MessageTest = new MessageTest();

        public Forum.MessageFile Toegevoegd
        {
            get;
            set;
        }

        [TestInitialize]
        [TestMethod]
        public void AddMessageFile()
        {
            this.MessageTest.AddMessage();

            this.Toegevoegd = new Forum.MessageFile("Name", "Location");
            this.MessageTest.Toegevoegd.AddMessageFile(this.Toegevoegd);

            List<Forum.MessageFile> messagefiles = Forum.MessageFile.GetMessageFilesByMessage(this.MessageTest.Toegevoegd);
            Assert.AreEqual(messagefiles.Count, 1, "Toegevoegde messagefile niet gevonden.");
        }

        [TestMethod]
        [TestCleanup]
        public void DeleteMessage()
        {
            this.Toegevoegd.Delete();
            List<Forum.MessageFile> messagefiles = Forum.MessageFile.GetMessageFilesByMessage(this.MessageTest.Toegevoegd);
            Assert.AreEqual(messagefiles.Count, 0, "Verwijderde messagefile gevonden.");
            this.MessageTest.DeleteMessage();
        }
    }
}
