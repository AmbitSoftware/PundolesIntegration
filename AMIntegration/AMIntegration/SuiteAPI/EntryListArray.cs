﻿namespace AMIntegration
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents EntryListArray class
    /// </summary>
    public class EntryListArray
    {
        /// <summary>
        /// Gets the entity  object
        /// </summary>
        public JObject Entity
        {
            get
            {
                var entity = new JObject();
                if (this.NameValueList == null)
                {
                    return entity;
                }

                IList<string> keys = this.NameValueList.Properties().Select(p => p.Name).ToList();
                foreach (var key in keys)
                {
                    var newKey = (string)this.NameValueList[key]["name"];
                    var newValue = (string)this.NameValueList[key]["value"];
                    entity.Add(new JProperty(newKey, newValue));
                }

                return entity;
            }
        }

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the entity module name
        /// </summary>
        [JsonProperty(PropertyName = "module_name")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the returned entity name value list
        /// </summary>
        [JsonProperty(PropertyName = "name_value_list")]
        public JObject NameValueList { get; set; }
    }
}
