using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;


namespace AMIntegration
{
    public class CreateContactRequest 
    {
     

        [JsonProperty(PropertyName = "Name"), XmlElement(IsNullable = true)]
        //[Required(ErrorMessage = "User Name field is required")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name"), XmlElement(IsNullable = true)]
        [Required(ErrorMessage = "Last Name field is required")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email1"), XmlElement(IsNullable = true)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone_work")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Salutation { get; set; }

        [JsonProperty(PropertyName = "phone_fax")]
        public string Fax { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Comments { get; set; }

       
        [JsonProperty(PropertyName = "company_name_c")]
        public string CompanyName { get; set; }
     
        [JsonProperty(PropertyName = "am_customer_id_c")]
        public string AMCustomerId { get; set; }

        [JsonProperty(PropertyName = "phone_number_two)")]
        public string PhoneNumberTwo { get; set; }

         public string ContactType { get; set; }
       // public string Approvalestatus { get; set; }

        [JsonProperty(PropertyName = "approval_status_c")]
        public string Approvalestatus { get; set; }
    }
}