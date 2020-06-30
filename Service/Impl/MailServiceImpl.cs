using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Service.Impl
{
    public class MailServiceImpl : IMailService
    {
        public void Send(string to, string subject, string body)
        {
            // TODO: add params to config
            SmtpClient client = new SmtpClient
            {
                Host = "mail@host.com",
                Port = 1234,
                Credentials = new NetworkCredential("username", "password")
            };

            client.SendMailAsync(new MailMessage(
                from: "jane@contoso.com",
                to,
                subject,
                body
            ));
        }
    }
}
