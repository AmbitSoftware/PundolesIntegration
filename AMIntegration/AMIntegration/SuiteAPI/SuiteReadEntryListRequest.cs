using System.Collections.Generic;


namespace AMIntegration
{
    public class SuiteReadEntryListRequest
    {
        public bool Deleted { get; internal set; }
        public bool Favorites { get; internal set; }

        public List<string> LinkNameToFieldsArray { get; internal set; }
        public int MaxResults { get; internal set; }
        public string ModuleName { get; internal set; }
        public int Offset { get; internal set; }
        public string Query { get; internal set; }
        public List<string> SelectFields { get; internal set; }
        public string SessionId { get; set; }
        public string OrderBy { get; internal set; }

        public SuiteReadEntryListRequest()
        {
            SessionId = string.Empty;
            ModuleName = string.Empty;
            Query = string.Empty;
            OrderBy = string.Empty;
            Offset = 0;
            SelectFields = new List<string>();
            LinkNameToFieldsArray = new List<string>();
            MaxResults = 99999;
            Deleted = false;
            Favorites = false;
        }
    }
}