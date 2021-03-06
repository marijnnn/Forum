﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public partial class MainCategory
    {
        private List<Category> categories;
        public MainCategory(int id, string name, int ordernumber)
            : this(name, ordernumber)
        {
            this.Id = id;
        }

        public MainCategory(string name, int ordernumber)
        {
            this.Name = name;
            this.OrderNumber = ordernumber;
        }

        public int Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }

        public int OrderNumber
        {
            get;
            set;
        }

        public List<Category> Categories
        {
            get
            {
                if (this.categories != null)
                {
                    return this.categories;
                }
                return this.categories = Category.GetCategoriesByMainCategory(this);
            }
            set
            {
                this.categories = value;
            }
        }

        public void AddCategory(Category category)
        {
            Category.AddCategory(this, category);
        }

        public void Delete()
        {
            MainCategory.DeleteMainCategory(this);
        }

        public void Save()
        {
            MainCategory.ChangeMainCategory(this);
        }
    }
}