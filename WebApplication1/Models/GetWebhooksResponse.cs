using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public partial class GetWebhooksResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public Errors Errors { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Errors
    {
        [JsonProperty("url")]
        public string[] Url { get; set; }
    }

    public partial class GetWebhooksResponse
    {
        public static GetWebhooksResponse FromJson(string json) => JsonConvert.DeserializeObject<GetWebhooksResponse>(json, Converter.Settings);
    }
}