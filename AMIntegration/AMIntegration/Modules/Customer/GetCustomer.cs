using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using AMIntegration.Common;

namespace AMIntegration.Modules.Customer
{
    public class GetCustomer 
    {
        string suitesessionId = string.Empty;
        CommonClass common = new CommonClass();

        public GetListofAMCustomerResponse GetListofAMCustomer(DateTime startDate, DateTime endDate, string logId)
        {
            SuiteWrapper.WriteTraceLog("GetCustomer", "---------------- CUSTOMER SYNC START ------------------------- ");
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Customer sync started", Name = "GetCustomer", Entity_c = "Customer" }, logId);

            var amWrapper = new AMWrapper();
            var ret = new GetListofAMCustomerResponse();


            try
            {
                var request = new ReadEntryListRequest
                {
                    AccessToken = amWrapper.apiAccessToken,
                    ModuleName = "customer"
                };

                var result_AMCustomers = amWrapper.GetAMList<AMCustomer.AMCustomer>(request, $"{amWrapper.apiUrl}/v2/admin/customer").GetAwaiter().GetResult();
                var result_page = JObject.Parse(result_AMCustomers).SelectToken("result_page");

                ReadAMContact(result_page, startDate, endDate, logId);

                var query_info = JObject.Parse(result_AMCustomers).SelectToken("query_info");
                var next_page_url = string.Empty;

                foreach (var next_page in query_info.SelectTokens("next_page"))
                {
                    next_page_url = next_page.ToString();
                    while (!string.IsNullOrEmpty(next_page_url))
                    {
                        var jstr_Next = amWrapper.GetAMList<AMCustomer.AMCustomer>(request, next_page_url).GetAwaiter().GetResult();
                        var result_page_Next = JObject.Parse(jstr_Next).SelectToken("result_page");

                        ReadAMContact(result_page_Next, startDate, endDate, logId);

                        var query_info_Next = JObject.Parse(jstr_Next).SelectToken("query_info");
                        foreach (var next_page1 in query_info_Next.SelectTokens("next_page"))
                        {
                            next_page_url = string.Empty;
                            next_page_url = next_page1.ToString();
                        }
                    }
                }

                ret.StatusCode = AMAPI.ClsConstant.SuccessCode;
                ret.Message = AMAPI.ClsConstant.SuccessMessage;

                return ret;
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("GetListofAMCustomer", "Error Message : " + ex.Message);
                ret.StatusCode = AMAPI.ClsConstant.ServerErrorCode;
                ret.Message = AMAPI.ClsConstant.ServerErrorMsg;

                return ret;
            }
        }

        private void ReadAMContact(JToken result_AMCustomers, DateTime startDate, DateTime endDate, string logId)
        {
            try
            {

                var suiteWrapper = new SuiteWrapper();

                if (result_AMCustomers.Count() == 0)
                {
                    SuiteWrapper.WriteTraceLog("GetCustomer", "No any Customer exist for period");
                    common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "No any Customer exist for period", Name = "ReadAMContact", Entity_c = "Customer" }, logId);
                    return;
                }

