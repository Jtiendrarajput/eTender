using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;
using eTenderService.Tempmodel;


namespace eTenderService.DataAccess
{
    public class CategoryMaster
    {

        public static int Add(tbl_Category Category)
        {
            try          
            {
                using(DB db  = new DB())
                {
                    
                    int Count = db.tbl_Category.Count(x => x.CatCode == Category.CatCode && x.CategoryName == Category.CategoryName);
                    if (Count > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        Category.Status = 1;
                        db.tbl_Category.Add(Category);
                        db.SaveChanges();

                        return Category.ID;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public static int Update(tbl_Category Category)
        {
            try
            {
                using (DB db = new DB())
                {
                    tbl_Category Update = db.tbl_Category.FirstOrDefault(x => x.ID == Category.ID);
                    Update.CatCode = Category.CatCode;
                    Update.CategoryName = Category.CategoryName;                   
                   db.SaveChanges();

                    return Category.ID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Delete(int ID)
        {
            try
            {
                using (DB db = new DB())
                {
                    tbl_Category update = db.tbl_Category.FirstOrDefault(x => x.ID == ID);
                    update.Status = 0;
                    db.SaveChanges();
                }
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<tbl_Category> AllCategory()
        {
            List<tbl_Category> lst = new List<tbl_Category>();
            try
            {
               using(DB db = new DB())
               {
                   lst = db.tbl_Category.Where(x => x.Status == 1).ToList();
               }

               return lst;
            }
            catch(Exception ex)
            {
                throw ex;
            }
         
        }




    }
}
