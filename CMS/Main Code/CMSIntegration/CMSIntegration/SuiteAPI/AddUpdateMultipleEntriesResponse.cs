using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSIntegration.SuiteAPI
{
    public class AddUpdateMultipleEntriesResponse
    {
        [JsonProperty(PropertyName = "ids")]
        public List<string> Id { get; set; }
    }
}