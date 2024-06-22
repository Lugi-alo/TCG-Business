using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var smtpSettings = _configuration.GetSection("Smtp");

            using (var client = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"])))
            {
                client.Credentials = new NetworkCredential(smtpSettings["UserName"], smtpSettings["Password"]);
                client.EnableSsl = bool.Parse(smtpSettings["EnableSsl"]);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["UserName"]),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
        catch (Exception ex)
        {
            // Handle or log any exceptions
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw; // Re-throw to propagate the exception
        }
    }
}
