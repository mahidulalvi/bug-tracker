﻿using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    /// <summary>
    /// Represents an e-mail sender
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        //Getting the settings from our private.config setup in web.config
        private string SmtpHost = ConfigurationManager.
            AppSettings["SmtpHost"];
        private int SmtpPort = Convert.ToInt32(
            ConfigurationManager.AppSettings["SmtpPort"]);
        private string SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
        private string SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
        private string SmtpFrom = ConfigurationManager.AppSettings["SmtpFrom"];
        private ApplicationDbContext DbContext { get; set; }

        public EmailService()
        {
            DbContext = new ApplicationDbContext();
        }

        /// <summary>
        /// Sends an e-mail
        /// </summary>
        /// <param name="to">The destination of the e-mail</param>
        /// <param name="body">The body of the e-mail</param>
        /// <param name="subject">The subject of the e-mail</param>
        public void Send(string to,
            string body,
            string subject)
        {                 
            //  Creates a MailMessage required to send messages
            var message = new MailMessage(SmtpFrom, to);
            message.Body = body;
            message.Subject = subject;
            message.IsBodyHtml = true;

            //  Creates a SmtpClient required to handle the communication
            //  between our application and the SMTP Server
            var smtpClient = new SmtpClient(SmtpHost, SmtpPort);
            smtpClient.Credentials =
                new NetworkCredential(SmtpUsername, SmtpPassword);
            smtpClient.EnableSsl = true;

            //  Send the message, but delay to bypass SMTP restrictions on emails per minute
            //  Uncomment to enable SMTP email rate bypass
            //  Task.Delay(1000).ContinueWith(_ =>
            //  {
            //      smtpClient.Send(message);               
            //  });            

            //smtpClient.Send(message);
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                smtpClient.Send(message);
            });
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Run(() =>                            
                Send(message.Destination, message.Body, message.Subject));
        }
    }
}