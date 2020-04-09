using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public partial class GetWebhooks : WebRequest
    {
        [JsonProperty("data")]
        public Webhook[] Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Webhook : WebRequest
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("events")]
        public string[] Events { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errors")]
        public Error[] Errors { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }
    }

    public partial class GetWebhooks
    {
        public static GetWebhooks FromJson(string json) => JsonConvert.DeserializeObject<GetWebhooks>(json, Converter.Settings);
    }
}