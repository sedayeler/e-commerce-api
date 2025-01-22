using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.From = new(_configuration["Mail:Username"], "E-Commerce", Encoding.UTF8);
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            SmtpClient smtp = new();
            smtp.Host = _configuration["Mail:Host"];
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = int.Parse(_configuration["Mail:Port"]);
            smtp.EnableSsl = bool.Parse(_configuration["Mail:EnableSsl"]);
            await smtp.SendMailAsync(mail);
        }
    }
}
