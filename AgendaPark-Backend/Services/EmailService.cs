using AgendaPark_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace AgendaPark_Back.Services
{
    public class EmailService
    {
        static public async Task<bool> mandaEmail(Email configEMail)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("suporte@agendabpkedu.space"));
                email.To.Add(MailboxAddress.Parse(configEMail.destinatario));
                email.Subject = configEMail.assunto;
                email.Body = new TextPart(TextFormat.Plain) { Text = configEMail.corpo };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.hostinger.com", 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate("suporte@agendabpkedu.space", "Resetpb1@@@");
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (InvalidCastException erro)
            {
                return false;
            }
        }
    }
}
