using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;
using eTenderService.Tempmodel;


namespace eTenderService.DataAccess
{
    public class DepartmentMaster
    {

        public static int Add(tbl_Department department)
        {
            try
            {
                using(DB db = new DB())
                {
                   
                    int Count = db.tbl_Department.Count(x => x.DeptCode == department.DeptCode && x.DepartmentName == department.DepartmentName);
                    if (Count > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        department.Status = 1;
                        db.tbl_Department.Add(department);
                        db.SaveChanges();
                        return department.ID;
                    }
                }
              
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int Update(tbl_Department department)
        {
            try
            {
                using(DB db = new DB())
                {
                    tbl_Department Update = db.tbl_Department.FirstOrDefault(x => x.ID == department.ID);
                    Update.DeptCode = department.DeptCode;
                    Update.DepartmentName = department.DepartmentName;
                    db.SaveChanges();

                    return department.ID;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int Delete(int ID)
        {
            try
            {
               using(DB db = new DB())
               {
                   tbl_Department delete = db.tbl_Department.FirstOrDefault(x => x.ID == ID);
                   delete.Status = 0;
                   db.SaveChanges();

                   return ID;
               }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public static List<tbl_Department> AllDepartment()
        {
            List<tbl_Department> lst = new List<tbl_Department>();
            try
            {
                using(DB db = new DB())
                {
                    lst = db.tbl_Department.Where(x => x.Status == 1).ToList();
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
