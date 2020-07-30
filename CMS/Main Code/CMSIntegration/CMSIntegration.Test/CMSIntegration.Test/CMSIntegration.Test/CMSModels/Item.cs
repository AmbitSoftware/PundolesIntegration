using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSIntegration.Test.CMSModels
{
    public class UploadLotNumber
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "lot_number_c")]
        public string LotNumber { get; set; }
    }

    public class UpdateItem
    {
        [JsonProperty(PropertyName = "id")]
        public string ItemId { get; set; }

        [JsonProperty(PropertyName = "cat1_categories_id_c")]
        public string CategoryID { get; set; }

        [JsonProperty(PropertyName = "ac1_auction_calendar_id_c")]
        public string AuctionCalenderID { get; set; }

        [JsonProperty(PropertyName = "itm1_item_id_c")]
        public string SymbolID { get; set; }

        [JsonProperty(PropertyName = "item_size_c")]
        public string ItemSize { get; set; }

        [JsonProperty(PropertyName = "author_c")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "region_c")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "marked_stamped_dated_c")]
        public string MarkedStampedDated { get; set; }

        [JsonProperty(PropertyName = "region_and_date_c")]
        public string RegionAndDate { get; set; }

        [JsonProperty(PropertyName = "school_general_description_c")]
        public string GeneralDescription { get; set; }

        [JsonProperty(PropertyName = "item_date_c")]
        public string ItemDate { get; set; }

        [JsonProperty(PropertyName = "artist_school_c")]
        public string ArtistSchool { get; set; }

        [JsonProperty(PropertyName = "title_c")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "low_estimate_c")]
        public decimal? LowEstimate { get; set; }

        [JsonProperty(PropertyName = "title_2_c")]
        public string Title2 { get; set; }

        [JsonProperty(PropertyName = "a1_artists_id_c")]
        public string ArtistID { get; set; }

        [JsonProperty(PropertyName = "lot_number_c")]
        public string LotNumber { get; set; }

        [JsonProperty(PropertyName = "year_of_birth_c")]
        public string YearBirth { get; set; }

        [JsonProperty(PropertyName = "year_of_death_c")]
        public string YearDeath { get; set; }

        [JsonProperty(PropertyName = "medium_c")]
        public string Medium { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "signature_details_c")]
        public string SignatureDetails { get; set; }

        [JsonProperty(PropertyName = "contact_id_c")]
        public string ClientID { get; set; }

        [JsonProperty(PropertyName = "client_number_c")]
        public string ClientNumber { get; set; }

        [JsonProperty(PropertyName = "client_name_c")]
        public string ClientName { get; set; }

        [JsonProperty(PropertyName = "add1_addresses_id_c")]
        public string AddressID { get; set; }

        [JsonProperty(PropertyName = "basic_description_c")]
        public string BasicDescription { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string ItemNumber { get; set; }

        [JsonProperty(PropertyName = "fp_event_locations_id_c")]
        public string LocationID { get; set; }

        [JsonProperty(PropertyName = "high_estimate_c")]
        public decimal? HighEstimate { get; set; }

        [JsonProperty(PropertyName = "reserve_c")]
        public decimal? Reserve { get; set; }

        [JsonProperty(PropertyName = "commission_rate_c")]
        public int? CommisionRate { get; set; }
    }
    public class MoveItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "cat1_categories_id_c")]
        public string CategoryID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string ItemNumber { get; set; }
    }
    public class MergeItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "itm1_item_id_c")]
        public string Symbol { get; set; }

    }

    public class NewItem
    {
        [JsonProperty(PropertyName = "name")]
        public string ItemNumber { get; set; }

        [JsonProperty(PropertyName = "contact_id_c")]
        public string ContactID { get; set; }

        [JsonProperty(PropertyName = "cat1_categories_id_c")]
        public string CategoryID { get; set; }
    }


}
