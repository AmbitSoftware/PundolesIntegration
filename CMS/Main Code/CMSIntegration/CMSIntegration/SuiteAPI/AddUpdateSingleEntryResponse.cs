using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSIntegration.SuiteAPI
{
    public class AddUpdateSingleEntryResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}