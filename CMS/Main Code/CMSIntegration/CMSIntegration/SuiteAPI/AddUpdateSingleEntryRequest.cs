using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSIntegration.SuiteAPI
{
    public class AddUpdateSingleEntryRequest
    {
        public string ModuleName { get; internal set; }
        public List<string> SelectFields { get; internal set; }
        public string SessionId { get; set; }
        public Object Entity { get; internal set; }

        public AddUpdateSingleEntryRequest()
        {
            SessionId = string.Empty;
            ModuleName = string.Empty;
            SelectFields = new List<string>();
        }
    }
}