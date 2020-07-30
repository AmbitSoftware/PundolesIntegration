using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMSIntegration.SuiteAPI
{
    public class ReadEntryListResponse
    {
        /// <summary>
        /// Gets the entity list returned from SugarCrm to json array objects
        /// </summary>
        public JArray EntityList
        {
            get
            {
                if (this.EntryList == null)
                {
                    return null;
                }

                var entityList = new JArray();
                foreach (var item in this.EntryList)
                {
                    entityList.Add(item.Entity);
                }

                return entityList;
            }
        }


        /// <summary>
        /// Gets or sets the number of entries returned
        /// </summary>
        [JsonProperty(PropertyName = "result_count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the total count of entries available
        /// </summary>
        [JsonProperty(PropertyName = "total_count")]
        public string TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the next offset
        /// </summary>
        [JsonProperty(PropertyName = "next_offset")]
        public int NextOffset { get; set; }

        /// <summary>
        /// Gets or sets the entry list in json
        /// </summary>
        [JsonProperty(PropertyName = "entry_list")]
        public List<EntryListArray> EntryList { get; set; }

        /// <summary>
        /// Gets or sets the relationship link entry list in json
        /// </summary>
        //[JsonProperty(PropertyName = "relationship_list")]
        //public List<JObject> RelationshipList { get; set; }

        [JsonProperty(PropertyName = "relationship_list")]
        public LinkList[] relationship_list { get; set; }


        [JsonProperty(PropertyName = "document_revision")]
        public List<EntryListArray> DocumentRevision { get; set; }
    }
    public class LinkList
    {
        public LinkListDetails[] link_list { get; set; }
    }

    public class LinkListDetails
    {
        public string name { get; set; }
        public RelationShipListArray[] records { get; set; }
    }

    public class RelationShipListArray
    {
        public JObject Entity
        {
            get
            {
                var entity = new JObject();
                if (this.link_value == null)
                {
                    return entity;
                }

                IList<string> keys = this.link_value.Properties().Select(p => p.Name).ToList();
                foreach (var key in keys)
                {
                    var newKey = (string)this.link_value[key]["name"];
                    var newValue = (string)this.link_value[key]["value"];
                    entity.Add(new JProperty(newKey, newValue));
                }

                return entity;
            }
        }
        [JsonProperty(PropertyName = "link_value")]
        public JObject link_value { get; set; }
    }
}