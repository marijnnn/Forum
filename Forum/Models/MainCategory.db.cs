using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
	public partial class MainCategory
	{
        public static List<MainCategory> GetMainCategories()
        {
            return new List<MainCategory>();
        }

        public static void AddMainCategory(MainCategory maincategory)
        {
            int id = Database.GetSequence("SEQ_MAINCATEGORY");
            Database.Execute("INSERT INTO MAINCATEGORY (MAINCATEGORY_ID, MAINCATEGORY_NAME, MAINCATEGORY_ORDERNUMBER) VALUES (@id, @name, @ordernumber)", new Dictionary<string, object>()
            {
                {"@name", maincategory.Name},
                {"@ordernumber", maincategory.OrderNumber}
            });
            maincategory.Id = id;
        }
	}
}