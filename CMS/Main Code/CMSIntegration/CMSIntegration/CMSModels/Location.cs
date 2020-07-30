using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMSIntegration.CMSModels
{
    public class PullLocation
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }

    public class PushLocation
    {
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }

    public class Location
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

        [JsonProperty(PropertyName = "semilocation_of_c")]
        public string SemilocationOf { get; set; }

        [JsonProperty(PropertyName = "address_city_c")]
        public string AddressCity { get; set; }

        [JsonProperty(PropertyName = "address_state_c")]
        public string AddressState { get; set; }

        [JsonProperty(PropertyName = "address_postalcode_c")]
        public string AddressPostalCode { get; set; }

        [JsonProperty(PropertyName = "address_country_c")]
        public string AddressCountry { get; set; }

        [JsonProperty(PropertyName = "return_address_city_c")]
        public string ReturnAddressCity { get; set; }

        [JsonProperty(PropertyName = "return_address_street_c")]
        public string ReturnAddressStreet { get; set; }

        [JsonProperty(PropertyName = "return_address_state_c")]
        public string ReturnAddressState { get; set; }

        [JsonProperty(PropertyName = "return_address_postalcode_c")]
        public string ReturnAddressPostalCode { get; set; }

        [JsonProperty(PropertyName = "address_street_c")]
        public string AddressStreet { get; set; }

    }
}