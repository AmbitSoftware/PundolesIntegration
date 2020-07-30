
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace AMIntegration
{
    public class GetListofAMCustomerRequest : AMIntegrationReq
    {
        [JsonProperty(PropertyName = "ReferenceID"), XmlElement(IsNullable = true)]
        [Required(ErrorMessage = "ReferenceID field is required")]
        [StringLength(250, ErrorMessage = "ReferenceID field must be a string with a maximum length of 250")]
        public string ReferenceID { get; set; }

        [JsonProperty(PropertyName = "Channel"), XmlElement(IsNullable = true)]
        [Required(ErrorMessage = "Channel field is required")]
        [StringLength(250, ErrorMessage = "Channel field must be a string with a maximum length of 250")]
        public string Channel { get; set; }
    }
}