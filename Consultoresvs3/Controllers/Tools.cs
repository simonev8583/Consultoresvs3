using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Consultoresvs3.Controllers
{
    public class Tools
    {
        // ojo :https://code.msdn.microsoft.com/Envio-de-Correos-con-81b6eb3b
        public void SendEmail(string EmailAddress, string txtSubject, string txtMessage)
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(EmailAddress);
            mail.From = new MailAddress("simone.villa18583@hotmail.com");
            mail.Subject = txtSubject;
            mail.Body = txtMessage;


            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("simone.villa18583@hotmail.com", "proptab");
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
    }
}