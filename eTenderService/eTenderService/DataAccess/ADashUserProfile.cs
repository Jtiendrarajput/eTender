using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;

namespace eTenderService.DataAccess
{
    public class ADashUserProfile
    {
        public static tblUserProfile GetUserDetails(int ID)
        {
            try
            {
                using (DB db = new DB())
                {
                    tblUserProfile usertbl = db.tblUserProfiles.FirstOrDefault(x => x.UserID == ID);
                    return usertbl;
                }
            }
            catch (Exception ex) { throw ex; }
        }




    }
}
