using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMSIntegration.CMSModels
{
    public class PullAuctionCalendar
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "auction_title_c")]
        public string AuctionTitle { get; set; }

        [JsonProperty(PropertyName = "auction_number_c")]
        public string AuctionNumber { get; set; }

        [JsonProperty(PropertyName = "start_date_c")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date_c")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "city_description_c")]
        public string CityDescription { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }

    public class PushAuctionCalendar
    {
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "auction_title_c")]
        public string AuctionTitle { get; set; }

        [JsonProperty(PropertyName = "auction_number_c")]
        public string AuctionNumber { get; set; }

        [JsonProperty(PropertyName = "start_date_c")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date_c")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "city_description_c")]
        public string CityDescription { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }

    public class AuctionCalendar
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "date_entered")]
        public DateTime? DateEntered { get; set; }

        [JsonProperty(PropertyName = "date_modified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty(PropertyName = "modified_user_id")]
        public string ModifiedUserId { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public string Deleted { get; set; }

        [JsonProperty(PropertyName = "assigned_user_id")]
        public string AssignedUserId { get; set; }

        [JsonProperty(PropertyName = "auction_type_c")]
        public string AuctionType { get; set; }

        [JsonProperty(PropertyName = "auction_title_c")]
        public string AuctionTitle { get; set; }

        [JsonProperty(PropertyName = "start_date_c")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date_c")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "viewing_c")]
        public string Viewing { get; set; }

        [JsonProperty(PropertyName = "city_description_c")]
        public string CityDescription { get; set; }

        [JsonProperty(PropertyName = "buyer_commission_in_c")]
        public string BuyerCommission { get; set; }

        [JsonProperty(PropertyName = "auction_number_c")]
        public string AuctionNumber { get; set; }

        [JsonProperty(PropertyName = "invoice_description_c")]
        public string InvoiceDescription { get; set; }

        [JsonProperty(PropertyName = "am_customer_id_c")]
        public string AmCustomer { get; set; }

        [JsonProperty(PropertyName = "currency_type_c")]
        public string CurrencyType { get; set; }

    }
}