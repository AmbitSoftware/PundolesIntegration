using AMIntegration.SuiteAPI;
using System;
using System.Collections.Generic;

namespace AMIntegration.Common
{
    public class CommonClass
    {
        string suitesessionId = string.Empty;

        public CreateTraceLogResponse InsertSuiteAmIntegrationLogDetail(CreateTraceLogRequest result_page, string logId)
        {

            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            var tracelogrequest = new CreateTraceLogRequest();
            tracelogrequest.Name = result_page.Name;
            tracelogrequest.Description = result_page.Description;
            tracelogrequest.Entity_c = result_page.Entity_c;
            tracelogrequest.Sync_date_c = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            tracelogrequest.Integration_summary_ida = logId;
            if (!string.IsNullOrEmpty(result_page.Email_c))
            {
                tracelogrequest.Email_c = result_page.Email_c;
            }

            var request_Integration_Detail = new InsertRequest
            {
                SessionId = suitesessionId,
                ModuleName = "pndl_AM_Integration_Detail",
                Entity = tracelogrequest
            };

            suiteWrapper.Insert(request_Integration_Detail).GetAwaiter().GetResult();

            return null;
        }

        public List<EntryListArray> GetSuiteContactIDByEmail(string email, string logId)
        {
            var wrapper = new SuiteWrapper();
            suitesessionId = wrapper.Login().GetAwaiter().GetResult();

            var request = new SuiteReadEntryListRequest();
            request.SessionId = suitesessionId;
            request.ModuleName = "Contacts";
            request.Query = " contacts" +
                             ".id in (SELECT eabr.bean_id FROM email_addr_bean_rel eabr JOIN email_addresses ea ON " +
                             "(ea.id = eabr.email_address_id) WHERE eabr.deleted=0 AND ea.email_address = '" + email + "')";
            request.SelectFields = new List<string> {
                    "Am_customer_id",
                    "email"
                    };
            try
            {

                var suiteConstactList = wrapper.GetList(request).GetAwaiter().GetResult();
                return suiteConstactList.EntryList;
            }

            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("GetRegistration", "Error : " + ex.Message);
                InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "GetSuiteConstactIDByEmail", Entity_c = "Customer" }, logId);
            }
            return null;
        }

        // Get NetSuite Customer internalID
        public SuiteContact GetSuiteConstactID(string auctionCustmoerID, string logId)
        {
            var suiteContact = new SuiteContact();
            var amwrapper = new AMWrapper();
            var suiteWrapper = new SuiteWrapper();
            var contactrequest = new CreateContactRequest();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            var request = new SuiteReadEntryListRequest();
            request.SessionId = suitesessionId;
            request.ModuleName = "Contacts";
            request.Query = " am_customer_id_c = '" + auctionCustmoerID + "'";
            request.SelectFields = new List<string> {
                    "id",
                    "am_customer_id_c",
                    "name",
                    "client_number_c"
                    };
            try
            {

                var suiteConstactlist = suiteWrapper.GetList(request).GetAwaiter().GetResult();

                foreach (var record in suiteConstactlist.EntryList)
                {
                    suiteContact.Id = record.Entity.SelectToken("id").ToString();
                    suiteContact.Name = record.Entity.SelectToken("name").ToString();
                    suiteContact.Client_number_c = record.Entity.SelectToken("client_number_c").ToString();   
                    return suiteContact;
                }
            }

            catch (Exception ex)
            {
                InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "CheckForRegistration", Entity_c = "Customer" }, logId);
                SuiteWrapper.WriteTraceLog("CheckForRegistration", "Error Message : " + ex.Message);
            }
            return null;
        }

    }
}