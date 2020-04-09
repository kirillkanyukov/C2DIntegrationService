using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp1
{
    class Program
    {
        static IOrganizationService _service;
        static void Main(string[] args)
        {
            GetOrganizationServiceProxy();

            UpsertChat("111");
        }
        public static EntityReference UpsertChat(string dialogid)
        {

            EntityReference chatReference = null;

            var chatQuery = new QueryExpression("new_chat2deskchat");
            chatQuery.Criteria = new FilterExpression()
            {
                Conditions =
                {
                    new ConditionExpression("new_chatid", ConditionOperator.Equal, dialogid)
                }
            };

            var results = _service.RetrieveMultiple(chatQuery).Entities;

            if (results.Count == 0)
            {

                var newChat = new Entity("new_chat2deskchat");
                newChat["new_chatid"] = dialogid;

                var createdNewChatSet = _service.Create(newChat);

                chatReference = new EntityReference("new_chat2deskchat", createdNewChatSet);

            }
            else
            {
                chatReference = results.FirstOrDefault().ToEntityReference();
            }

            return chatReference;
        }
        private static void GetOrganizationServiceProxy()
        {
            try
            {
                var orgName = "testdevs4";                            // CRM Organization Name
                var userName = "ivan666@testdevs5.onmicrosoft.com";   // User Name
                var password = "Appal00sa";                          // Password

                CRMConnect(userName, password, "https://testdevs5.api.crm.dynamics.com/XRMServices/2011/Organization.svc");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CRMConnect(string UserName, string Password, string OrgServiceUri)
        {
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = UserName;
                credentials.UserName.Password = Password;
                Uri serviceUri = new Uri(OrgServiceUri);
                OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
                proxy.EnableProxyTypes();
                _service = (IOrganizationService)proxy;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
