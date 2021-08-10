﻿namespace Oversteer.Services.EmailSenders
{
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using MailKit.Security;

    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Options;

    using MimeKit;
    using MimeKit.Text;

    using Oversteer.Web.ViewModels.Email;

    public class MailKitSender : IEmailSender
    {
        public MailKitSender(IOptions<MailKitEmailSenderOptions> options)
        {
            this.Options = options.Value;
        }

        public MailKitEmailSenderOptions Options { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return this.Execute(email, subject, message);
        }

        public Task Execute(string to, string subject, string message)
        {
            // create message
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(Options.SenderEmail)
            };

            if (!string.IsNullOrEmpty(Options.SenderName))
            {
                email.Sender.Name = Options.SenderName;
            }

            email.From.Add(email.Sender);

            email.To.Add(MailboxAddress.Parse(to));

            email.Subject = subject;

            email.Body = new TextPart(TextFormat.Html) { Text = message };

            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(Options.HostAddress, Options.HostPort, SecureSocketOptions.Auto);

                smtp.Authenticate(Options.HostUsername, Options.HostPassword);

                smtp.Send(email);

                smtp.Disconnect(true);
            }

            return Task.FromResult(true);
        }
    }
}
