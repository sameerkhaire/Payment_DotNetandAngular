using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Payment_DotNetandAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult sendEmail([FromBody] string email)
        {
            var emails = new MimeMessage();
            emails.From.Add(MailboxAddress.Parse("Sameerkhaire7038@gmail.com"));
            emails.To.Add(MailboxAddress.Parse("Samk705711@gmail.com"));
            emails.Subject = "Testing a mail working or not!!";
            emails.Body = new TextPart(TextFormat.Html) { Text = email };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("sampleEmail@gmail.com", "Password");

            smtp.Send(emails);

            smtp.Disconnect(true);
            return Ok();
        }
    }
}
