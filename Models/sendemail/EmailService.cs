using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
namespace WIRKDEVELOPER.Models.sendemail
{
   
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        // Constructor that accepts IConfiguration for SMTP settings
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Send email method using MailKit
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Retrieve SMTP settings from appsettings.json
            var smtpSettings = _configuration.GetSection("Smtp");

            // Create a new email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Application", smtpSettings["Username"]));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            // Email body content
            message.Body = new TextPart("html")
            {
                Text = body
            };

            // Use MailKit's SmtpClient to send the email
            using (var client = new SmtpClient())
            {
                // Connect to the SMTP server
                await client.ConnectAsync(smtpSettings["Host"], int.Parse(smtpSettings["Port"]), MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate using SMTP credentials
                await client.AuthenticateAsync(smtpSettings["Username"], smtpSettings["Password"]);

                // Send the email
                await client.SendAsync(message);

                // Disconnect from the SMTP server
                await client.DisconnectAsync(true);
            }
        }
    }


}
