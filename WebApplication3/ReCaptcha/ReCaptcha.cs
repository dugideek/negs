using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;

namespace WebApplication3.ReCaptcha
{
    public abstract class ReCaptcha
    {
        public static bool validate(string gRecCaptchaResponse)
        {
            string response = gRecCaptchaResponse;
            string secretKey = WebConfigurationManager.AppSettings["ReCaptcha:SecretKey"];
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            return status;
        }
    }
}