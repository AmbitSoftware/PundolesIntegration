using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMSIntegration.CMSModels
{
    public class PullArtist
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

    public class PushArtist
    {      
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "year_of_birth_c")]
        public string year_of_birth_c { get; set; }

        [JsonProperty(PropertyName = "year_of_death_c")]
        public string YearOfDeth { get; set; }

    }

    public class Artist
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "date_entered")]
        public DateTime? DateEntered { get; set; }

        [JsonProperty(PropertyName = "date_modified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty(PropertyName = "modified_user_id")]
        public string ModifiedUserId { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public string Deleted { get; set; }

        [JsonProperty(PropertyName = "assigned_user_id")]
        public string AssignedUserId { get; set; }

        [JsonProperty(PropertyName = "year_of_birth_c")]
        public string YearOfBirth { get; set; }

        [JsonProperty(PropertyName = "year_of_death_c")]
        public string YearOfDeth { get; set; }

    }

    public partial class CreateArtist
    {
        [Required(ErrorMessage = "name field is required")]
        public string name { get; set; }
        public string description { get; set; }
        public string year_of_birth_c { get; set; }
        public string year_of_death_c { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> createdby_id { get; set; }
        public Nullable<int> modifiedby_id { get; set; }
    }

    public partial class UpdateArtist
    {
        [Required(ErrorMessage = "id field is required")]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string year_of_birth_c { get; set; }
        public string year_of_death_c { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modifiedby_id { get; set; }
    }

    public class CreateArtistResponse
    {
        public Nullable<int> id { get; set; }
        public string status { get; set; }
    }

}