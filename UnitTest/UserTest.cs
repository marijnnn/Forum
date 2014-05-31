using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UserTest
    {
        public Forum.User Toegevoegd
        {
            get;
            set;
        }

        [TestMethod]
        [TestInitialize]
        public void AddUser()
        {
            this.Toegevoegd = new Forum.User(Path.GetRandomFileName().Replace(".", ""), "Password");
            Forum.User.Register(this.Toegevoegd);

            Forum.User user = Forum.User.GetUser(this.Toegevoegd.Id);
            Assert.IsNotNull(user, "Toegevoegde gebruiker niet gevonden");
        }

        [TestMethod]
        public void GetUser()
        {
            Forum.User user = Forum.User.GetUser(this.Toegevoegd.Username);
            Assert.IsNotNull(user, "Gebruiker niet gevonden");
            Assert.AreEqual(this.Toegevoegd.Id, user.Id, "Id komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.Password, user.Password, "Password komt niet overeen met toegevoegd");
        }
    }
}
