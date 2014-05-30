using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forum;

namespace UnitTest
{
    [TestClass]
    public class MainCategoryTest
    {
        public Forum.MainCategory Toegevoegd
        {
            get;
            set;
        }

        [TestInitialize]
        public void AddMainCategory()
        {
            this.Toegevoegd = new Forum.MainCategory("test", 7);
            Forum.Forum.AddMainCategory(this.Toegevoegd);
        }

        [TestMethod]
        public void GetMainCategory()
        {
            Forum.MainCategory maincategory = Forum.MainCategory.GetMainCategory(this.Toegevoegd.Id);
            Assert.IsNotNull(maincategory, "Toegevoegde MainCategory niet gevonden.");
            Assert.AreEqual(maincategory.Name, this.Toegevoegd.Name, "Naam komt niet overeen met toegevoegd");
            Assert.AreEqual(maincategory.OrderNumber, this.Toegevoegd.OrderNumber, "Ordernumber komt niet overeen met toegevoegd");
        }

        [TestMethod]
        public void FindMainCategoryInList()
        {
            Forum.MainCategory maincategory = Forum.MainCategory.GetMainCategories().Find(a => a.Id == this.Toegevoegd.Id);
            Assert.IsNotNull(maincategory, "Toegevoegde MainCategory niet gevonden in lijst.");
        }

        [TestMethod]
        public void ChangeMainCategory()
        {
            this.Toegevoegd.Name = "test2";
            this.Toegevoegd.OrderNumber = 8;
            this.Toegevoegd.Save();

            Forum.MainCategory maincategory = Forum.MainCategory.GetMainCategory(this.Toegevoegd.Id);
            Assert.IsNotNull(maincategory, "Veranderde MainCategory niet gevonden.");
            Assert.AreEqual(maincategory.Name, this.Toegevoegd.Name, "Naam komt niet overeen met gewijzigd");
            Assert.AreEqual(maincategory.OrderNumber, this.Toegevoegd.OrderNumber, "Ordernumber komt niet overeen met gewijzigd");
        }

        [TestCleanup]
        [TestMethod]
        public void DeleteMainCategory()
        {
            this.Toegevoegd.Delete();
            Assert.IsNull(Forum.MainCategory.GetMainCategory(this.Toegevoegd.Id), "Verwijderde MainCategory gevonden.");
        }

    }
}
