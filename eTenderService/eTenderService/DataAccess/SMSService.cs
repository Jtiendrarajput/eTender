using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace eTenderService.DataAccess
{
   public  class SMSService
    {
       public static void Send(string message, string mobilenumber)
            {
           try
           {
               //string URL = "http://sms3.indiainteractive.net/submitsms.jsp?user=Interact&key=bd71879f51XX&mobile=" + mobilenumber + "&message=" + message + "&senderid=NPPMTR&accusage=1&unicode=1";
               //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
               //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               //response.Close();
           }
           catch (Exception ex)
           {
               throw ex;
           }   
       }
    }
}
