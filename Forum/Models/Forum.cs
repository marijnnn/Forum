using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public static partial class Forum
    {
        public static List<MainCategory> MainCategories
        {
            get
            {
                Dictionary<int, MainCategory> maincategories = MainCategory.GetMainCategories().ToDictionary(v => v.Id, v => v);

                foreach (KeyValuePair<int, MainCategory> pair in maincategories)
                {
                    pair.Value.Categories = new List<Category>();
                }

                foreach (KeyValuePair<int, List<Category>> pair in Category.GetCategoriesByMainCategories())
                {
                    foreach (Category category in pair.Value)
                    {
                        if (category.HasAccess())
                        {
                            maincategories[pair.Key].Categories.Add(category);
                        }
                    }
                }

                return MainCategory.GetMainCategories();
            }
        }

        public static void AddMainCategory(MainCategory maincategory)
        {
            MainCategory.AddMainCategory(maincategory);
        }
    }
}