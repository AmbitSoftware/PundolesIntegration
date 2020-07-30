using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using AMIntegration.SugarCrmModels;

namespace AMIntegration
{
    public class CreateTraceLogRequest
    {
        [JsonProperty(PropertyName = "name"), XmlElement(IsNullable = true)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "entity_c"), XmlElement(IsNullable = true)]
        public string Entity_c { get; set; }

        [JsonProperty(PropertyName = "email_c")]
        public string Email_c { get; set; }

        [JsonProperty(PropertyName = "sync_date_c")]
        public DateTime Sync_date_c { get; set; }

        [JsonProperty(PropertyName = "pandl_am_integration_summary_pndl_am_integration_detail_1pandl_am_integration_summary_ida")]
        public string Integration_summary_ida { get; set; }


        [JsonProperty(PropertyName = "pandl_am_integration_summary_pndl_am_integration_detail_1pndl_am_integration_detail_idb")]
        public string Integration_detail_idb { get; set; }

    }
}