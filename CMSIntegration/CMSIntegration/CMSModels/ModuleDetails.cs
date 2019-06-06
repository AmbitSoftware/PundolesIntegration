using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSIntegration.CMSModels
{
    public class ModuleDetails
    {
        [JsonProperty(PropertyName = "module_name")]
        public string ModuleName { get; set; }

        [JsonProperty(PropertyName = "table_name")]
        public string TableName { get; set; }

        [JsonProperty(PropertyName = "module_fields")]
        public object Modulefields { get; set; }

        [JsonProperty(PropertyName = "link_fields")]
        public object LinkFields { get; set; }
    }
}