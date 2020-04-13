using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System;
using System.Net;
using System.Text;
using System.Web.Configuration;
using WebApplication1.Models;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.MapSignalR(new HubConfiguration
            {
                EnableJSONP = true
            });
        }

    }
}