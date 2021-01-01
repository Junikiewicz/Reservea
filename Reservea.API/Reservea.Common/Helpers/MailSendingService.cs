using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Reservea.Common.Interfaces;
using Reservea.Common.Mails.Models;
using System;
using System.Threading.Tasks;

namespace Reservea.Common.Helpers
{
    public class MailSendingService : IMailSendingService
    {
        private readonly IConfiguration _configuration;
        private readonly IMailTemplatesHelper _mailTemplatesHelper;

        public MailSendingService(IConfiguration configuration, IMailTemplatesHelper mailTemplatesHelper)
        {
            _configuration = configuration;
            _mailTemplatesHelper = mailTemplatesHelper;
        }

        public async Task SendMailFromTemplateAsync(string template, BaseMailModel baseMailModel)
        {
            var templateString = _mailTemplatesHelper.GetTemplateString(template, baseMailModel);

            await SendMailAsync(baseMailModel.To, baseMailModel.ToAddress, baseMailModel.Subject, templateString);
        }

        public async Task SendMailAsync(string to, string toAddress, string subject, string messageContent)
        {
            string fromAddress = _configuration["EmailSettings:FromAddress"];
            string from = _configuration["EmailSettings:From"];

            string serverAddress = _configuration["EmailSettings:ServerAddress"];
            string username = _configuration["EmailSettings:Username"];
            string password = _configuration["EmailSettings:Password"];

            int port = Convert.ToInt32(_configuration["EmailSettings:Port"]);
            bool isUseSsl = Convert.ToBoolean(_configuration["EmailSettings:IsUseSsl"]);


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from, fromAddress));
            message.To.Add(new MailboxAddress(to, toAddress));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = messageContent
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(serverAddress, port, isUseSsl);

                await client.AuthenticateAsync(username, password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
