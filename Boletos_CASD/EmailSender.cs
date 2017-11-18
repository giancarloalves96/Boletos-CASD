using System.Net.Mail;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Boletos_CASD
{
	public static class EmailSender
	{
		public static void SendMailList(Dictionary<string, KeyValuePair<string, string>> nameMap, string subject, string body)
		{
			foreach (string name in nameMap.Keys)
			{
				SendMail(nameMap[name].Key, nameMap[name].Value, subject.ReplaceSpecialSequences(name),
					body.ReplaceSpecialSequences(name));
				Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["emailInterval"]));
			}
		}

		private static void SendMail(string fileName, string destinationMail, string subject, string body)
		{
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(MainWindow.emailUser, //ConfigurationManager.AppSettings["emailUser"],
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
			mailClient.Credentials = new System.Net.NetworkCredential(MainWindow.emailUser, //ConfigurationManager.AppSettings["emailUser"],
				MainWindow.password);// ConfigurationManager.AppSettings["emailPassword"]);//, ConfigurationManager.AppSettings["emailDomain"]);
			mailClient.Timeout = 120000;

            try
            {
                mailClient.Send(mail);
            }
            catch (SmtpException ex)
            {
                using (var output = new StreamWriter("emailErrors.txt", true))
                {
                    output.WriteLine("Não foi possível enviar email para " + destinationMail + ".");
                }
                Thread.Sleep(120000);
            }
        }
	}
}