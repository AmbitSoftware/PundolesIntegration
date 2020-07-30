using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMSIntegration.CMSModels
{
    public class PullCategories
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "code_c")]
        public string Code { get; set; }
        
    }

    public class PushCategories
    {
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "id field is required")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "code_c")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class Categories
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

        [JsonProperty(PropertyName = "code_c")]
        public string Code { get; set; }

    }
}