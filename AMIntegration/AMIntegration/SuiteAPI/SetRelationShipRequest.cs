using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMIntegration
{
    public class SetRelationShipRequest
    {
        public string session { get; set; }
        public string module_name { get; internal set; }
        public string module_id { get; internal set; }
        public string link_field_name { get; internal set; }
        public List<string> related_ids { get; internal set; }
        public List<string> name_value_list { get; internal set; }
        public bool delete { get; internal set; }

        public bool[] delete_array { get; internal set; }

        public SetRelationShipRequest()
        {
            session = string.Empty;
            module_name = string.Empty;
            module_id = string.Empty;
            link_field_name = string.Empty;
            related_ids = new List<string>();
            name_value_list = new List<string>();
            delete = false;
            delete_array = new bool[] { false };
        }
    }
}