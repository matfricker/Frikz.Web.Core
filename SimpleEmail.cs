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
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            string devAddress = ConfigurationManager.AppSettings["DevAddress"].ToString();
            string exceptionError = null;

            MailMessage mailObj = new MailMessage(fromAddress, toAddress, subject, bodyText);
            mailObj.IsBodyHtml = true;

            if (bcc != null)
            {
                MailAddress BccAddress = new MailAddress(bcc);
                mailObj.Bcc.Add(BccAddress);
                // Add development email address
                mailObj.Bcc.Add(devAddress);
            }

            SmtpClient SMTPServer = new SmtpClient(smtpServer);
            try
            {
                SMTPServer.Send(mailObj);
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