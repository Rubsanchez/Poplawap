using Microsoft.AspNetCore.Identity;
using Poplawap.Backend.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace Poplawap.Backend.Helpers
{
    public static class Utils
    {
        public static Response GetResponse(string code, string description)
        {
            return new Response { Code = code, Description = description };

        }

        public static Response GetResponseByErrorList(IEnumerable<IdentityError> identityErrors)
        {
            List<IdentityError> errorList = new List<IdentityError>();
            foreach (var error in identityErrors)
            {
                errorList.Add(error);
            }

            return new Response { Code = errorList[0].Code, Description = errorList[0].Description };
        }

        public static void SendEmail(string emailAddress, string link)
        {
            #region formatter
            string text = string.Format("Please click on this link to {0}: {1}", emailAddress, link);
            string html = "Please confirm your account by clicking this link: <a href=\"" + link + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + link);
            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("soporte.poplawap@gmail.com");
            msg.To.Add(new MailAddress(emailAddress));
            msg.Subject = "Confirm your email";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("soporte.poplawap@gmail.com", "@Poplawap19@");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
