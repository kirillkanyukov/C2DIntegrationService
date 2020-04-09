using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OutgoingMessage : WebRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("transport")]
        public string Transport { get; set; }

        [JsonProperty("operator_id")]
        public string OperatorId { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("contact_id")]
        public string ContactId { get; set; }
    }
    
}