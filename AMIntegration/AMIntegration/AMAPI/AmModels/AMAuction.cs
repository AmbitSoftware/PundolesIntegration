using System;
using Newtonsoft.Json;


namespace AMIntegration.AMCustomer
{
    public class AMAuction : EntityBase
    {
        [JsonProperty(PropertyName = "row_id")]
        public string Row_id { get; set; }

        [JsonProperty(PropertyName = "time_start")]
        public string Time_start { get; set; }

        [JsonProperty(PropertyName = "integration_id")]
        public string SuiteId { get; set; }
              
    }
}