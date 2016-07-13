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
        public static bool GenerateEmail(string fromAddress, string toAddress, string bcc, string subject, string bodyText, bool isSsl)
        {
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            string account = ConfigurationManager.AppSettings["AdminAddress"].ToString();
            string pass = ConfigurationManager.AppSettings["AdminPass"].ToString();
            string exceptionError = null;

            SmtpClient client = new SmtpClient(smtpServer, smtpPort);
            client.UseDefaultCredentials = true;
            client.EnableSsl = isSsl;
            client.Credentials = new System.Net.NetworkCredential(account, pass);
            
            
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailObj = new MailMessage(fromAddress, toAddress, subject, bodyText);
            mailObj.IsBodyHtml = true;

            if (bcc != null)
            {
                MailAddress BccAddress = new MailAddress(bcc);
                mailObj.Bcc.Add(BccAddress);
            }

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