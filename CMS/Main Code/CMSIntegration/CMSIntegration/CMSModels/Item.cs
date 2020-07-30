using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSIntegration.CMSModels
{
    public class Item
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

        [JsonProperty(PropertyName = "title_c")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "lot_number_c")]
        public string LotNumber { get; set; }

        [JsonProperty(PropertyName = "item_name_c")]
        public string ItemName { get; set; }

        [JsonProperty(PropertyName = "item_size_c")]
        public string ItemSize { get; set; }

        [JsonProperty(PropertyName = "basic_description_c")]
        public string BasicDescription { get; set; }

        [JsonProperty(PropertyName = "client_name_c")]
        public string ClientName { get; set; }

        [JsonProperty(PropertyName = "author_c")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "region_c")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "medium_c")]
        public string Medium { get; set; }

        [JsonProperty(PropertyName = "region_and_date_c")]
        public string RegionandDate { get; set; }

        [JsonProperty(PropertyName = "year_of_birth_c")]
        public string YearOfBirth { get; set; }

        [JsonProperty(PropertyName = "school_general_description_c")]
        public string SchoolGeneralDescription { get; set; }

        [JsonProperty(PropertyName = "marked_stamped_dated_c")]
        public string MarkedStampedDated { get; set; }

        [JsonProperty(PropertyName = "date_of_painting_c")]
        public string DateOfPainting { get; set; }

        [JsonProperty(PropertyName = "title_2_c")]
        public string Title2 { get; set; }

        [JsonProperty(PropertyName = "year_of_death_c")]
        public string YearOfDeath { get; set; }

        [JsonProperty(PropertyName = "item_date_c")]
        public string ItemDate { get; set; }

        [JsonProperty(PropertyName = "signature_details_c")]
        public string SignatureDetails { get; set; }

        [JsonProperty(PropertyName = "low_resolution_image_c")]
        public string LowResolutio { get; set; }

        [JsonProperty(PropertyName = "artist_school_c")]
        public string ArtistSchool { get; set; }

        [JsonProperty(PropertyName = "photography_c")]
        public string Photography { get; set; }

        [JsonProperty(PropertyName = "hsn_sac_code_c")]
        public string HsnSacCode { get; set; }

        [JsonProperty(PropertyName = "high_estimate_c")]
        public string HighEstimate { get; set; }

        [JsonProperty(PropertyName = "commission_rate_c")]
        public string CommisionRate { get; set; }

        [JsonProperty(PropertyName = "reserve_c")]
        public string Reserve { get; set; }

        [JsonProperty(PropertyName = "asi_registration_number_c")]
        public string AsiRegistrationNumber { get; set; }

        [JsonProperty(PropertyName = "base_rate_c")]
        public string BaseRate { get; set; }

        [JsonProperty(PropertyName = "low_estimate_c")]
        public string LowEstimate { get; set; }

        [JsonProperty(PropertyName = "ac1_auction_calendar_id_c")]
        public string AuctionCalendarId { get; set; }

        [JsonProperty(PropertyName = "cat1_categories_id_c")]
        public string CategoriesId { get; set; }

        [JsonProperty(PropertyName = "fp_event_locations_id_c")]
        public string EventLocations { get; set; }

        [JsonProperty(PropertyName = "itm1_item_id_c")]
        public string ItemId { get; set; }

        [JsonProperty(PropertyName = "contact_id_c")]
        public string ContactId { get; set; }

        [JsonProperty(PropertyName = "add1_addresses_id_c")]
        public string AddressId { get; set; }

        [JsonProperty(PropertyName = "a1_artists_id_c")]
        public string ArtistId { get; set; }

    }


    public class UploadLotNumber
    {
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "lot_number_c")]
        [Required(ErrorMessage = "lot_number_c field is required")]
        public string LotNumber { get; set; }
    }

    public class MoveItem
    {
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "cat1_categories_id_c")]
        [Required(ErrorMessage = "cat1_categories_id_c field is required")]
        public string CategoryID { get; set; }

        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "name field is required")]
        public string ItemNumber { get; set; }
    }

    public class MergeItem
    {
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "itm1_item_id_c")]
        [Required(ErrorMessage = "itm1_item_id_c field is required")]
        public string Symbol { get; set; }

    }

    public class UpdateItem
    {
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string ItemId { get; set; }

        [JsonProperty(PropertyName = "cat1_categories_id_c")]
        public string CategoryID { get; set; }

        [JsonProperty(PropertyName = "ac1_auction_calendar_id_c")]
        public string AuctionCalenderID { get; set; }

        [JsonProperty(PropertyName = "sym1_symbols_id_c")]
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

        [JsonProperty(PropertyName = "loc_location_id_c")]
        public string LocationID { get; set; }       

        private string lowEstimate;

        [JsonProperty(PropertyName = "low_estimate_c")]
        public decimal? LowEstimate
        {
            get
            {
                return (string.IsNullOrEmpty(this.lowEstimate)) ? (decimal?)null : Convert.ToDecimal(this.lowEstimate);
            }
            set
            {
                this.lowEstimate = Convert.ToString(value);
            }
        }       

        private string highEstimate;

        [JsonProperty(PropertyName = "high_estimate_c")]
        public decimal? HighEstimate
        {
            get
            {
                return (string.IsNullOrEmpty(this.highEstimate)) ? (decimal?)null : Convert.ToDecimal(this.highEstimate);
            }
            set
            {
                this.highEstimate = Convert.ToString(value);
            }
        }
       
        private string reserve;

        [JsonProperty(PropertyName = "reserve_c")]
        public decimal? Reserve
        {
            get
            {
                return (string.IsNullOrEmpty(this.reserve)) ? (decimal?)null : Convert.ToDecimal(this.reserve);
            }
            set
            {
                this.reserve = Convert.ToString(value);
            }
        }
              
        private string commisionRate;

        [JsonProperty(PropertyName = "commission_rate_c")]
        public int? CommisionRate
        {
            get
            {
                return (string.IsNullOrEmpty(this.commisionRate)) ? (int?)null : Convert.ToInt32(this.commisionRate);
            }
            set
            {
                this.commisionRate = Convert.ToString(value);
            }
        }
    }

    public class NewItem
    {
        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "name field is required")]
        public string ItemNumber { get; set; }

        [JsonProperty(PropertyName = "contact_id_c")]
        [Required(ErrorMessage = "contact_id_c field is required")]
        public string ContactID { get; set; }

        [JsonProperty(PropertyName = "client_number_c")]
        public string ClientNumber { get; set; }

        [JsonProperty(PropertyName = "cat1_categories_id_c")]
        [Required(ErrorMessage = "cat1_categories_id_c field is required")]
        public string CategoryID { get; set; }


    }

    public class ItemModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string lot_number { get; set; }
        public string item_name { get; set; }
        public string item_size { get; set; }
        public string basic_description { get; set; }
        public string author { get; set; }
        public string region { get; set; }
        public string medium { get; set; }
        public string region_and_date { get; set; }
        public string year_of_birth { get; set; }
        public string school_general_description { get; set; }
        public string marked_stamped_dated { get; set; }
        public string date_of_painting { get; set; }
        public string title_2 { get; set; }
        public string year_of_death { get; set; }
        public string item_date { get; set; }
        public string signature_details { get; set; }
        public string low_resolution_image { get; set; }
        public string artist_school { get; set; }
        public string photography { get; set; }
        public string hsn_sacode { get; set; }
        public Nullable<decimal> high_estimate { get; set; }
        public Nullable<int> commission_rate { get; set; }
        public Nullable<decimal> reserve { get; set; }
        public string asi_registration_number { get; set; }
        public Nullable<decimal> base_rate { get; set; }
        public Nullable<decimal> low_estimate { get; set; }
        public string ac1_auctionalendar_id { get; set; }
        public string cat1ategories_id { get; set; }
        public string add1_addresses_id { get; set; }
        public string a1_artists_id { get; set; }
        public string loc_location_id { get; set; }
        public string sym1_symbols_id { get; set; }
        public string client_number { get; set; }
        public string contact_id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> date_entered { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public string modified_user_id { get; set; }
        public string created_by { get; set; }
        public string description { get; set; }
        public Nullable<bool> deleted { get; set; }
        public string assigned_user_id { get; set; }
    }

    public class PushItemResponse
    {
        public Nullable<int> itemid { get; set; }
        public string status { get; set; }
    }
}