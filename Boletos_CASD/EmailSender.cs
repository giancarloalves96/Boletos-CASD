using System.Net.Mail;
using System.Configuration;

namespace Boletos_CASD
{
    public class EmailSender
    {
        private EmailSender() { }

        public static void sendMail(string fileName, string destinationMail, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["emailUser"],
                ConfigurationManager.AppSettings["emailName"], System.Text.Encoding.UTF8);
            mail.To.Add(new MailAddress(destinationMail));
            mail.Subject = subject;
            mail.Body = body;
            mail.Attachments.Add(new Attachment(fileName));

            SmtpClient mailClient = new SmtpClient(ConfigurationManager.AppSettings["emailServer"],
                int.Parse(ConfigurationManager.AppSettings["emailServerPort"]));
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["emailUser"], 
                ConfigurationManager.AppSettings["emailPassword"], ConfigurationManager.AppSettings["emailDomain"]);
            mailClient.Timeout = 300000;

            mailClient.Send(mail);
        }
    }
}
