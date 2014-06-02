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

                Dictionary<int, List<Category>> categories = Category.GetCategoriesByMainCategories();
                Dictionary<int, int> unreadtopiccounts = Category.GetUnreadTopicCounts(categories.Values.SelectMany(a => a).ToList());

                foreach (KeyValuePair<int, List<Category>> pair in categories)
                {
                    foreach (Category category in pair.Value)
                    {
                        if (category.HasAccess())
                        {
                            category.UnreadTopicCount = unreadtopiccounts[category.Id];
                            maincategories[pair.Key].Categories.Add(category);
                        }
                    }
                }

                return maincategories.Values.ToList();
            }
        }

        public static void AddMainCategory(MainCategory maincategory)
        {
            MainCategory.AddMainCategory(maincategory);
        }
    }
}