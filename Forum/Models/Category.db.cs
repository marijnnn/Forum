using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
    public partial class Category
    {
        private static Category rowToCategory(DataRow row)
        {
            return new Category(Convert.ToInt32(row["CATEGORY_ID"]), row["CATEGORY_NAME"].ToString(), row["CATEGORY_DESCRIPTION"].ToString(), Convert.ToInt32(row["CATEGORY_ORDERNUMBER"]), Convert.ToInt32(row["CATEGORY_TOPICCOUNT"]), Convert.ToInt32(row["CATEGORY_MESSAGECOUNT"]), Convert.ToInt32(row["CATEGORY_LASTMESSAGE_ID"]), (Right)Enum.Parse(typeof(Right), row["CATEGORY_MINIMUMRIGHT"].ToString()));
        }

        public static List<Category> GetCategories()
        {
            return (List<Category>)null;
        }

        public static Category GetCategory(int id)
        {
            return (Category)null;
        }

        public static List<Category> GetCategoryByMainCategory(MainCategory maincategory)
        {
            return (List<Category>)null;
        }

        public static void AddCategory(MainCategory maincategory, Category category)
        {
            int id = Database.GetSequence("SEQ_CATEGORY");
            Database.Execute("INSERT INTO CATEGORY (CATEGORY_ID, CATEGORY_NAME, CATEGORY_DESCRIPTION, CATEGORY_ORDERNUMBER, CATEGORY_MINIMUMRIGHT, CATEGORY_MAINCATEGORY_ID) VALUES (@id, @name, @description, @ordernumber, @minimumright, @maincategory_id)", new Dictionary<string, object>()
            {
                {"@id", id},
                {"@name", category.Name},
                {"@description", category.Description},
                {"@ordernumber", category.OrderNumber},
                {"@minimumright", category.MinimumRight},
                {"@maincategory_id", maincategory.Id}
            });
            category.Id = id;
        }
    }
}