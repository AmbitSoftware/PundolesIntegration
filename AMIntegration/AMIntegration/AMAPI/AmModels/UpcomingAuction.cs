using Newtonsoft.Json;


namespace AMIntegration.AMCustomer
{
    public class UpcomingAuction : EntityBase
    {
        [JsonProperty(PropertyName = "row_id")]
        public string Auction_id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Auction_title { get; set; }

        [JsonProperty(PropertyName = "time_start")]
        public string Auction_startdate { get; set; }

     
    }
}