﻿using Microsoft.Pfe.Xrm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
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

            CreateMessage(chat, request);

            
        }


        private EntityReference CreateChat(WebHookRequestBody request)
        {
            var newChat = new Entity(EntityNames.Chat);

            newChat[ChatAttributes.Chat2DeskClientId] = request.ClientId;
            newChat[ChatAttributes.Chat2DeskOperatorId] = request.OperatorId;
            newChat[ChatAttributes.ContactId] = GetContact(request.ClientId);
            newChat[ChatAttributes.OperatorId] = GetSystemUser(request.OperatorId);

            return new EntityReference(EntityNames.Chat, _service.Create(newChat));
        }

        private EntityReference GetContact(long clientId)
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
        private EntityReference GetSystemUser(long operatorId)
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
                    new ConditionExpression(ChatAttributes.Chat2DeskId, ConditionOperator.Equal, chatId)
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

            int direction;
            switch (requestBody.Type)
            {
                case "from_client":
                    direction = Directions.Incoming;
                    break;
                case "to_client":
                    direction = Directions.Outgoing;
                    break;
                default:
                    direction = Directions.Incoming;
                    break;
            }

            var newMessage = new Entity(EntityNames.Message);

            newMessage[MessageAttributes.Text] = requestBody.Text;
            newMessage[MessageAttributes.ChatId] = chat;
            newMessage[MessageAttributes.Direction] = new OptionSetValue(direction);
            newMessage[MessageAttributes.Transport] = new OptionSetValue(transport);

            _service.Create(newMessage);
        }

        private static void GetOrganizationServiceProxy()
        {
            try
            {
                
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