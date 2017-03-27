
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using SF.Entitys.Abstraction;
using SF.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SF.Web.Extensions
{
    /// <summary>
    /// to use extensions from inside a controller you have to use the this keyword to call the extension method
    /// http://stackoverflow.com/questions/12105869/controller-extension-method-without-this
    /// </summary>
    public static class ControllerExtensions
    {
        public static async Task<RecaptchaResponse> ValidateRecaptcha(
            this Controller controller,
            HttpRequest request,
            string secretKey)
        {
            var response = request.Form["g-recaptcha-response"];
            using (var client = new HttpClient())
            {
                string result = await client.GetStringAsync(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                        secretKey,
                        response)
                        );

                var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(result);

                return captchaResponse;
            }       
        }

        public static bool SessionIsAvailable(this Controller controller)
        {
            var sessionFeature = controller.HttpContext.Features.Get<ISessionFeature>();
            return (sessionFeature != null);
        }

        public static void AlertSuccess(
            this Controller controller,
            string message,
            bool dismissable = true)
        {
            controller.AddAlert(AlertStyles.Success, message, dismissable);
        }

        public static void AlertInformation(
            this Controller controller,
            string message,
            bool dismissable = false)
        {
            controller.AddAlert(AlertStyles.Information, message, dismissable);
        }

        public static void AlertWarning(
            this Controller controller,
            string message,
            bool dismissable = false)
        {
            controller.AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public static void AlertDanger(
            this Controller controller,
            string message,
            bool dismissable = false)
        {
            controller.AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private static void AddAlert(
            this Controller controller,
            string alertStyle,
            string message,
            bool dismissable)
        {
            
            if (controller.SessionIsAvailable())
            {
                var alerts = controller.TempData.GetAlerts();

                alerts.Add(new Alert
                {
                    AlertStyle = alertStyle,
                    Message = message,
                    Dismissable = dismissable
                });

                controller.TempData.AddAlerts(alerts);
            }

        }

        public static void AddAlerts(this ITempDataDictionary tempData, List<Alert> alerts)
        {
            tempData[Alert.TempDataKey] = JsonConvert.SerializeObject(alerts);
        }

        public static List<Alert> GetAlerts(this ITempDataDictionary tempData)
        {
            if(tempData.ContainsKey(Alert.TempDataKey))
            {
                string json = (string)tempData[Alert.TempDataKey];
                List<Alert> alerts = JsonConvert.DeserializeObject<List<Alert>>(json);
                return alerts;
            }

            return new List<Alert>();
        }

 

    }
}
