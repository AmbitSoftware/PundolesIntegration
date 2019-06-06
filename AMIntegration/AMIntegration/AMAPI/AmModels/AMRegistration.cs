
using Newtonsoft.Json;


namespace AMIntegration.AMCustomer
{
    public class AMRegistration : EntityBase
    {
        
        [JsonProperty(PropertyName = "customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty(PropertyName = "paddle")]
        public string Paddle { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string Created_at { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public AMCustomer Customer { get; set; }

        

    }
}