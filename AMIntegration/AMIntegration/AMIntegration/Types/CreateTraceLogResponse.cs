namespace AMIntegration
{ 
    using Newtonsoft.Json;


    /// <summary>
    /// Represents ReadEntryListResponse class
    /// </summary>
    public class CreateTraceLogResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

    }
}
