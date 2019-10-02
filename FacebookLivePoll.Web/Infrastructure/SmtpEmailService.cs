using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace FacebookLivePoll.Web.Infrastructure
{
    public class SmtpEmailService : IIdentityMessageService
    {
        string credentialUserName = "SMTP_Injection";
        string sentFrom = "no-reply@cognitolearning.com";
        string pwd = "95a73bfbba418a73a2ae863e9350bf4478f6c35d";
        string server = "smtp.sparkpostmail.com";

        public async Task SendAsync(IdentityMessage message)
        {
            // Configure the client:
            SmtpClient client =
                new SmtpClient(server);

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                //new System.Net.Mail.MailMessage(sentFrom, message.Destination);
                new System.Net.Mail.MailMessage();
           
            mail.From = new MailAddress(sentFrom);
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.IsBodyHtml = true;
            mail.Body = message.Body;

            // Send:
            await client.SendMailAsync(mail);
        }

        public void Send(IdentityMessage message)
        {
            // Configure the client:
            SmtpClient client =
                new SmtpClient(server);

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                //new System.Net.Mail.MailMessage(sentFrom, message.Destination);
                new System.Net.Mail.MailMessage();

            mail.From = new MailAddress(sentFrom);
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.IsBodyHtml = true;
            mail.Body = message.Body;

            // Send:
            client.Send(mail);
        }

    }
}