using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace Frikz.Web.Core
{
    public class SimpleEmail
    {
        public static bool GenerateEmail(string fromAddress, string toAddress, string bcc, string subject, string bodyText)
        {
            string exceptionError = null;

            SmtpClient client = 
                new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString(), 
                               Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]));
            client.UseDefaultCredentials = false;
            client.Credentials = 
                new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminAddress"].ToString(),
                                                 ConfigurationManager.AppSettings["AdminPass"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            MailMessage mailObj = new MailMessage(fromAddress, toAddress, subject, bodyText);
            mailObj.IsBodyHtml = true;

            if (bcc != null)
            {
                MailAddress BccAddress = new MailAddress(bcc);
                mailObj.Bcc.Add(BccAddress);
            }

            //SmtpClient SMTPServer = new SmtpClient(smtpServer);
            try
            {
                client.Send(mailObj);
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Implement Error Logging
                exceptionError = ex.ToString();
                return false;
                
            }    
        }
        
        
    }
}