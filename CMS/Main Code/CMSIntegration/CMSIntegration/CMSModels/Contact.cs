using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSIntegration.CMSModels
{
    public class PullContact
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "client_number_c")]
        public string ClientNumber { get; set; }

        [JsonProperty(PropertyName = "primary_address_street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "primary_address_city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "primary_address_state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "primary_address_postalcode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "primary_address_country")]
        public string Country { get; set; }

        public List<Addresses> ListOfAddresses { get; set; }
    }

    public class GeneralContact
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "client_number_c")]
        public string ClientNumber { get; set; }
     
    }

    public class Addresses
    {
        [JsonProperty(PropertyName = "primary_address_street_c")]
        public string primary_address_street_c { get; set; }

        [JsonProperty(PropertyName = "primary_address_city_c")]
        public string primary_address_city_c { get; set; }

        [JsonProperty(PropertyName = "primary_address_state_c")]
        public string primary_address_state_c { get; set; }

        [JsonProperty(PropertyName = "primary_address_postalcode_c")]
        public string primary_address_postalcode_c { get; set; }

        [JsonProperty(PropertyName = "primary_address_country_c")]
        public string primary_address_country_c { get; set; }
    }

    public class ContactModel
    {
        [Required(ErrorMessage = "contact_id field is required")]
        public int contact_id { get; set; }

        public string salutation { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company_name { get; set; }
        public string contact_type { get; set; }

        public string client_number { get; set; }

        public string interest_id { get; set; }

        public string category_id { get; set; }

        public string customer_category_id { get; set; }

        public string level_id { get; set; }

        public string catalogue_id { get; set; }

        public string marital_status_id { get; set; }

        public Nullable<System.DateTime> marriage_anniversary_date { get; set; }

        public string am_customer_id { get; set; }

        public string approval_status_id { get; set; }

        public string authorized_to_bid_id { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string fax { get; set; }

        public string mobile { get; set; }

        public string other_phone { get; set; }

        public string clients_vat_tin_no { get; set; }

        public string aadhar_number { get; set; }

        public string pan_no { get; set; }

        public Nullable<System.DateTime> date_created { get; set; }

        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<int> createdby_id { get; set; }
        public Nullable<int> modifiedby_id { get; set; }
    }

    public class PushContactResponse
    {
        public Nullable<int> contactid { get; set; }
        public string status { get; set; }
    }
}