﻿using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace ECommerce516.Utitlity
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mohamedashrafmahmoudgad@gmail.com", "gsje pjnh amaf ehkt")
            };

            return client.SendMailAsync(
        new MailMessage(from: "mohamedashrafmahmoudgad@gmail.com",
                        to: email,
                        subject,
                        htmlMessage
                        )
        {
            IsBodyHtml = true
        });
        }
    }
}
