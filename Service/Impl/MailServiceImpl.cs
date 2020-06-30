using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Service.Impl
{
    public class MailServiceImpl : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailServiceImpl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient
            {
                Host = _configuration["Mail:Host"],
                Port = int.Parse(_configuration["Mail:Port"]),
                Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"])
            };

            client.SendMailAsync(new MailMessage(
                from: _configuration["Mail:Sender"],
                to,
                subject,
                body
            ));
        }
    }
}
