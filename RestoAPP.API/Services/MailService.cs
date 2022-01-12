using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RestoAPP.API.Services
{

    public class MailConfig
    {
        public string Host { get; set; }
        public string Mail { get; set; }
        public string Pwd { get; set; }
        public int Port { get; set; }
    }
    public class MailService
    {
        private MailConfig _mailConfig;
        private SmtpClient _smtpClient;

        public MailService(MailConfig mailConfig, SmtpClient smtpClient)
        {
            _mailConfig = mailConfig;
            _smtpClient = smtpClient;
        }

        public void SendEmail(string subject, string content, params string[] mails)
        {
            _smtpClient.Credentials = new NetworkCredential(_mailConfig.Mail, _mailConfig.Pwd);
            _smtpClient.Host = _mailConfig.Host;
            _smtpClient.Port = _mailConfig.Port;
            _smtpClient.EnableSsl = true;
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = content;
            message.IsBodyHtml = true;
            message.From = new MailAddress(_mailConfig.Mail);
            foreach(string mail in mails)
            {
                message.To.Add(mail);
            }
            try
            {
                _smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //ecrire dans un fichier de log 
                throw;
            }
        }
    }
}
