using Newtonsoft.Json;

namespace AMIntegration.SugarCrmModels
{
    public class GetRelationshipsResponse : EntityBase
    {
        [JsonProperty(PropertyName = "id")]
        public string DocumentID { get; set; }
       
    }
}