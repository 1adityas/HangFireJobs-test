using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;

namespace HangFireLearn.Services
{
    public static class MailSender
    {
        public static void SendSingleMail()
        {
            //DateTime now = DateTime.Now;
            // complete the message. message is deleted from the queue. 

            string fromMail = "prudhvicharantest@gmail.com";
            string fromPassword = "gibvkznsxzjtmegj";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "common subject";
            message.To.Add(new MailAddress("adityapra.kingofworld@gmail.com"));
            message.Body = "mail body";
            message.IsBodyHtml = false;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                //Port = 465,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

        }

    }
}
