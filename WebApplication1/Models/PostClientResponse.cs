using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public partial class PostClientResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("assigned_name")]
        public object AssignedName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("avatar")]
        public object Avatar { get; set; }

        [JsonProperty("region_id")]
        public object RegionId { get; set; }

        [JsonProperty("country_id")]
        public long CountryId { get; set; }

        [JsonProperty("first_client_message")]
        public string FirstClientMessage { get; set; }

        [JsonProperty("last_client_message")]
        public string LastClientMessage { get; set; }
    }

    public partial class PostClientResponse
    {
        public static PostClientResponse FromJson(string json) => JsonConvert.DeserializeObject<PostClientResponse>(json, Converter.Settings);
    }
}