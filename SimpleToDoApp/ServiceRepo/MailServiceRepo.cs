using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using SimpleToDoApp.Extensions.Configs;
using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.IServiceRepo;

namespace SimpleToDoApp.ServiceRepo;

public class MailServiceRepo: IMailServiceRepo
{
    private readonly EmailConfig _emailConfig;
    public MailServiceRepo(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }
    public async Task<IdentityResult> SendEmailAsync(EmailParams emailDTO)
    {
        try
        {
            var toEmail = emailDTO.recipientEmail;
            var subject = emailDTO.emailSubject;
            var body = emailDTO.emailBody;
            var smtpHost = _emailConfig.HOST;
            int smtpPort = _emailConfig.PORT;
            var userName = _emailConfig.USERNAME;
            var mailFrom = emailDTO.senderEmail;
            var smtpPassword = _emailConfig.PASSWORD;
            bool enableSsl = _emailConfig.ENABLESSL;
            var senderName = emailDTO.senderName;
            bool isHtml = emailDTO.isHtml;

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.EnableSsl = enableSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(userName, smtpPassword);
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mailFrom, senderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml,
                };
                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);

                return await Task.FromResult(IdentityResult.Success);
            }
        }
        catch (Exception ex)
        {
            var errorMsg = $"something happened trying to send email. Details: {ex.StackTrace}";
            var identityError = new IdentityError { Code = "500", Description = errorMsg };
            return IdentityResult.Failed(identityError);
        }
    }
}
