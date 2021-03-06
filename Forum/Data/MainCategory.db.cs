﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Forum
{
	public partial class MainCategory
	{
        public static MainCategory RowToMainCategory(DataRow row)
        {
            return new MainCategory(Convert.ToInt32(row["MAINCATEGORY_ID"]), row["MAINCATEGORY_NAME"].ToString(), Convert.ToInt32(row["MAINCATEGORY_ORDERNUMBER"]));
        }

        public static List<MainCategory> GetMainCategories()
        {
            List<MainCategory> maincategories = new List<MainCategory>();

            foreach (DataRow row in Database.GetData("SELECT * FROM MAINCATEGORY").Rows)
            {
                maincategories.Add(RowToMainCategory(row));
            }

            return maincategories;
        }

        public static MainCategory GetMainCategory(int id)
        {
            foreach (DataRow row in Database.GetData("SELECT * FROM MAINCATEGORY WHERE MAINCATEGORY_ID = " + id).Rows)
            {
                return RowToMainCategory(row);
            }

            return null;
        }

        public static void AddMainCategory(MainCategory maincategory)
        {
            int id = Database.GetSequence("SEQ_MAINCATEGORY");
            Database.Execute("INSERT INTO MAINCATEGORY (MAINCATEGORY_ID, MAINCATEGORY_NAME, MAINCATEGORY_ORDERNUMBER) VALUES (:id, :name, :ordernumber)", new Dictionary<string, object>()
            {
                {"id", id},
                {"name", maincategory.Name},
                {"ordernumber", maincategory.OrderNumber}
            });
            maincategory.Id = id;
        }

        public static void ChangeMainCategory(MainCategory maincategory)
        {
            Database.Execute("UPDATE MAINCATEGORY SET MAINCATEGORY_NAME = :name, MAINCATEGORY_ORDERNUMBER = :ordernumber WHERE MAINCATEGORY_ID = " + maincategory.Id, new Dictionary<string, object>()
            {
                {"name", maincategory.Name},
                {"ordernumber", maincategory.OrderNumber}
            });
        }

        public static void DeleteMainCategory(MainCategory maincategory)
        {
            Database.Execute("DELETE FROM MAINCATEGORY WHERE MAINCATEGORY_ID = " + maincategory.Id);
        }
	}
}