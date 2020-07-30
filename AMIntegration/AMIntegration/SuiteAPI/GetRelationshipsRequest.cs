
using System.Collections.Generic;

namespace AMIntegration
{
    public class GetRelationshipsRequest
    {
        public string session { get; set; }
        public string module_name { get; internal set; }
        public string module_id { get; internal set; }
        public string link_field_name { get; internal set; }
        public string related_module_query { get; internal set; }
        public List<string> related_fields { get; internal set; }
        public Dictionary<string, object> related_module_link_name_to_fields_array { get; internal set; }
        public bool deleted { get; internal set; }
        public string order_by { get; internal set; }
        public int offset { get; internal set; }
        public int limit { get; internal set; }

        public GetRelationshipsRequest()
        {
            session = string.Empty;
            module_name = string.Empty;
            module_id = string.Empty;
            link_field_name = string.Empty;
            related_module_query = string.Empty;
            related_fields = new List<string>();
            related_module_link_name_to_fields_array = new Dictionary<string, object>();
            deleted = false;
            order_by = string.Empty;
            offset = 0;
            limit = 99999;
        }
    }
}