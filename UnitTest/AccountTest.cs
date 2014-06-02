using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class AccountTest
    {
        public Forum.Account Toegevoegd
        {
            get;
            set;
        }

        [TestMethod]
        [TestInitialize]
        public void AddAccount()
        {
            this.Toegevoegd = new Forum.Account(Path.GetRandomFileName().Replace(".", ""), "Password");
            Forum.Account.Register(this.Toegevoegd);

            Forum.Account user = Forum.Account.GetAccount(this.Toegevoegd.Id);
            Assert.IsNotNull(user, "Toegevoegde gebruiker niet gevonden");
        }

        [TestMethod]
        public void GetAccount()
        {
            Forum.Account account = Forum.Account.GetAccount(this.Toegevoegd.Username);
            Assert.IsNotNull(account, "Gebruiker niet gevonden");
            Assert.AreEqual(this.Toegevoegd.Id, account.Id, "Id komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.Password, account.Password, "Password komt niet overeen met toegevoegd");
        }
    }
}
