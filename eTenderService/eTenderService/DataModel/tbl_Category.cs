using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
   public class tbl_Category
    {
       public int ID { get; set; }
       public string CatCode { get; set; }
       public string CategoryName { get; set; }
       public int Status { get; set; }
    }
}
