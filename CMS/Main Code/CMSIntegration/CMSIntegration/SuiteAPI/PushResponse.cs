using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSIntegration.SuiteAPI
{
    public class PushResponseforMultipleEntries<T>
    {
        public int SucceedCount { get; set; }
        public int FailedCount { get; set; }

        public PushResponseforMultipleEntries<T> BuildResponse(string sessionId, T[] arrayofData, string moduleName)
        {
            PushResponseforMultipleEntries<T> pushResponse = new PushResponseforMultipleEntries<T>();
            int SucceedCount = 0;
            int FailedCount = 0;
            foreach (var data in arrayofData)
            {
                try
                {
                    var req = new AddUpdateSingleEntryRequest();
                    req.SessionId = sessionId;
                    req.ModuleName = moduleName;
                    req.Entity = data;
                    var response = SuiteWrapper.AddUpdateSingleEntry(req).GetAwaiter().GetResult();
                    if (!string.IsNullOrEmpty(response.Id))
                        SucceedCount++;
                    else
                        FailedCount++;
                }
                catch (Exception)
                {
                    FailedCount++;
                }
            }
            pushResponse.SucceedCount = SucceedCount;
            pushResponse.FailedCount = FailedCount;

            return pushResponse;
        }
    }

    public class PushResponseforSingleEntry
    {
        public string Id { get; set; }

        public string Status { get; set; }
    }
}