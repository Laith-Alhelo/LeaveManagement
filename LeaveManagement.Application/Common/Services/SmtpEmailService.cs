using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Common.Interfaces;
namespace LeaveManagement.Common.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpClient _client;
        private readonly string _fromAddress;

        public SmtpEmailService(IConfiguration configuration)
        {
            // read from appsettings.json → "Smtp": { ... }
            var host = configuration["Smtp:Host"];
            var port = int.Parse(configuration["Smtp:Port"] ?? "25");
            var user = configuration["Smtp:User"];
            var pass = configuration["Smtp:Password"];
            _fromAddress = configuration["Smtp:From"];

            _client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(user, pass)
            };
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var mail = new MailMessage(_fromAddress, to, subject, body)
            {
                IsBodyHtml = false
            };

            await _client.SendMailAsync(mail);
        }
    }
}
