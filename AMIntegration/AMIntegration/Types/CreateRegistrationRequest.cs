using Newtonsoft.Json;
using System.Xml.Serialization;


namespace AMIntegration
{
    public class CreateRegistrationRequest
    {
        [JsonProperty(PropertyName = "paddle_number_c")]
        public string Paddle_number_c { get; set; }

        [JsonProperty(PropertyName = "contacts_reg_registration_1contacts_ida"), XmlElement(IsNullable = true)]
        public string Contact_id { get; set; }

        [JsonProperty(PropertyName = "ac1_auction_calendar_id_c")]
        public string Ac1_auction_calendar_id_c { get; set; }


        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}