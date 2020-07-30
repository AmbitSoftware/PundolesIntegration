using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSIntegration.SuiteAPI
{
    public class ReadEntryListRequest
    {
        public bool Deleted { get; internal set; }
        public bool Favorites { get; internal set; }
        public ArrayList LinkNameToFieldsArray { get; internal set; }
        public int MaxResults { get; internal set; }
        public string ModuleName { get; internal set; }
        public Nullable<int> Offset { get; internal set; }
        public string Query { get; internal set; }
        public List<string> SelectFields { get; internal set; }
        public string SessionId { get; set; }
        public string OrderBy { get; internal set; }

        public string MethodName { get; set; }

        public ReadEntryListRequest()
        {
            SessionId = string.Empty;
            ModuleName = string.Empty;
            Query = string.Empty;
            OrderBy = string.Empty;
            Offset = 0;
            SelectFields = new List<string>();
            LinkNameToFieldsArray = new ArrayList();
            MaxResults = 99999;
            Deleted = false;
            Favorites = false;
            MethodName = string.Empty;
        }
    }
}