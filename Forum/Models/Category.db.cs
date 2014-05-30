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
            return new Category(
                Convert.ToInt32(row["CATEGORY_ID"]), 
                row["CATEGORY_NAME"].ToString(), 
                row["CATEGORY_DESCRIPTION"].ToString(), 
                Convert.ToInt32(row["CATEGORY_ORDERNUMBER"]), 
                Convert.ToInt32(row["CATEGORY_TOPICCOUNT"]), 
                Convert.ToInt32(row["CATEGORY_MESSAGECOUNT"]), 
                row["CATEGORY_LASTMESSAGE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["CATEGORY_LASTMESSAGE_ID"]), 
                (Right)Enum.Parse(typeof(Right), row["CATEGORY_MINIMUMRIGHT"].ToString())
            );
        }

        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            foreach (DataRow row in Database.GetData("SELECT * FROM CATEGORY").Rows)
            {
                categories.Add(rowToCategory(row));
            }

            return categories;
        }

        public static Category GetCategory(int id)
        {
            foreach (DataRow row in Database.GetData("SELECT * FROM CATEGORY WHERE CATEGORY_ID = " + id).Rows)
            {
                return rowToCategory(row);
            }

            return null;
        }

        public static List<Category> GetCategoriesByMainCategory(MainCategory maincategory)
        {
            List<Category> categories = new List<Category>();

            foreach (DataRow row in Database.GetData("SELECT * FROM CATEGORY WHERE CATEGORY_MAINCATEGORY_ID = " + maincategory.Id).Rows)
            {
                categories.Add(rowToCategory(row));
            }

            return categories;
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
                {"@minimumright", (int)category.MinimumRight},
                {"@maincategory_id", maincategory.Id}
            });
            category.Id = id;
        }

        public static void ChangeCategory(Category category, MainCategory maincategory = null)
        {
            Database.Execute("UPDATE CATEGORY SET CATEGORY_NAME = @name, CATEGORY_DESCRIPTION = @description, CATEGORY_ORDERNUMBER = @ordernumber, CATEGORY_MINIMUMRIGHT = @minimumright WHERE CATEGORY_ID = @id", new Dictionary<string, object>()
            {
                {"@id", category.Id},
                {"@name", category.Name},
                {"@description", category.Description},
                {"@ordernumber", category.OrderNumber},
                {"@minimumright", (int)category.MinimumRight}
            });

            if (maincategory != null)
            {
                Database.Execute("UPDATE CATEGORY SET CATEGORY_MAINCATEGORY_ID = " + maincategory.Id + " WHERE CATEGORY_ID = " + category.Id);
            }
        }
    }
}