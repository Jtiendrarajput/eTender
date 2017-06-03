using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
namespace eTenderService.DataAccess
{
    public class MailHelper
    {
        public static void sendmail(string to, string subject, string body)
        {
            string password = "#Ki934to";
            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.From = new MailAddress("admin@nppmathura.com");
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.To.Add(new MailAddress(to));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "198.38.90.113";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = false;
            NetworkCredential nc = new NetworkCredential("admin@nppmathura.com", password);
            smtp.Credentials = nc;
            smtp.Send(msg);




        }
    }
}
