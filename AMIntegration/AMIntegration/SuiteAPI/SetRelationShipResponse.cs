namespace AMIntegration
{
    using Newtonsoft.Json;
   

    public class SetRelationShipResponse
    {
        [JsonProperty(PropertyName = "created")]
        public bool IsCreated { get; set; }

        [JsonProperty(PropertyName = "failed")]
        public bool IsFailed { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Isdeleted { get; set; }
    }
}
