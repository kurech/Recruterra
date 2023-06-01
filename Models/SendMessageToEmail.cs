using System.Net;
using System.Net.Mail;

namespace WebApplication2.Models
{
    public class SendMessageToEmail
    {
        public static bool SendMessage(string email, string subject, string body)
        {
            try
            {
                MailAddress from = new MailAddress("recruterra@mail.ru", "Recruterra");
                MailAddress to = new MailAddress(email);

                MailMessage message = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("recruterra@mail.ru", "WkEXb6t8ULG0u7rVRbwj");
                smtp.EnableSsl = true;

                smtp.SendMailAsync(message);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
