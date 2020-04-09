using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace WebApplication1.Models
{
    public class WebHookRequestBody : WebRequest
    {
        [JsonProperty("message_id")]
        public long MessageId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("transport")]
        public string Transport { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("operator_id")]
        public long OperatorId { get; set; }

        [JsonProperty("dialog_id")]
        public long DialogId { get; set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }

        [JsonProperty("photo")]
        public object Photo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("audio")]
        public object Audio { get; set; }

        [JsonProperty("pdf")]
        public object Pdf { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("hook_type")]
        public string HookType { get; set; }

        [JsonProperty("request_id")]
        public long RequestId { get; set; }

        [JsonProperty("attachments")]
        public object[] Attachments { get; set; }

        [JsonProperty("is_new_request")]
        public bool IsNewRequest { get; set; }

        [JsonProperty("is_new_client")]
        public bool IsNewClient { get; set; }

        [JsonProperty("insta_comment")]
        public bool InstaComment { get; set; }

        [JsonProperty("extra_data")]
        public object ExtraData { get; set; }

        [JsonProperty("event_time")]
        public DateTimeOffset EventTime { get; set; }
    }

    public partial class Client
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("client_phone")]
        public object ClientPhone { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("assigned_name")]
        public object AssignedName { get; set; }

        [JsonProperty("external_id")]
        public object ExternalId { get; set; }
    }

    
}
