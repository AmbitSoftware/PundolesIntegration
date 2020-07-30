using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSIntegration.Test.CMSModels
{
    public class Artist
    {
        public string id { get; set; }
        public string Name { get; set; }
        public DateTime? DateEntered { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedUserId { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public string Deleted { get; set; }
        public string AssignedUserId { get; set; }
        public string YearOfBirth { get; set; }
        public string YearOfDeth { get; set; }

    }

    public class PushArtist
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "year_of_birth_c")]
        public string YearOfBirth { get; set; }

        [JsonProperty(PropertyName = "year_of_death_c")]
        public string YearOfDeth { get; set; }

    }
}
