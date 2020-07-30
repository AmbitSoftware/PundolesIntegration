using System.Collections.Generic;


namespace AMIntegration
{
    public class ReadEntryListRequest
    {
        public bool Deleted { get; internal set; }
        public bool Favorites { get; internal set; }

        public List<string> LinkNameToFieldsArray { get; internal set; }
        public int MaxResults { get; internal set; }
        public string ModuleName { get; internal set; }
        public int Offset { get; internal set; }
        public string Query { get; internal set; }
        public List<string> SelectFields { get; internal set; }
        public string AccessToken { get; set; }
        public string OrderBy { get; internal set; }

        public ReadEntryListRequest()
        {
            AccessToken = string.Empty;
        }
    }
}