                foreach (var AMCustomers in result_AMCustomers)
                {
                    var suiteRecordId = string.Empty;
                    var amCustomerId = Convert.ToString(AMCustomers.SelectToken("row_id"));
                    var amCustomerCreatedDate = Convert.ToDateTime(AMCustomers.SelectToken("created_at")).ToString("yyyy-MM-dd")+ 'T'+ Convert.ToDateTime(AMCustomers.SelectToken("created_at")).ToString("HH:mm:ss.fff") + 'Z';
                    var amCustomerUpdatedtedDate = Convert.ToDateTime(AMCustomers.SelectToken("updated_at")).ToString("yyyy-MM-dd") + 'T' + Convert.ToDateTime(AMCustomers.SelectToken("updated_at")).ToString("HH:mm:ss.fff") + 'Z';
                  
                    var suiteContact = common.GetSuiteConstactID(amCustomerId, logId);

                    var strFromDateTime = getAuctionDateTimeStringFormat(startDate);
                    var strToDateTime = getAuctionDateTimeStringFormat(endDate);

                    if (Convert.ToDateTime(amCustomerCreatedDate) >= Convert.ToDateTime(strFromDateTime) && Convert.ToDateTime(amCustomerCreatedDate) <= Convert.ToDateTime(strToDateTime) && suiteContact == null)
                    {
                        suiteRecordId = InsertSuiteContact(AMCustomers, logId);
                        if (suiteRecordId != "")
                        {
                            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Customer created Successfully. AM ID: " + amCustomerId, Name = "GetCustomer", Entity_c = "Customer" }, logId);
                            SuiteWrapper.WriteTraceLog("GetCustomer", "Customer created Successfully.AM ID: " + amCustomerId);
                        }
                    }


                    if (Convert.ToDateTime(amCustomerUpdatedtedDate) >= Convert.ToDateTime(strFromDateTime) && Convert.ToDateTime(amCustomerUpdatedtedDate) <= Convert.ToDateTime(strToDateTime))
                    {
                        // Updated date is Greater than Created date
                        if (Convert.ToDateTime(amCustomerUpdatedtedDate) > Convert.ToDateTime(amCustomerCreatedDate) && suiteContact != null)
                        {

                            suiteRecordId = UpdateCustomerRecord(AMCustomers, suiteContact.Id, logId);
                            if (suiteRecordId != "")
                            {
                                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Customer updated Successfully. AM ID: " + amCustomerId, Name = "ReadAMContact", Entity_c = "Customer" }, logId);
                                SuiteWrapper.WriteTraceLog("GetCustomer", "Customer updated Successfully. AM ID: " + amCustomerId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error Message : " + ex.Message, Name = "ReadAMContact", Entity_c = "Customer" }, logId);
                SuiteWrapper.WriteTraceLog("GetCustomer", "Error Message : " + ex.Message);
            }
        }

        private string getAuctionDateTimeStringFormat(DateTime date)
        {
            var strDATE = getstrDATE(date, "YYYY-MM-DD");
            strDATE = strDATE.Replace("/", "-");
            var strTIME = "00:00:00";
            var strDATETIME = strDATE + 'T' + strTIME + 'Z';
            return strDATETIME;
        }

        private string getstrDATE(DateTime date, string dateTimeFormat)
        {
            var dd = Convert.ToString(date.Day);
            var mm = Convert.ToString(date.Month);
            var yy = Convert.ToString(date.Year);

            if (Convert.ToInt32(dd) < 10)
                dd = "0" + Convert.ToString(dd);

            if (Convert.ToInt32(mm) < 10)
                mm = '0' + mm;

            var strDATE = "";
            if (dateTimeFormat == "DD/MM/YYYY")
                strDATE = dd + "/" + mm + "/" + yy;

            if (dateTimeFormat == "DD-MM-YYYY")
                strDATE = dd + "-" + mm + "-" + yy;

            if (dateTimeFormat == "MM/DD/YYYY")
                strDATE = mm + "/" + dd + "/" + yy;

            if (dateTimeFormat == "MM-DD-YYYY")
                strDATE = mm + "-" + dd + "-" + yy;

            if (dateTimeFormat == "YYYY/MM/DD")
                strDATE = yy + "/" + mm + "/" + dd;

            if (dateTimeFormat == "YYYY-MM-DD")
                strDATE = yy + "-" + mm + "-" + dd;

            return strDATE;
        }

        private string UpdateCustomerRecord(JToken amCustomers_Result, string suiteConstactId, string logId)
        {
            try
            {
                if (amCustomers_Result != null && suiteConstactId != null)
                {
                    var suiteWrapper = new SuiteWrapper();
                    suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

                    var contactrequest = new CreateContactRequest
                    {
                        Email = Convert.ToString(amCustomers_Result.SelectToken("preferred_email")),
                        Phone = Convert.ToString(amCustomers_Result.SelectToken("phone_number")),
                        FirstName = Convert.ToString(amCustomers_Result.SelectToken("given_name")),
                        LastName = Convert.ToString(amCustomers_Result.SelectToken("family_name")),
                        Salutation = Convert.ToString(amCustomers_Result.SelectToken("title")),
                        Fax = Convert.ToString(amCustomers_Result.SelectToken("fax_number")),
                        Comments = Convert.ToString(amCustomers_Result.SelectToken("notes")),
                        ContactType = "Individual",
                        CompanyName = Convert.ToString(amCustomers_Result.SelectToken("company_name")),
                        AMCustomerId = Convert.ToString(amCustomers_Result.SelectToken("row_id")),
                        Approvalestatus = "Pending_Approval",
                        Id = suiteConstactId
                    };

                    var request = new InsertRequest
                    {
                        SessionId = suitesessionId,
                        ModuleName = "Contacts",
                        Entity = contactrequest
                    };
                    var resp = suiteWrapper.Update(request).GetAwaiter().GetResult();
                    common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Updated Customer RecordID :" + resp.Id, Name = "InsertSuiteContact", Entity_c = "Customer" }, logId);
                    SuiteWrapper.WriteTraceLog("InsertSuiteContact", "Updated Customer RecordID :" + resp.Id);
                    return resp.Id;
                }
            }
            catch (Exception ex)
            {
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "InsertSuiteContact", Entity_c = "Customer" }, logId);
                SuiteWrapper.WriteTraceLog("InsertSuiteContact", "Error : " + ex.Message);
            }
            return "";
        }

        private string InsertSuiteContact(JToken AMCustomers_Result, string logId)
        {
            try
            {
                // Based on Email-id search Customer Record and update record
                var custEmail = Convert.ToString(AMCustomers_Result.SelectToken("preferred_email"));
                var suiteContact = common.GetSuiteContactIDByEmail(custEmail, logId);
                var internalID = string.Empty;


                if (suiteContact.Count() > 0)
                {
                    if (suiteContact.Count() == 1)
                    {
                        foreach (EntryListArray contact in suiteContact)
                        {
                            internalID = contact.Id;
                            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Only one record exist for email id :'" + custEmail + "' in Suite", Name = "InsertSuiteContact", Entity_c = "Customer" }, logId);
                            SuiteWrapper.WriteTraceLog("InsertSuiteContact", "Only one record exist for email id :'" + custEmail + "' in Suite");
                        }
                    }
                    else
                    {
                        if (custEmail != "khorshed@pundoles.com")
                        {
                            foreach (var k in suiteContact)
                            {
                                internalID = k.Id;
                                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Email_c = custEmail, Description = "Multiple records exist for email id :'" + custEmail + "' in Suite: ", Name = "InsertSuiteContact", Entity_c = "Customer" }, logId);
                                SuiteWrapper.WriteTraceLog("InsertSuiteContact", "Multiple records exist for email id :'" + custEmail + "' in Suite: ");
                            }
                        }
                    }
                }
                else
                {
                    // Create NetSuite Customer Record
                    var suiteWrapper = new SuiteWrapper();
                    suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

                    var contactrequest = new CreateContactRequest
                    {
                        Email = Convert.ToString(AMCustomers_Result.SelectToken("preferred_email")),
                        Phone = Convert.ToString(AMCustomers_Result.SelectToken("phone_number")),
                        FirstName = Convert.ToString(AMCustomers_Result.SelectToken("given_name")),
                        LastName = Convert.ToString(AMCustomers_Result.SelectToken("family_name")),
                        Salutation = Convert.ToString(AMCustomers_Result.SelectToken("title")),
                        Fax = Convert.ToString(AMCustomers_Result.SelectToken("fax_number")),
                        Comments = Convert.ToString(AMCustomers_Result.SelectToken("notes")),
                        ContactType = "Individual",
                        CompanyName = Convert.ToString(AMCustomers_Result.SelectToken("company_name")),
                        AMCustomerId = Convert.ToString(AMCustomers_Result.SelectToken("row_id")),
                        Approvalestatus = "Pending_Approval"
                    };

                    var request = new InsertRequest
                    {
                        SessionId = suitesessionId,
                        ModuleName = "Contacts",
                        Entity = contactrequest
                    };
                    var resp = suiteWrapper.Insert(request).GetAwaiter().GetResult();

                    common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Customer Inserted to Suite successfully for Id :" + resp.Id, Name = "InsertSuiteContact", Entity_c = "Customer" }, logId);
                    SuiteWrapper.WriteTraceLog("InsertSuiteContact", "Customer Inserted to Suite successfully for Id :" + resp.Id);

                    return resp.Id;
                }


            }
            catch (Exception ex)
            {
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "InsertSuiteContact", Entity_c = "Customer" }, logId);
                SuiteWrapper.WriteTraceLog("InsertSuiteContact", "Error : " + ex.Message);
            }

            return "";
        }
    }
}