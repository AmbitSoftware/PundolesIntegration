using System;
using System.Collections.Generic;


namespace AMIntegration
{
    public class InsertRequest
    {
        public string ModuleName { get; internal set; }
        public List<string> SelectFields { get; internal set; }
        public string SessionId { get; set; }
        public Object Entity { get; internal set; }

        public InsertRequest()
        {
            SessionId = string.Empty;
            ModuleName = string.Empty;
            SelectFields = new List<string>();
        }
    }
}