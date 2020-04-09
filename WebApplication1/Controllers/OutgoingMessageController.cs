using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class OutgoingMessageController : ApiController
    {        

        // POST api/<controller>
        public void Post([FromBody]OutgoingMessage value)
        {
            ProcessOutgoingMessageChat2Desk(value);
            //var test = value.ToJson();
        }

        private void ProcessOutgoingMessageChat2Desk(OutgoingMessage request)
        {

            var chat2deskToken = WebConfigurationManager.AppSettings["chat2deskToken"];

            if (request.ClientId != null)
            {

                using (WebClient client = new WebClient())
                {

                    client.Encoding = Encoding.UTF8;
                    client.Headers["Content-type"] = "application/json";
                    client.Headers["Authorization"] = chat2deskToken;
                    var urlString = "https://api.chat2desk.com/v1/messages";

                    var responseJson = client.UploadString(urlString, "POST", request.ToJson());

                    var response = PostMessageChat2DeskResponse.FromJson(responseJson);

                }

            } else
            {
                //post client with phone
                if (request.Phone != null && request.ContactId != null)
                {
                    using (WebClient client = new WebClient())
                    {

                        var postClient = new PostClient()
                        {
                            Phone = request.Phone,
                            Transport = request.Transport
                        };

                        client.Encoding = Encoding.UTF8;
                        client.Headers["Content-type"] = "application/json";
                        client.Headers["Authorization"] = chat2deskToken;
                        var urlString = "https://api.chat2desk.com/v1/clients";

                        var responseJson = client.UploadString(urlString, "POST", postClient.ToJson());

                        var response = PostMessageChat2DeskResponse.FromJson(responseJson);

                    }
                }
            }
        }

    }
}