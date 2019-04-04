using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace BL19.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration _config;

        public async Task SendEmailAsync(string email, string subject, string emailBody)
        {
            var message = new MailMessage
            {
                From = new MailAddress("billluckett@hotmail.com"),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));

            using (var smtp = new SmtpClient("smtp.live.com", 587))
            {
                smtp.Credentials = new NetworkCredential("billluckett@hotmail.com", _config["EmailPassword"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
    }

}
