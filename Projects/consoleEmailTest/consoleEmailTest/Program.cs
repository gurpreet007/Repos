using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
namespace consoleEmailTest
{
    class Program
    {
        private const string EMAIL_GURPREET = "gurpreet007@gmail.com";
        private const string EMAIL_JSIR = "jasvirsaini2002@yahoo.com";
        private const string FROM_ADDRESS = "seitpspcl@gmail.com";
        private const string DISP_NAME = "PSPCL";
        private bool SendEmail(string fromAdd, string dispName, string toAdd, string subject, string body, Attachment attachedFile = null)
        {
            MailAddressCollection addColl = new MailAddressCollection();
            addColl.Add(toAdd);
            return SendEmail(fromAdd, dispName, addColl, subject, body, attachedFile);
        }
        private bool SendEmail(string fromAdd, string dispName, MailAddressCollection toAdds, string subject, string body, Attachment attachedFile = null)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(fromAdd, dispName);
            foreach (MailAddress toAdd in toAdds)
            {
                mailMsg.To.Add(toAdd);
            }
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            if (attachedFile != null)
            {
                mailMsg.Attachments.Add(attachedFile);
            }

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(mailMsg.From.Address, "pspcl123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMsg);
            return true;
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            MailAddressCollection toAdds = new MailAddressCollection();
            toAdds.Add(new MailAddress(EMAIL_GURPREET));
            toAdds.Add(new MailAddress(EMAIL_JSIR));
            bool retVal = p.SendEmail(FROM_ADDRESS, DISP_NAME, toAdds, "subject", "body", null);
            Console.WriteLine(retVal?"SUCCESS":"FAILED");
        }
    }
}
