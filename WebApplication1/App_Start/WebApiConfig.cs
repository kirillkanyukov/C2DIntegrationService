using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            SetChat2DeskWebhook();
        }

        private static void SetChat2DeskWebhook()
        {
            var chat2deskToken = WebConfigurationManager.AppSettings["chat2deskToken"];

            var serviceUrl = WebConfigurationManager.AppSettings["serviceUrl"];            

            using (WebClient client = new WebClient())
            {

                var isWebhookFound = false;

                client.Encoding = Encoding.UTF8;
                client.Headers["Content-type"] = "application/json";
                client.Headers["Authorization"] = chat2deskToken;
                var urlString = "https://api.chat2desk.com/v1/webhooks";

                var responseJson = client.DownloadString(urlString);

                var response = GetWebhooks.FromJson(responseJson);

                if (response != null && response.Data != null)
                {
                    foreach (var webhook in response.Data)
                    {

                        if (webhook.Url.AbsoluteUri == serviceUrl + "/api/webhook")
                            isWebhookFound = true;

                    }

                }

                if (!isWebhookFound)
                {

                    var webhook = new Webhook()
                    {
                        Url = new Uri(serviceUrl + "/api/webhook"),
                        Name = "webhook1",
                        Events = new string[] {
                        "inbox",
                        "outbox",
                        "new_client",
                        "add_tag_to_client",
                        "add_tag_to_request",
                        "delete_tag_from_client",
                        "close_dialog",
                        "close_request",
                        "new_qr_code"
                        }
                    };

                    client.Encoding = Encoding.UTF8;
                    client.Headers["Content-type"] = "application/json";
                    client.Headers["Authorization"] = chat2deskToken;                    

                    var postResponseJson = client.UploadString(urlString, "POST", webhook.ToJson());

                    var responsePost = GetWebhooksResponse.FromJson(responseJson);

                    if (responsePost.Status == "error")
                        throw new Exception("При настройке взаимодействия с Chat2Desk возникла ошибка: " + responsePost.Message);

                }
            }
        }
    }
}
