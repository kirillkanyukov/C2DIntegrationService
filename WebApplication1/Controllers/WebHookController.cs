using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class WebHookController : ApiController
    {
        string _serverUrl = WebConfigurationManager.AppSettings["serviceUrl"];

        // POST: api/WebHook
        public void Post([FromBody]WebHookRequestBody value)
        {

            var crmHelper = new CRM.CrmHelper();

            switch (value.HookType)
            {

                case "inbox":
                case "outbox":
                    //Создать сообщение в CRM
                    crmHelper.ProcessMessage(value);
                    //Отобразить сообщение на вебресурсе через SignalR

                    var hubConnection = new HubConnection(_serverUrl);
                    IHubProxy logHubProxy = hubConnection.CreateHubProxy("chat2DeskHub");
                    hubConnection.Start().Wait();
                    logHubProxy.Invoke("Send", value.Text,
                        DateTime.Now.ToLocalTime().ToString());
                    break;

            }

        }

    }
}
