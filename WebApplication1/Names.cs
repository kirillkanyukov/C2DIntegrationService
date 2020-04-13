using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public static class EntityNames
    {
        public const string Chat = "new_chat2deskchat";

        public const string Message = "new_chat2deskmessage";

        public const string SystemUser = "systemuser";

        public const string Contact = "contact";
    }

    public static class ChatAttributes
    {
        public const string Id = "new_chat2deskchatid";

        public const string Chat2DeskId = "new_chat2deskid";

        public const string OperatorId = "new_operatorid";

        public const string Chat2DeskOperatorId = "new_chat2deskoperatorid";

        public const string ContactId = "new_contactid";

        public const string Chat2DeskClientId = "new_chat2deskclientid";
    }

    public static class MessageAttributes
    {
        public const string ChatId = "new_chatid";

        public const string Transport = "new_transport";

        public const string Direction = "new_type";

        public const string Text = "new_text";

        public const string EventTime = "new_eventtime";
    }

    public static class SystemUserAttributes
    {
        public const string Id = "systemuserid";
        public const string Chat2DeskOperatorId = "new_chat2deskoperatorid";
    }

    public static class ContactAttributes
    {
        public const string Id = "contactid";
        public const string Chat2DeskClientId = "new_chat2deskclientid";        
    }






    public static class Transports
    {
        public const int Viber = 100000001;
        public const int Telegram = 100000000;
        public const int SMS = 100000002;
        public const int Other = 100000003;
    }
    public static class Types
    {
        public const int Incoming = 100000000;
        public const int Outgoing = 100000001;
        public const int System = 100000002;
    }
}