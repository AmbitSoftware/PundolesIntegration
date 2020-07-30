using AMIntegration.Common;
using AMIntegration.SugarCrmModels;
using AMIntegration.SuiteAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;

namespace AMIntegration.Modules.Registration
{
    public class GetRegistration
    {
        string suitesessionId = string.Empty;
        CommonClass common = new CommonClass();

        public GetListofAMRegistrationResponse GetListofAMRegistration(string logId)
        {

            SuiteWrapper.WriteTraceLog("GetRegistration", "---------------- REGISTRATION SYNC START ------------------------- ");
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Registration sync started", Name = "GetRegistration", Entity_c = "Registration" }, logId);

            var amWrapper = new AMWrapper();
            var ret = new GetListofAMRegistrationResponse();

            try
            {
                var req = new ReadEntryListRequest();
                req.AccessToken = amWrapper.apiAccessToken;
                req.ModuleName = "auction";
                var AMRegistration_List = amWrapper.GetAMList<AMCustomer.AMAuction>(req, $"{amWrapper.apiUrl}/v1/auction/upcoming").GetAwaiter().GetResult();

                var result_page = JObject.Parse(AMRegistration_List).SelectToken("result_page");

                var listOfUpcomingAuction = ReadAllAMAuctionUpcoming(result_page);

                ProcessSuiteRegistration(listOfUpcomingAuction, logId);

                var query_info = JObject.Parse(AMRegistration_List).SelectToken("query_info");
                var next_page_url = string.Empty;

                foreach (var next_page in query_info.SelectTokens("next_page"))
                {
                    next_page_url = next_page.ToString();
                    while (!string.IsNullOrEmpty(next_page_url))
                    {
                        var jstr_Next = amWrapper.GetAMList<AMCustomer.AMAuction>(req, next_page_url).GetAwaiter().GetResult();
                        var result_page_Next = JObject.Parse(jstr_Next).SelectToken("result_page");
                        listOfUpcomingAuction = ReadAllAMAuctionUpcoming(result_page_Next);
                        ProcessSuiteRegistration(listOfUpcomingAuction, logId);

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
                SuiteWrapper.WriteTraceLog("GetListofAMRegistration", "Error Message : " + ex.Message);
                ret.StatusCode = AMAPI.ClsConstant.ServerErrorCode;
                ret.Message = AMAPI.ClsConstant.ServerErrorMsg;

                return ret;
            }
        }
        //Past Data
        public GetListofAMRegistrationResponse GetListofAMRegistrationPast(DateTime startDate, DateTime endDate, string logId)
        {

            SuiteWrapper.WriteTraceLog("GetRegistration", "---------------- REGISTRATION SYNC START ------------------------- ");
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Registration sync started", Name = "GetRegistration", Entity_c = "Registration" }, logId);

            var amWrapper = new AMWrapper();
            var ret = new GetListofAMRegistrationResponse();

            try
            {
                var req = new ReadEntryListRequest();
                req.AccessToken = amWrapper.apiAccessToken;
                req.ModuleName = "auction";
                var AMRegistration_List = amWrapper.GetAMList<AMCustomer.AMAuction>(req, $"{amWrapper.apiUrl}/v1/auction/past").GetAwaiter().GetResult();

                var result_page = JObject.Parse(AMRegistration_List).SelectToken("result_page");

                var listOfUpcomingAuction = ReadAllAMAuctionPast(result_page, startDate, endDate);

                ProcessSuiteRegistration(listOfUpcomingAuction, logId);

                var query_info = JObject.Parse(AMRegistration_List).SelectToken("query_info");
                var next_page_url = string.Empty;

                foreach (var next_page in query_info.SelectTokens("next_page"))
                {
                    next_page_url = next_page.ToString();
                    while (!string.IsNullOrEmpty(next_page_url))
                    {
                        var jstr_Next = amWrapper.GetAMList<AMCustomer.AMAuction>(req, next_page_url).GetAwaiter().GetResult();
                        var result_page_Next = JObject.Parse(jstr_Next).SelectToken("result_page");
                        listOfUpcomingAuction = ReadAllAMAuctionPast(result_page_Next, startDate, endDate);
                        ProcessSuiteRegistration(listOfUpcomingAuction, logId);

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
                SuiteWrapper.WriteTraceLog("GetListofAMRegistration", "Error Message : " + ex.Message);
                ret.StatusCode = AMAPI.ClsConstant.ServerErrorCode;
                ret.Message = AMAPI.ClsConstant.ServerErrorMsg;

                return ret;
            }
        }
        //
        private List<AMCustomer.UpcomingAuction> ReadAllAMAuctionUpcoming(JToken result_page)
        {
            var listOfUpcomingAuction = new List<AMCustomer.UpcomingAuction>();

            foreach (var result in result_page)
            {
               

                var AmUpcomingAuction = new AMCustomer.UpcomingAuction();
                AmUpcomingAuction.Auction_id = Convert.ToString(result.SelectToken("row_id"));
                AmUpcomingAuction.Auction_startdate = Convert.ToString(result.SelectToken("time_start"));
                AmUpcomingAuction.Auction_title = Convert.ToString(result.SelectToken("title"));
                listOfUpcomingAuction.Add(AmUpcomingAuction);
            
            }

            return listOfUpcomingAuction;
        }
        private List<AMCustomer.UpcomingAuction> ReadAllAMAuctionPast(JToken result_page, DateTime startDate, DateTime endDate)
        {
            var listOfUpcomingAuction = new List<AMCustomer.UpcomingAuction>();

            foreach (var result in result_page)
            {
                if (Convert.ToDateTime(result.SelectToken("time_start")) >= startDate && Convert.ToDateTime(result.SelectToken("time_start")) <= endDate)
                {

                    var AmUpcomingAuction = new AMCustomer.UpcomingAuction();
                    AmUpcomingAuction.Auction_id = Convert.ToString(result.SelectToken("row_id"));
                    AmUpcomingAuction.Auction_startdate = Convert.ToString(result.SelectToken("time_start"));
                    AmUpcomingAuction.Auction_title = Convert.ToString(result.SelectToken("title"));
                    listOfUpcomingAuction.Add(AmUpcomingAuction);
                }
            }

            return listOfUpcomingAuction;
        }
        private void ProcessSuiteRegistration(List<AMCustomer.UpcomingAuction> result_page, string logId)
        {
            foreach (var amAuction in result_page)
            {
                var auctionID = amAuction.Auction_id;
                var auctionIntegrationId = GetAuctionIntegrationId(auctionID);
                var auctionSummaryArray = GetAuctionSummary(auctionID, logId);
                var auctionStartDate = auctionSummaryArray.Time_start;
                var auctionName = auctionSummaryArray.Truncated_description;

                var auctionType = auctionSummaryArray.Auction_type;
                var auctionTimeZone = auctionSummaryArray.Timezone;
                var auctionCode = auctionSummaryArray.Auction_code;
                var auctionViewingInfo = auctionSummaryArray.Viewing_information;
                var auctionCurrencyCode = auctionSummaryArray.Currency_code;
                var auctionCity = auctionSummaryArray.Location_name;

                var auctionCustomerIdArray = GetAuctionRegistrationArray(auctionID, logId);
                if (auctionCustomerIdArray.Count == 0)
                {
                    SuiteWrapper.WriteTraceLog("GetRegistration", "No any Registration exist for period");
                    common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "No any Auction Registration exist for period ", Name = "GetRegistration", Entity_c = "Registration" }, logId);
                }
                foreach (var ac in auctionCustomerIdArray)
                {
                    var paddle = ac.Paddle;
                    var custRecords = ac.Customer;
                    var amCustomerCreatedDate = ac.Created_at;
                    System.Diagnostics.Debug.WriteLine(ac.Customer_id);
                    var suiteContact = common.GetSuiteConstactID(ac.Customer_id, logId);
                    if (suiteContact != null)
                    {

                        var recordId = UpdateCustomerRecordRegistration(auctionIntegrationId, suiteContact, paddle, auctionStartDate, auctionName, auctionType, auctionTimeZone, auctionViewingInfo, auctionCurrencyCode, auctionCity, custRecords, logId);
                        SuiteWrapper.WriteTraceLog("GetRegistration", "Registration done for customer: " + ac.Customer_id);
                        common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = ac + ".Registration done for customer: " + ac.Customer_id, Name = "ProcessSuiteRegistration", Entity_c = "Registration" }, logId);
                    }
                    else
                    {
                        // Check for Customer based on email id.
                        var custEmail = ac.Customer.Email;
                        if (custEmail != "")
                        {
                            var SuiteContactList = common.GetSuiteContactIDByEmail(custEmail, logId);

                            foreach (var contact in SuiteContactList)
                            {
                                suiteContact.Id = contact.Id;

                                var recordId = UpdateCustomerRecordRegistration(auctionIntegrationId, suiteContact, paddle, auctionStartDate, auctionName, auctionType, auctionTimeZone, auctionViewingInfo, auctionCurrencyCode, auctionCity, custRecords, logId);
                                SuiteWrapper.WriteTraceLog("GetRegistration", "Registration done for customer: " + ac.Customer_id);
                                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = ac + "Registration done for customer: " + ac.Customer_id, Name = "ProcessSuiteRegistration", Entity_c = "Registration" }, logId);

                            }

                        }
                        else
                        {
                            SuiteWrapper.WriteTraceLog("GetRegistration", "For the customer " + ac.Customer.Firstname + " " + ac.Customer.Lastname + " AM CUSTOMER ID " + ac.Customer_id + " not found");
                            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = ac + ". For the customer " + ac.Customer.Firstname + " " + ac.Customer.Lastname + " AM CUSTOMER ID " + ac.Customer_id + " not found", Name = "ProcessSuiteRegistration", Entity_c = "Registration" }, logId);
                        }
                    }
                }
            }
        }

        // Get Auction registrations customer ID
        private List<AMCustomer.AMRegistration> GetAuctionRegistrationArray(string AuctionID, string LogId)
        {
            var amAuctionSummary = new List<AMCustomer.AMRegistration>();

            var amWrapper = new AMWrapper();
            var ret = new GetListofAMAuctionResponse();
            var GetListofAMAuctionReq = new GetListofAMAuctionRequest();
            ret.ReferenceID = GetListofAMAuctionReq.ReferenceID;

            try
            {
                var req = new ReadEntryListRequest();
                req.AccessToken = req.AccessToken;
                req.ModuleName = "auction";
                var list = amWrapper.GetAMList<AMCustomer.AMRegistration>(req, $"{amWrapper.apiUrl}/v2/admin/auction/" + AuctionID + "/registrations?fieldset=customer-addresses").GetAwaiter().GetResult();
                var result_page = JObject.Parse(list).SelectToken("result_page");

                foreach (var result in result_page)
                {
                    amAuctionSummary.Add(GetAuctionSummary(result));
                }

                var query_info = JObject.Parse(list).SelectToken("query_info");
                var next_page_url = string.Empty;

                foreach (var next_page in query_info.SelectTokens("next_page"))
                {
                    next_page_url = next_page.ToString();
                    while (!string.IsNullOrEmpty(next_page_url))
                    {
                        var jstr_Next = amWrapper.GetAMList<AMCustomer.AMAuction>(req, next_page_url).GetAwaiter().GetResult();
                        var result_page_Next = JObject.Parse(jstr_Next).SelectToken("result_page");

                        foreach (var result in result_page_Next)
                        {
                            amAuctionSummary.Add(GetAuctionSummary(result));
                        }

                        var query_info_Next = JObject.Parse(jstr_Next).SelectToken("query_info");
                        foreach (var next_page1 in query_info_Next.SelectTokens("next_page"))
                        {
                            next_page_url = string.Empty;
                            next_page_url = next_page1.ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("GetSuiteAddress", "Error : " + ex.Message);
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error Message : " + ex.Message, Name = "GetSuiteAddress", Entity_c = "Registration" }, LogId);
            }

            return amAuctionSummary;
        }

        private string GetAuctionIntegrationId(string aucId)
        {
            var amWrapper = new AMWrapper();
            var ret = new GetListofAMAuctionResponse();
            var getListofAMAuctionReq = new GetListofAMAuctionRequest();
            ret.ReferenceID = getListofAMAuctionReq.ReferenceID;
            var aucIntId = string.Empty;
            try
            {

                var req = new ReadEntryListRequest();
                req.AccessToken = req.AccessToken;
                req.ModuleName = "auction";
                var auction_Details = amWrapper.GetAMList<AMCustomer.AMAuction>(req, $"{amWrapper.apiUrl}/v1/auction/" + aucId + "/").GetAwaiter().GetResult();
                var result_page = JObject.Parse(auction_Details).SelectToken("response");

                foreach (var result in result_page.SelectTokens("row_id"))
                {
                    aucIntId = result.ToString();
                }
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("GetAuctionIntegrationId", "Error Message : " + ex.Message);
            }
            return aucIntId;
        }

        // Get Auction Summary
        private AMCustomer.AMAuctionSummary GetAuctionSummary(string auctionID, string logId)
        {
            var amWrapper = new AMWrapper();
            var ret = new GetListofAMAuctionResponse();
            var getListofAMAuctionReq = new GetListofAMAuctionRequest();
            ret.ReferenceID = getListofAMAuctionReq.ReferenceID;

            try
            {
                var req = new ReadEntryListRequest();
                req.AccessToken = req.AccessToken;
                req.ModuleName = "auction";
                var auctionDetails = amWrapper.GetAMList<AMCustomer.AMAuction>(req, $"{amWrapper.apiUrl}/v1/auction/" + auctionID + "/summary").GetAwaiter().GetResult();
                var result_page = JObject.Parse(auctionDetails).SelectToken("response");
                var amAuctionSummaryRecord = new AMCustomer.AMAuctionSummary();

                amAuctionSummaryRecord.Row_id = result_page.Value<string>("row_id");
                amAuctionSummaryRecord.Auction_type = result_page.Value<string>("auction_type");
                amAuctionSummaryRecord.Time_start = result_page.Value<string>("time_start");
                amAuctionSummaryRecord.Timezone = result_page.Value<string>("timezone");
                amAuctionSummaryRecord.Auction_code = result_page.Value<string>("auction_code");
                amAuctionSummaryRecord.Title = result_page.Value<string>("title");
                amAuctionSummaryRecord.Truncated_description = result_page.Value<string>("truncated_description");
                amAuctionSummaryRecord.Viewing_information = result_page.Value<string>("viewing_information");
                amAuctionSummaryRecord.Currency_code = result_page.Value<string>("currency_code");
                amAuctionSummaryRecord.Location_name = result_page.Value<string>("location_name");

                return amAuctionSummaryRecord;
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("GetAuctionSummary", "Error : " + ex.Message);
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "GetAuctionSummary", Entity_c = "Registration" }, logId);
            }
            return null;
        }

        private AMCustomer.AMRegistration GetAuctionSummary(JToken result)
        {
            var AMAuctionSummaryRecord = new AMCustomer.AMRegistration();
            AMAuctionSummaryRecord.Customer_id = Convert.ToString(result.SelectToken("customer_id"));
            AMAuctionSummaryRecord.Paddle = Convert.ToString(result.SelectToken("paddle"));
            AMAuctionSummaryRecord.Created_at = Convert.ToString(result.SelectToken("created_at"));

            var amAddress = result.SelectToken("customer").SelectToken("addresses");
            var AMCustomerResult = new AMCustomer.AMCustomer
            {
                Addresses = new List<AMCustomer.AMAddress>()
            };

            foreach (var address in amAddress)
            {
                var addressresult = new AMCustomer.AMAddress
                {
                    Country_code = Convert.ToString(address.SelectToken("country_code")),
                    Locality = Convert.ToString(address.SelectToken("locality")),
                    Postal_code = Convert.ToString(address.SelectToken("postal_code")),
                    Region = Convert.ToString(address.SelectToken("region")),
                    Street_address = Convert.ToString(address.SelectToken("street_address")),
                    Type = Convert.ToString(address.SelectToken("type"))
                };
                AMCustomerResult.Addresses.Add(addressresult);
            }


            AMAuctionSummaryRecord.Customer = AMCustomerResult;
            return AMAuctionSummaryRecord;
        }

        // Add Registration details to Customer subTab
        private string UpdateCustomerRecordRegistration(string auctionInternalId, SuiteContact suiteContact, string paddle, string startDate, string auctionName, string auctionType, string auctionTimeZone, string auctionViewingInfo, string auctionCurrencyCode, string auctionCity, AMCustomer.AMCustomer amCustomer, string logId)
        {
            try
            {
                if (auctionInternalId != "" && suiteContact.Id != "")
                {
                    if (amCustomer.Addresses.Count() > 0)
                    {
                        var suiteCustomerAddressRecord = GetSuiteAddressCount(suiteContact.Id, logId, amCustomer.Addresses, suiteContact.Name);
                        //create address subrecord
                        if (suiteCustomerAddressRecord == 0)
                        {
                            foreach (var amAddress in amCustomer.Addresses)
                            {
                                InsertSuiteAddress(amAddress, suiteContact, logId);
                            }
                        }
                    }
                }

                if (CheckForAuctionAvailability(auctionInternalId, logId))
                {

                    var recordID = CheckForRegistration(auctionInternalId, suiteContact, paddle, auctionName, logId);

                    return recordID;
                }

            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("UpdateCustomerRecordRegistration", "Error Message : " + ex.Message);
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "UpdateCustomerRecordRegistration", Entity_c = "Registration" }, logId);
            }
            return "";
        }
        private bool SendMail(string suiteContactName)
        {
            try
            {
                string mail_From = ConfigurationManager.AppSettings["mail_From"];
                string mail_To = ConfigurationManager.AppSettings["mail_To"];
                string mail_Cc = ConfigurationManager.AppSettings["mail_Cc"];
                string Smtp_Mail_Server = ConfigurationManager.AppSettings["Smtp_Mail_Server"];
                string SmtpServer_UserID = ConfigurationManager.AppSettings["SmtpServer_UserID"];
                string SmtpServer_password = ConfigurationManager.AppSettings["SmtpServer_password"];
                int SmtpServer_port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpServer_port"]);

                MailMessage mail = new MailMessage();
                System.Net.Mail.SmtpClient SmtpServer = new SmtpClient(Smtp_Mail_Server);
                mail.From = new MailAddress(mail_From);
                mail.To.Add(mail_To);                      
                mail.Subject = "Auction Mobility: Update in address details ";
                mail.Body = @"Dear Both,
<br>
<br>
Please note that the address details of " + suiteContactName + @" have been updated.  Kindly check Auction Mobility and if accurate, update these details in Suite CRM.
<br>
<br>
<br>
<br>
Thanks and regards
<br>
Pundoles CRM";
                mail.IsBodyHtml = true;
                SmtpServer.Port = SmtpServer_port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpServer_UserID, SmtpServer_password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("SendMail", "Error Message : " + ex.Message);
                return false;
            }
        }

        private void InsertSuiteAddress(AMCustomer.AMAddress addrDetails, SuiteContact suiteContact, string logId)
        {
            try
            {

                var suiteWrapper = new SuiteWrapper();
                suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

                var addressrequest = new CreateAddressRequest();

                addressrequest.Name = suiteContact.Name;
                addressrequest.Country = addrDetails.Country_code;
                addressrequest.Address1 = addrDetails.Street_address;
                addressrequest.City = addrDetails.Locality;
                addressrequest.Client_number_c = suiteContact.Client_number_c;
                addressrequest.State = addrDetails.Region;

                addressrequest.Zip = addrDetails.Postal_code;

                if (addrDetails.Type == "home" || addrDetails.Type == null)
                    addressrequest.Shipping_address = true;

                if (addrDetails.Type == "billing")
                    addressrequest.Billing_address = true;


                addressrequest.Contacts_add1_addresses_1contacts_ida = suiteContact.Id;

                var request = new InsertRequest();
                request.SessionId = suitesessionId;
                request.ModuleName = "add1_Addresses";
                request.Entity = addressrequest;
                var resp = suiteWrapper.Insert(request).GetAwaiter().GetResult();
                
                if (resp == null)
                {
                    SuiteWrapper.WriteTraceLog("InsertSuiteAddress", "Error : In updating Address for client Number");
                }               

            }
            catch (Exception ex)
            {
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "InsertSuiteAddress", Entity_c = "Registration" }, logId);
                SuiteWrapper.WriteTraceLog("InsertSuiteAddress", "Error : " + ex.Message);
            }

        }

        private int GetSuiteAddressCount(string ContactId, string LogId,List< AMCustomer.AMAddress> AMAddresses, string customerName)
        {
            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            GetRelationshipsRequest request = new GetRelationshipsRequest
            {
                session = suitesessionId,
                module_id = ContactId,
                module_name = "Contacts",
                link_field_name = "contacts_add1_addresses_1",
                related_fields = new List<string>() { "id", "primary_address_street_c" }
            };

            try
            {
                var suiteAddress = suiteWrapper.GetRelationship<GetRelationshipsResponse>(request).GetAwaiter().GetResult();
                if (suiteAddress.SugarEntryList.Count() > 0)
                {
                    SendEmailOnAddressUpdate(suiteAddress.SugarEntryList, AMAddresses, customerName);
                }
                return suiteAddress.SugarEntryList.Count();
            }

            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("GetSuiteAddress", "Error : " + ex.Message);
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "GetSuiteAddress", Entity_c = "Customer" }, LogId);
            }
            return -1;
        }

        private void SendEmailOnAddressUpdate(List<EntryListArray> SuiteAddress,List< AMCustomer.AMAddress> AMAddresses, string customerName)
        {
            foreach (var amAddress in AMAddresses)
            {
                var result = SuiteAddress.FirstOrDefault(x => x.NameValueList["primary_address_street_c"]["value"].ToString() == amAddress.Street_address);        
                if (result == null)
                {
                    SendMail(customerName);
                }
            }
        }

        // *****************Check For Auction Avaliability ********************************************
        private bool CheckForAuctionAvailability(string auctionInternalId, string logId)
        {
            try
            {
                if (GetListSuiteAuctionCalender(auctionInternalId, logId) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("CheckForAuctionAvailability", "Error Message : " + ex.Message);
                return false;
            }
        }

        // ********************Check customer Registration for Auction *******************************
        private string CheckForRegistration(string auctionInternalId, SuiteContact suiteContact, string paddle, string auctionName, string logId)
        {
            var recordID = "";
            var registerrequest = new CreateRegistrationRequest();
            registerrequest.Ac1_auction_calendar_id_c = auctionInternalId;
            registerrequest.Contact_id = suiteContact.Id;
            registerrequest.Paddle_number_c = paddle;
            registerrequest.Name = paddle + "-" + suiteContact.Name;

            try
            {
                var searchResultsRegistratioList = GetListSuiteRegistration(auctionInternalId, suiteContact.Id, logId);


                if (searchResultsRegistratioList!=null && searchResultsRegistratioList.Count() > 0)
                {
                    foreach (var r in searchResultsRegistratioList)
                    {
                        registerrequest.Id = Convert.ToString(((Newtonsoft.Json.Linq.JProperty)r).Value);
                        recordID = UpdateSuiteRegistration(registerrequest, logId);
                        return registerrequest.Id;
                    }
                }
                else
                {
                    recordID = InsertSuiteRegistration(registerrequest, logId);
                    registerrequest.Id = recordID;
                    recordID = UpdateSuiteRegistration(registerrequest, logId);
                }
                return recordID;
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("CheckForRegistration", "Error : " + ex.Message);
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "CheckForRegistration", Entity_c = "Registration" }, logId);
                throw ex;
            }
        }

        private string UpdateSuiteRegistration(CreateRegistrationRequest registerRequest, string logId)
        {

            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();
            var request = new InsertRequest();
            request.SessionId = suitesessionId;
            request.ModuleName = "reg_Registration";
            request.Entity = registerRequest;
            var resp = suiteWrapper.Update(request).GetAwaiter().GetResult();
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Updated Suite Registration successfully Id: " + resp.Id, Name = "UpdateSuiteRegistration", Entity_c = "Registration" }, logId);
            SuiteWrapper.WriteTraceLog("UpdateSuiteRegistration", "Updated Suite Registration successfully Id: " + resp.Id);
            return resp.Id;

        }

        private string InsertSuiteRegistration(CreateRegistrationRequest registerRequest, string logId)
        {

            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            var request = new InsertRequest();
            request.SessionId = suitesessionId;
            request.ModuleName = "reg_Registration";
            request.Entity = registerRequest;
            var resp = suiteWrapper.Insert(request).GetAwaiter().GetResult();
            if (resp != null)
            {
                UpdateContactRegistration(resp.Id, registerRequest.Contact_id, logId);
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Inserted Suite Registration successfully Id: " + resp.Id, Name = "InsertSuiteRegistration", Entity_c = "Registration" }, logId);
                SuiteWrapper.WriteTraceLog("InsertSuiteRegistration", "Inserted Suite Registration successfully Id: " + resp.Id);
                return resp.Id;
            }
            return null;
        }

        public void UpdateContactRegistration(string reg_id, string contact_id, string logId)
        {
            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            SetRelationShipRequest relationshipRequest = new SetRelationShipRequest();
            relationshipRequest.session = suitesessionId;
            relationshipRequest.module_name = "reg_Registration";
            relationshipRequest.module_id = reg_id;
            relationshipRequest.link_field_name = "contacts_reg_registration_1contacts_ida";
            relationshipRequest.related_ids = new List<string>() { contact_id };

            var relationshipResponse = suiteWrapper.SetRelationship(relationshipRequest).GetAwaiter().GetResult();
            if (relationshipResponse == null)
            {
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Failed to update Client id in Suite Registration Id: " + reg_id, Name = "InsertSuiteRegistration", Entity_c = "Registration" }, logId);
                SuiteWrapper.WriteTraceLog("InsertSuiteRegistration", "Failed to update Client id in Suite Registration  Id: " + reg_id);
            }

        }

        public GetListofSuiteContactResponse GetListSuiteContact(GetListofSuiteContactRequest GetSuiteContactRequest)
        {
            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            var request = new SuiteReadEntryListRequest();
            request.SessionId = suitesessionId;
            request.ModuleName = "Contacts";
            request.SelectFields = new List<string> {
                    "first_name",
                    "last_name",
                    "phone_work",
                    "title",
                    "phone_fax",
                    "description",
                    };

            var contacts_List = suiteWrapper.GetList(request).GetAwaiter().GetResult();

            for (var i = 0; i <= contacts_List.EntityList.Count(); i++)
            {
                var values = new Dictionary<string, string>
                {
                    { "method", "POST" },
                    { "input_type", "JSON" },
                    { "response_type", "JSON" },
                    { "email_address","test1@test.com" },
                    { "given_name", "ABC" }  ,
                    { "family_name", "Xyz" },
                    { "password", "1234567890" },
                    { "first_name",contacts_List.EntryList[i].Entity.SelectToken("first_name").ToString()},
                    { "last_name",contacts_List.EntryList[i].Entity.SelectToken("last_name").ToString()},
                    { "phone_work",contacts_List.EntryList[i].Entity.SelectToken("phone_work").ToString()},
                    { "title",contacts_List.EntryList[i].Entity.SelectToken("title").ToString()},
                    { "phone_fax",contacts_List.EntryList[i].Entity.SelectToken("phone_fax").ToString()},
                    { "description",contacts_List.EntryList[i].Entity.SelectToken("description").ToString()}
                };
            }

            return null;
        }

        public EntryListArray GetListSuiteAuctionCalender(string id, string logId)
        {
            var amWrapper = new AMWrapper();
            var suiteWrapper = new SuiteWrapper();
            var contactrequest = new CreateContactRequest();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            var request = new SuiteReadEntryListRequest();
            request.SessionId = suitesessionId;
            request.ModuleName = "AC1_Auction_Calendar";
            request.Query = "AC1_Auction_Calendar_cstm.am_customer_id_c='" + id + "'";
            request.SelectFields = new List<string> {
                    "name",
                    "id"
                    };

            var auction_List = suiteWrapper.GetList(request).GetAwaiter().GetResult();
            if (auction_List.EntityList == null)
                return null;

            var auction_List_Cnt = auction_List.EntityList.Count();

            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Total Auction Calender in Suite: " + auction_List_Cnt, Name = "GetListSuiteAuctionCalender", Entity_c = "Registration" }, logId);
            if (auction_List.EntityList.Count() == 0) return null;
            for (var i = 0; i <= auction_List_Cnt; i++)
            {
                return auction_List.EntryList[i];
            }
            return null;
        }

        public JToken GetListSuiteRegistration(string auctionCalenderId, string clientId, string logId)
        {
            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            var request = new SuiteReadEntryListRequest();
            request.SessionId = suitesessionId;
            request.ModuleName = "reg_Registration";
            request.Query = "reg_Registration_cstm.ac1_auction_calendar_id_c='" + auctionCalenderId + "'";
            //request.Query = $"reg_Registration_cstm.ac1_auction_calendar_id_c='{auctionCalenderId}' and id in ( SELECT contacts_reg_registration_1reg_registration_idb  FROM contacts_reg_registration_1_c  where contacts_reg_registration_1contacts_ida = '{clientId}') ";
            request.SelectFields = new List<string> {
                    "id",
                    };

            var registration_list = suiteWrapper.GetList(request).GetAwaiter().GetResult();

            foreach (var registratonRecord in registration_list.EntityList)
            {
                if (CheckForRegistrationAvailalibilityForContact(registratonRecord.SelectToken("id").ToString(), clientId, logId) > 0)
                    return  registratonRecord;
            }
            return null; 
        }

        private int CheckForRegistrationAvailalibilityForContact(string registrationId,string clientId, string logId)
        {
            var suiteWrapper = new SuiteWrapper();
            suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

            GetRelationshipsRequest request = new GetRelationshipsRequest
            {
                session = suitesessionId,
                module_id = registrationId,
                module_name = "reg_Registration",
                link_field_name = "contacts_reg_registration_1",
                related_fields = new List<string>() {"id"  },
                related_module_query= $"contacts_reg_registration_1contacts_ida='{clientId}'" 
            };

            try
            {
                var registration_List = suiteWrapper.GetRelationship<GetRelationshipsResponse>(request).GetAwaiter().GetResult();
                return registration_List.SugarEntryList.Count();
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("GetSuiteAddress", "Error : " + ex.Message);
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error : " + ex.Message, Name = "GetSuiteAddress", Entity_c = "Customer" }, logId);
            }
            return -1;

        }

    }
}