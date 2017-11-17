using System;
using System.Web.Mvc;
using System.Net.Mail;
using System.Web.Configuration;
using System.Net;
using WebApplication3.Models;
using System.Web.Http;
using WebApplication3.Attributes;
using Umbraco.Web.Mvc;
using System.Configuration;

namespace WebApplication3.Controllers
{
    public class MailController : SurfaceController
    {
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateAjax]
        public JsonResult SendMail([FromBody]ContactUsForm form)
        {
            var gReCaptchaResponse = Request["g-recaptcha-response"];

            if(!ReCaptcha.ReCaptcha.validate(gReCaptchaResponse))
            {
                Response.StatusCode = 400;
                return Json(new
                {
                    status = 400,
                    message = "ReCaptchaFailed"
                });
            }

            string host = WebConfigurationManager.AppSettings["SmtpHostName"];
            string senderEmail = WebConfigurationManager.AppSettings["SmtpSenderEmail"];
            string receivingEmail = WebConfigurationManager.AppSettings["SmtpReceivingEmail"];
            string username = WebConfigurationManager.AppSettings["SmtpUserName"];
            string password = WebConfigurationManager.AppSettings["SmtpPassword"];

            string body = Server.HtmlEncode($"The following message was submitted:\n\nFirstName: {form.FirstName}\nLastName: {form.LastName}\nEmail: {form.EmailAddress}\nPhone: {form.PhoneNumber}\nMessage:\n{form.Message}");

            SmtpClient client = new SmtpClient()
            {
                Host = host,
                Credentials = new NetworkCredential(username, password),
                Port = 25
            };

            MailMessage message = new MailMessage(new MailAddress(senderEmail), new MailAddress(receivingEmail))
            {
                Body = body,
                BodyEncoding = System.Text.Encoding.UTF8,
                Subject = "Website - Contact us form submission",
                SubjectEncoding = System.Text.Encoding.UTF8,
            };

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    status = 500,
                    message = $"Exception caught when trying to Send : {ex.ToString()}"
                });
            }

            return Json(new
            {
                status = 200
            });


        }
    }
}
