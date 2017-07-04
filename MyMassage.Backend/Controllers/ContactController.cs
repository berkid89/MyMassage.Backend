using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MyMassage.Backend.Models;
using MyMassage.Backend.Resources;

namespace MyMassage.Backend.Controllers
{
    public class ContactController : ControllerBase
    {
        public ContactController(ISettings settings) : base(settings) { }

        [HttpPost]
        public IActionResult Send([FromBody] Contact contact)
        {
            if (ModelState.IsValid)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(contact.Email));
                message.To.Add(new MailboxAddress(settings.SMTP_Address));
                message.Subject = Common.ContactMessageSubject;

                message.Body = new TextPart("html")
                {
                   Text = string.Format(Common.ContactMessageTemplate, contact.Name, contact.Email, contact.Message)
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(settings.SMTP_Host, settings.SMTP_Port, settings.SMTP_EnableSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(settings.SMTP_UserName, settings.SMTP_Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }

            return JResult(contact);
        }
    }
}
