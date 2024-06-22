using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Helpers.Mail;
using SendGrid;

public class SendGridEmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SendGridEmailSender> _logger;


    public SendGridEmailSender(IConfiguration configuration, ILogger<SendGridEmailSender> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        string sendGridApiKey = _configuration["SendGrid:ApiKey"];
        if (string.IsNullOrEmpty(sendGridApiKey))
        {
            throw new Exception("The 'SendGrid:ApiKey' is not configured");
        }

        var client = new SendGridClient(sendGridApiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(***REMOVED***, ***REMOVED***),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        var response = await client.SendEmailAsync(msg);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Email queued successfully");
        }
        else
        {
            _logger.LogError("Failed to send email");
        }
    }

}
