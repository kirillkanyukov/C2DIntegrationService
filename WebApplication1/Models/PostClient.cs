using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public partial class PostClient
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("transport")]
        public string Transport { get; set; }
    }

    public partial class PostClient
    {
        public static PostClient FromJson(string json) => JsonConvert.DeserializeObject<PostClient>(json, QuickType.Converter.Settings);
    }
}