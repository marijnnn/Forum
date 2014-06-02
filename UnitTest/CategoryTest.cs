using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class CategoryTest
    {
        public MainCategoryTest MainCategoryTest = new MainCategoryTest();

        public Forum.Category Toegevoegd
        {
            get;
            set;
        }

        [TestInitialize]
        [TestMethod]
        public void AddCategory()
        {
            this.MainCategoryTest.AddMainCategory();

            this.Toegevoegd = new Forum.Category("Name", "Description", 5, Forum.Right.User);
            this.MainCategoryTest.Toegevoegd.AddCategory(this.Toegevoegd);

            Forum.Category category = Forum.Category.GetCategory(this.Toegevoegd.Id);
            Assert.IsNotNull(category, "Toegevoegde category niet gevonden.");
        }

        [TestMethod]
        public void FindCategoryInMainCategory()
        {
            Forum.Category category = this.MainCategoryTest.Toegevoegd.Categories.Find(a => a.Id == this.Toegevoegd.Id);
            Assert.IsNotNull(category, "Category niet gevonden.");
        }

        [TestMethod]
        public void GetCategory()
        {
            Forum.Category category = Forum.Category.GetCategory(this.Toegevoegd.Id);
            Assert.IsNotNull(category, "Toegevoegde category niet gevonden.");
            Assert.AreEqual(this.Toegevoegd.Name, category.Name, "Naam komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.Description, category.Description, "Omschrijving komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.OrderNumber, category.OrderNumber, "Ordernumber komt niet overeen met toegevoegd");
            Assert.AreEqual(this.Toegevoegd.MinimumRight, category.MinimumRight, "Minimumright komt niet overeen met toegevoegd");
        }

        [TestMethod]
        public void FindCategoryInList()
        {
            Forum.Category category = Forum.Category.GetCategoriesByMainCategories().Find(a => a.Id == this.Toegevoegd.Id);
            Assert.IsNotNull(category, "Category niet gevonden.");
        }

        [TestMethod]
        public void ChangeCategory()
        {
            this.Toegevoegd.Name = "Name2";
            this.Toegevoegd.Description = "Description2";
            this.Toegevoegd.OrderNumber = 10;
            this.Toegevoegd.MinimumRight = Forum.Right.Administrator;
            this.Toegevoegd.Save();

            Forum.Category category = Forum.Category.GetCategory(this.Toegevoegd.Id);
            Assert.IsNotNull(category, "Toegevoegde category niet gevonden.");
            Assert.AreEqual(this.Toegevoegd.Name, category.Name, "Naam komt niet overeen met gewijzigd");
            Assert.AreEqual(this.Toegevoegd.Description, category.Description, "Omschrijving komt niet overeen met gewijzigd");
            Assert.AreEqual(this.Toegevoegd.OrderNumber, category.OrderNumber, "Ordernumber komt niet overeen met gewijzigd");
            Assert.AreEqual(this.Toegevoegd.MinimumRight, category.MinimumRight, "Minimumright komt niet overeen met gewijzigd");
        }

        [TestCleanup]
        [TestMethod]
        public void DeleteCategory()
        {
            this.Toegevoegd.Delete();
            Assert.IsNull(Forum.Category.GetCategory(this.Toegevoegd.Id), "Verwijderde Category gevonden.");
            this.MainCategoryTest.DeleteMainCategory();
        }
    }
}
