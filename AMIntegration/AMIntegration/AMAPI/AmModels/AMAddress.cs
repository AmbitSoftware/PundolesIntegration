using System;
using Newtonsoft.Json;


namespace AMIntegration.AMCustomer
{
    public class AMAddress : EntityBase
    {
        [JsonProperty(PropertyName = "street_address")]
        public string Street_address { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public string Country_code { get; set; }

        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "postal_code")]
        public string Postal_code { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

      
    }
}