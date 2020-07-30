using Newtonsoft.Json;
using System.Collections.Generic;

namespace AMIntegration.AMCustomer
{
    public class AMCustomer : EntityBase
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "preferred_email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone_number")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "tenant_id")]
        public string DeviceLocation { get; set; }

        [JsonProperty(PropertyName = "preferred_payment_method")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "paddle")]
        public string AssignToGroup { get; set; }

        [JsonProperty(PropertyName = "given_name")]
        public string Firstname { get; set; }

        [JsonProperty(PropertyName = "family_name")]
        public string Lastname { get; set; }

        [JsonProperty(PropertyName = "integration_id")]
        public string SuiteId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string DebitCardGroup { get; set; }

        [JsonProperty(PropertyName = "fax_number")]
        public string Fax { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Comments { get; set; }

        [JsonProperty(PropertyName = "row_id")]
        public string AuctionRecordId { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "email_address")]
        public string Email_address { get; set; }

        [JsonProperty(PropertyName = "company_name")]
        public string Companyname { get; set; }


        public List<AMAddress> Addresses { get; set; }
      
    }
}