using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;


namespace AMIntegration
{
    public class CreateAddressRequest
    {
        [JsonProperty(PropertyName = "name"), XmlElement(IsNullable = true)]
        //[Required(ErrorMessage = "User Name field is required")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "primary_address_street_c")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName = "primary_address_city_c"), XmlElement(IsNullable = true)]
        [Required(ErrorMessage = "Last Name field is required")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "primary_address_country_c"), XmlElement(IsNullable = true)]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "primary_address_state_c")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "shipping_address_c")]
        public Boolean Shipping_address { get; set; }

        [JsonProperty(PropertyName = "billing_address_c")]
        public Boolean Billing_address { get; set; }

        [JsonProperty(PropertyName = "catalouge_address_c")]
        public Boolean Cataouge_address { get; set; }

        [JsonProperty(PropertyName = "client_number_c")]
        public string Client_number_c { get; set; }

        [JsonProperty(PropertyName = "contacts_add1_addresses_1contacts_ida")]
        public string Contacts_add1_addresses_1contacts_ida { get; set; }


        [JsonProperty(PropertyName = "primary_address_postalcode_c")]
        public string Zip { get; set; }


    }
}