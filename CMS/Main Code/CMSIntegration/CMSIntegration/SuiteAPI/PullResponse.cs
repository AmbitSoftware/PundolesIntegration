using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSIntegration.SuiteAPI
{
    public class PullResponse<T>
    {
        public List<T> data { get; set; }

        public bool IsDataAvailable { get; set; }

        public string NextDataURL { get; set; }

        public static PullResponse<T> pullResponse(ReadEntryListResponse res, string methodName, bool deleted, string lastSyncDate)
        {
            PullResponse<T> response = new PullResponse<T>();

            var listObjectConverted = SuiteWrapper.GetListObjectConverted<T>(res);
            response.data = listObjectConverted;
            if (int.Parse(res.TotalCount) == res.NextOffset || int.Parse(res.TotalCount) < res.NextOffset)
                response.IsDataAvailable = false;
            else
                response.IsDataAvailable = true;

            if (!response.IsDataAvailable)
                response.NextDataURL = null;
            else
                response.NextDataURL = SuiteWrapper.nextURL + "api/SyncMasters/PullMasterData?methodName=" + methodName + "&lastSyncDate=" + lastSyncDate + "&nextOffSet=" + res.NextOffset + "&deleted=" + deleted + "";

            return response;
        }

        public static PullResponse<T> pullResponseforContact(ReadEntryListResponse res)
        {
            PullResponse<T> response = new PullResponse<T>();

            var listObjectConverted = SuiteWrapper.GetListObjectConverted<T>(res);
            response.data = listObjectConverted;
            if (int.Parse(res.TotalCount) == res.NextOffset || int.Parse(res.TotalCount) < res.NextOffset)
                response.IsDataAvailable = false;
            else
                response.IsDataAvailable = true;

            if (!response.IsDataAvailable)
                response.NextDataURL = null;
            else
                response.NextDataURL = SuiteWrapper.nextURL + "api/Contact/PullContacts?nextOffSet=" + res.NextOffset + "";

            return response;
        }
    }

    public class PullResponseWithoutPagination<T>
    {
        public List<T> data { get; set; }

        public static PullResponseWithoutPagination<T> pullResponse(ReadEntryListResponse res)
        {
            PullResponseWithoutPagination<T> response = new PullResponseWithoutPagination<T>();

            var listObjectConverted = SuiteWrapper.GetListObjectConverted<T>(res);
            response.data = listObjectConverted;

            return response;
        }


    }



}