using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public partial class PostMessageChat2DeskResponse : WebRequest
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("message_id")]
        public long MessageId { get; set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }

        [JsonProperty("operator_id")]
        public long OperatorId { get; set; }

        [JsonProperty("transport")]
        public string Transport { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("dialog_id")]
        public object DialogId { get; set; }

        [JsonProperty("request_id")]
        public long RequestId { get; set; }
    }
    public partial class PostMessageChat2DeskResponse
    {
        public static PostMessageChat2DeskResponse FromJson(string json) => JsonConvert.DeserializeObject<PostMessageChat2DeskResponse>(json, Converter.Settings);
    }
}