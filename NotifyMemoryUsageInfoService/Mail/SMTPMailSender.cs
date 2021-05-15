using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public class SMTPMailSender : IMailService
    {
        private ILogger _logger { get; }

        public SMTPMailSender(ILogger logger)
        {
            _logger = logger;
        }

        public bool SendMail(string mailBodyText)
        {
            bool isMailSend = false;
            SmtpClient smtpClient = new SmtpClient(ConstantFields.MailInfo.Host)
            {
                Port = ConstantFields.MailInfo.Port,
                Credentials = new NetworkCredential(ConstantFields.MailInfo.Mail, ConstantFields.MailInfo.Password),
                EnableSsl = true
            };

            MailMessage message = new MailMessage()
            {
                From = new MailAddress(ConstantFields.MailInfo.Mail),
                Subject = ConstantFields.SystemText.MailSubject,
                Body = mailBodyText,
                IsBodyHtml = true
            };

            IList<string> mailListToBeSent = ConstantFields.MailListToSend;
            if (mailListToBeSent.IsNotNullOrNotEmpty())
            {
                mailListToBeSent.ToList().ForEach(mail =>
                {
                    message.To.Add(mail);
                });

                try
                {
                    smtpClient.Send(message);
                    _logger.WriteToFile(ConstantFields.SystemText.NotificationText + Environment.NewLine);
                    isMailSend = true;
                }
                catch (Exception e)
                {
                    _logger.WriteToFile(e.ToString() + Environment.NewLine);
                }
            }
            else
            {
                _logger.WriteToFile(ConstantFields.SystemText.CheckMailListToSend);
            }

            return isMailSend;
        }

    }
}
