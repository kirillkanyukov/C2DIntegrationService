using Microsoft.Pfe.Xrm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Web.Configuration;
using WebApplication1.Models;
namespace WebApplication1.CRM
{
    public class CrmHelper
    {
        static IOrganizationService _service;        

        public CrmHelper()
        {
            GetOrganizationServiceProxy();
        }

        public void ProcessMessage(WebHookRequestBody request)
        {
            EntityReference chat = GetChat(request.DialogId);
            if (chat == null)
                chat = CreateChat(request);

            EntityReference message = GetMessage(request.MessageId);
            if (message == null)
                CreateMessage(chat, request);

            
        }

        private EntityReference GetMessage(long messageId)
        {
            var query = new QueryExpression(EntityNames.Message);
            query.ColumnSet = new ColumnSet(MessageAttributes.Id);
            query.Criteria = new FilterExpression()
            {
                Conditions =
                {
                    new ConditionExpression(MessageAttributes.Chat2DeskId, ConditionOperator.Equal, messageId.ToString())
                }
            };
            var result = _service.RetrieveMultiple(query).Entities;
            return result.Count == 0 ? null : result.FirstOrDefault().ToEntityReference();
        }


        private EntityReference CreateChat(WebHookRequestBody request)
        {
            var newChat = new Entity(EntityNames.Chat);

            newChat[ChatAttributes.Chat2DeskClientId] = request.ClientId.ToString();
            newChat[ChatAttributes.Chat2DeskOperatorId] = request.OperatorId.ToString();
            newChat[ChatAttributes.ContactId] = GetContact(request.ClientId.ToString());
            newChat[ChatAttributes.OperatorId] = GetSystemUser(request.OperatorId.ToString());

            return new EntityReference(EntityNames.Chat, _service.Create(newChat));
        }

        private EntityReference GetContact(string clientId)
        {
            var query = new QueryExpression(EntityNames.Contact);
            query.ColumnSet = new ColumnSet(ContactAttributes.Id);
            query.Criteria = new FilterExpression()
            {
                Conditions =
                {
                    new ConditionExpression(ContactAttributes.Chat2DeskClientId, ConditionOperator.Equal, clientId)
                }
            };
            var result = _service.RetrieveMultiple(query).Entities;
            return result.Count == 0 ? null : result.FirstOrDefault().ToEntityReference();
        }
        private EntityReference GetSystemUser(string operatorId)
        {
            var query = new QueryExpression(EntityNames.SystemUser);
            query.ColumnSet = new ColumnSet(SystemUserAttributes.Id);
            query.Criteria = new FilterExpression()
            {
                Conditions =
                {
                    new ConditionExpression(SystemUserAttributes.Chat2DeskOperatorId, ConditionOperator.Equal, operatorId)
                }
            };
            var result = _service.RetrieveMultiple(query).Entities;
            return result.Count == 0 ? null : result.FirstOrDefault().ToEntityReference();
        }

        private EntityReference GetChat(long chatId)
        {
            var query = new QueryExpression(EntityNames.Chat);
            query.ColumnSet = new ColumnSet(ChatAttributes.Id);
            query.Criteria = new FilterExpression()
            {
                Conditions =
                {
                    new ConditionExpression(ChatAttributes.Chat2DeskId, ConditionOperator.Equal, chatId.ToString())
                }
            };
            var result = _service.RetrieveMultiple(query).Entities;
            return result.Count == 0 ? null : result.FirstOrDefault().ToEntityReference();
        }
        

        public void CreateMessage(EntityReference chat, WebHookRequestBody requestBody)
        {

            int transport;
            switch (requestBody.Transport)
            {
                case "telegram":
                    transport = Transports.Telegram;
                    break;
                case "viber":
                    transport = Transports.Viber;
                    break;
                case "sms":
                    transport = Transports.SMS;
                    break;
                default:
                    transport = Transports.Other;
                    break;
            }

            int type;
            switch (requestBody.Type)
            {
                case "from_client":
                    type = Types.Incoming;
                    break;
                case "to_client":
                    type = Types.Outgoing;
                    break;
                default:
                    type = Types.System;
                    break;
            }

            var newMessage = new Entity(EntityNames.Message);

            newMessage[MessageAttributes.Text] = requestBody.Text;
            newMessage[MessageAttributes.ChatId] = chat;
            newMessage[MessageAttributes.Direction] =  new OptionSetValue(type);
            newMessage[MessageAttributes.Transport] = new OptionSetValue(transport);
            newMessage[MessageAttributes.EventTime] = requestBody.EventTime.Date;

            _service.Create(newMessage);
        }

        public void UpdateContactWithClientId(string contactId, string clientId)
        {
            var updContact = new Entity(EntityNames.Contact) { Id = new Guid(contactId) };
            updContact[ContactAttributes.Chat2DeskClientId] = clientId;

            _service.Update(updContact);
        }

        private static void GetOrganizationServiceProxy()
        {
            try
            {
                
                var userName = WebConfigurationManager.AppSettings["crmUsername"];   // User Name
                var password = WebConfigurationManager.AppSettings["crmPassword"];                          // Password

                CRMConnect(userName, password, WebConfigurationManager.AppSettings["crmUrl"]);
                
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
                throw ex;
            }
        }


    }
}