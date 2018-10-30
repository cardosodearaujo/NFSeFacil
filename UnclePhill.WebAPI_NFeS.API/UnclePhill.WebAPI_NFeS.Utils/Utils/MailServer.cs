﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UnclePhill.WebAPI_NFeS.Utils.Utils
{
    public class MailServer
    {
        private SmtpClient MailClient;
        
        public MailServer(string Server, int Port, string User, string Password)
        {
            MailClient = new SmtpClient(Server, Port);
            MailClient.UseDefaultCredentials = false;
            MailClient.EnableSsl = true;
            NetworkCredential basicAuthenticationInfo = new NetworkCredential(User, Password);
            MailClient.Credentials = basicAuthenticationInfo;
        }

        public void SendEmail(string Sender, string Receiver, string Subject, string Message, string DisplayName)
        {
            try
            {
                MailMessage email = new MailMessage(new MailAddress(Sender, DisplayName), new MailAddress(Receiver));
                email.Subject = Subject;
                email.SubjectEncoding = Encoding.UTF8;
                email.Body = Message;
                email.BodyEncoding = Encoding.UTF8;
                email.IsBodyHtml = true;
            
                MailClient.Send(email);
            }
            catch
            {
            }
        }
    }
}
