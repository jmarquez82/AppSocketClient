using System;
using Newtonsoft.Json;

namespace AppSocketClient
{
    public class Model
    {
        public Model()
        {
        }
    }

    public partial class GettingStarted
    {
        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("attributes")]
        public Attribute[] Attributes { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Attribute
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class GettingStarted
    {
        public static GettingStarted FromJson(string json) => JsonConvert.DeserializeObject<GettingStarted>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GettingStarted self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }

}
