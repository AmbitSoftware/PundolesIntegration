using System;
using Newtonsoft.Json;


namespace AMIntegration.AMCustomer
{
    public class AMAuctionSummary : EntityBase
    {
        [JsonProperty(PropertyName = "row_id")]
        public string Row_id { get; set; }

        [JsonProperty(PropertyName = "auction_type")]
        public string Auction_type { get; set; }

        [JsonProperty(PropertyName = "time_start")]
        public string Time_start { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }

        [JsonProperty(PropertyName = "auction_code")]
        public string Auction_code { get; set; }

        [JsonProperty(PropertyName = "truncated_description")]
        public string Truncated_description { get; set; }

        [JsonProperty(PropertyName = "viewing_information")]
        public string Viewing_information { get; set; }

        [JsonProperty(PropertyName = "currency_code")]
        public string Currency_code { get; set; }

        [JsonProperty(PropertyName = "location_name")]
        public string Location_name { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}