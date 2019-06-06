using CMSIntegration.CMSModels;
using CMSIntegration.EntityModel;
using CMSIntegration.SuiteAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace CMSIntegration.Controller
{
    [RoutePrefix("SyncMasters")]
    public class SyncMastersController : ApiController
    {
        public static string lastestSyncedDate = string.Empty;

        [HttpGet]
        [ActionName("GetModulesDetails")]
        public HttpResponseMessage GetModulesDetails()
        {
            var sessionId = "0";
            try
            {
                sessionId = SuiteWrapper.Login().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message.ToString());
            }
            try
            {
                var req = new SuiteAPI.ReadEntryListRequest();
                req.SessionId = sessionId;
                req.ModuleName = "Contacts";
                req.MethodName = "get_module_fields";
                var list = SuiteWrapper.GetModuleList<CMSModels.ModuleDetails>(req).GetAwaiter().GetResult();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpPost]
        [ActionName("PushArtists")]
        public HttpResponseMessage PushArtists(PushArtist[] artistRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'PushArtists' with request :" + JsonConvert.SerializeObject(artistRequest));

            // Validating
            string outputMessage = string.Empty;
            foreach (var data in artistRequest)
            {
                if (!SuiteWrapper.ValidateRequest(data, out outputMessage))
                {
                    //Trace Log
                    SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(data) + " is : " + outputMessage);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
                }
            }
            var sessionId = "0";
            try
            {
                sessionId = SuiteWrapper.Login().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while generating session id is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message.ToString());
            }

            PushResponseforMultipleEntries<PushArtist> pushResponse = new PushResponseforMultipleEntries<PushArtist>();
            var pushResponseResult = pushResponse.BuildResponse(sessionId, artistRequest, "A1_Artists");

            SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseResult));
            return Request.CreateResponse(HttpStatusCode.OK, pushResponseResult);
        }

        [HttpPost]
        [ActionName("PushAuctions")]
        public HttpResponseMessage PushAuctions(PushAuctionCalendar[] auctionCalendarRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'PushAuctions' with request :" + JsonConvert.SerializeObject(auctionCalendarRequest));
            var sessionId = "0";
            // Validating
            string outputMessage = string.Empty;
            foreach (var data in auctionCalendarRequest)
            {
                if (!SuiteWrapper.ValidateRequest(data, out outputMessage))
                {
                    //Trace Log
                    SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(data) + " is : " + outputMessage);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
                }
            }
            try
            {
                sessionId = SuiteWrapper.Login().GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while generating session id is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message.ToString());
            }
            try
            {
                PushResponseforMultipleEntries<PushAuctionCalendar> pushResponse = new PushResponseforMultipleEntries<PushAuctionCalendar>();
                var pushResponseResult = pushResponse.BuildResponse(sessionId, auctionCalendarRequest, "AC1_Auction_Calendar");

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseResult));

                return Request.CreateResponse(HttpStatusCode.OK, pushResponseResult);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Adding/updating the data is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpPost]
        [ActionName("PushSymbols")]
        public HttpResponseMessage PushSymbols(PushSymbols[] symbolsRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'PushSymbols' with request :" + JsonConvert.SerializeObject(symbolsRequest));
            var sessionId = "0";
            // Validating
            string outputMessage = string.Empty;
            foreach (var data in symbolsRequest)
            {
                if (!SuiteWrapper.ValidateRequest(data, out outputMessage))
                {
                    //Trace Log
                    SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(data) + " is : " + outputMessage);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
                }
            }
            try
            {
                sessionId = SuiteWrapper.Login().GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while generating session id is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message.ToString());
            }
            try
            {
                var request = new AddUpdateMultipleEntriesRequest();
                request.SessionId = sessionId;
                request.ModuleName = "Sym1_Symbols";
                request.Entity = symbolsRequest;
                var response = SuiteWrapper.AddUpdateMultipleEntries(request).GetAwaiter().GetResult();

                PushResponseforMultipleEntries<PushSymbols> pushResponse = new PushResponseforMultipleEntries<PushSymbols>();
                var pushResponseResult = pushResponse.BuildResponse(sessionId, symbolsRequest, "Sym1_Symbols");

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseResult));

                return Request.CreateResponse(HttpStatusCode.OK, pushResponseResult);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Adding/updating the data is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpPost]
        [ActionName("PushLocations")]
        public HttpResponseMessage PushLocations(PushLocation[] locationRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'PushLocations' with request :" + JsonConvert.SerializeObject(locationRequest));
            var sessionId = "0";
            // Validating
            string outputMessage = string.Empty;
            foreach (var data in locationRequest)
            {
                if (!SuiteWrapper.ValidateRequest(data, out outputMessage))
                {
                    //Trace Log
                    SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(data) + " is : " + outputMessage);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
                }
            }
            try
            {
                sessionId = SuiteWrapper.Login().GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while generating session id is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message.ToString());
            }
            try
            {
                PushResponseforMultipleEntries<PushLocation> pushResponse = new PushResponseforMultipleEntries<PushLocation>();
                var pushResponseResult = pushResponse.BuildResponse(sessionId, locationRequest, "loc_Location");

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseResult));

                return Request.CreateResponse(HttpStatusCode.OK, pushResponseResult);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Adding/updating the data is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpPost]
        [ActionName("PushCategories")]
        public HttpResponseMessage PushCategories(PushCategories[] CategoriesRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'PushCategories' with request :" + JsonConvert.SerializeObject(CategoriesRequest));
            var sessionId = "0";
            // Validating
            string outputMessage = string.Empty;
            foreach (var data in CategoriesRequest)
            {
                if (!SuiteWrapper.ValidateRequest(data, out outputMessage))
                {
                    //Trace Log
                    SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(data) + " is : " + outputMessage);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
                }
            }
            try
            {
                sessionId = SuiteWrapper.Login().GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while generating session id is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message.ToString());
            }
            try
            {
                PushResponseforMultipleEntries<PushCategories> pushResponse = new PushResponseforMultipleEntries<PushCategories>();
                var pushResponseResult = pushResponse.BuildResponse(sessionId, CategoriesRequest, "Cat1_Categories");

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseResult));

                return Request.CreateResponse(HttpStatusCode.OK, pushResponseResult);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Adding/updating the data is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpGet]
        [ActionName("PullMasterData")]
        public HttpResponseMessage PullMasterData(string methodName, string lastSyncDate = null, int? nextOffSet = null, bool deleted = false)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'PullMasterData' with request : MethodName-" + methodName + " and nextOffSet=" + nextOffSet);
            var sessionId = "0";
            try
            {
                sessionId = SuiteWrapper.Login().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while generating session id is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message.ToString());
            }

            var query = string.Empty;
            var req = new SuiteAPI.ReadEntryListRequest();
            req.SessionId = sessionId;
            req.MethodName = "get_entry_list";
            req.MaxResults = SuiteWrapper.maxPullResults;
            req.Deleted = deleted;
            if (nextOffSet != null && nextOffSet > 0)
                req.Offset = nextOffSet;
            int v = (req.Deleted) ? 1 : 0;
            try
            {
                if (methodName == "Artists")
                {
                    req.ModuleName = "A1_Artists";
                    PropertyInfo[] props = typeof(PullArtist).GetProperties();
                    req.SelectFields = SuiteAPI.SuiteWrapper.GetFieldList(props);
                    query = "A1_Artists.deleted=" + Convert.ToInt32((req.Deleted) ? 1 : 0) + " ";
                    if (!string.IsNullOrEmpty(lastSyncDate))
                        req.Query = query + "and A1_Artists.date_modified >='" + lastSyncDate + "'";
                    else
                        req.Query = query;
                    var list = SuiteWrapper.GetList<PullArtist>(req).GetAwaiter().GetResult();
                    var pullResponse = PullResponse<PullArtist>.pullResponse(list, methodName, deleted, lastSyncDate);
                    SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pullResponse));

                    return Request.CreateResponse(HttpStatusCode.OK, pullResponse);

                }
                else if (methodName == "Auctions")
                {
                    req.ModuleName = "AC1_Auction_Calendar";
                    PropertyInfo[] props = typeof(PullAuctionCalendar).GetProperties();
                    query = "AC1_Auction_Calendar.deleted=" + Convert.ToInt32((req.Deleted) ? 1 : 0) + " ";
                    if (!string.IsNullOrEmpty(lastSyncDate))
                        req.Query = query + "and AC1_Auction_Calendar.date_modified >='" + lastSyncDate + "'";
                    else
                        req.Query = query;
                    var list = SuiteWrapper.GetList<PullAuctionCalendar>(req).GetAwaiter().GetResult();
                    var pullResponse = PullResponse<PullAuctionCalendar>.pullResponse(list, methodName, deleted, lastSyncDate);
                    SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pullResponse));

                    return Request.CreateResponse(HttpStatusCode.OK, pullResponse);
                }
                else if (methodName == "Symbols")
                {
                    req.ModuleName = "Sym1_Symbols";
                    PropertyInfo[] props = typeof(PullSymbols).GetProperties();
                    req.SelectFields = SuiteAPI.SuiteWrapper.GetFieldList(props);
                    query = "Sym1_Symbols.deleted=" + Convert.ToInt32((req.Deleted) ? 1 : 0) + " ";
                    if (!string.IsNullOrEmpty(lastSyncDate))
                        req.Query = query + "and Sym1_Symbols.date_modified >='" + lastSyncDate + "'";
                    else
                        req.Query = query;
                    var list = SuiteWrapper.GetList<PullSymbols>(req).GetAwaiter().GetResult();
                    var pullResponse = PullResponse<PullSymbols>.pullResponse(list, methodName, deleted, lastSyncDate);
                    SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pullResponse));

                    return Request.CreateResponse(HttpStatusCode.OK, pullResponse);
                }
                else if (methodName == "Locations")
                {
                    req.ModuleName = "loc_Location";
                    PropertyInfo[] props = typeof(PullLocation).GetProperties();
                    req.SelectFields = SuiteAPI.SuiteWrapper.GetFieldList(props);
                    query = "loc_Location.deleted=" + Convert.ToInt32((req.Deleted) ? 1 : 0) + " ";
                    if (!string.IsNullOrEmpty(lastSyncDate))
                        req.Query = query + "and loc_Location.date_modified >='" + lastSyncDate + "'";
                    else
                        req.Query = query;
                    var list = SuiteWrapper.GetList<PullLocation>(req).GetAwaiter().GetResult();
                    var pullResponse = PullResponse<PullLocation>.pullResponse(list, methodName, deleted, lastSyncDate);
                    SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pullResponse));

                    return Request.CreateResponse(HttpStatusCode.OK, pullResponse);
                }
                else if (methodName == "Categories")
                {
                    req.ModuleName = "Cat1_Categories";
                    PropertyInfo[] props = typeof(PullCategories).GetProperties();
                    req.SelectFields = SuiteAPI.SuiteWrapper.GetFieldList(props);
                    query = "Cat1_Categories.deleted=" + Convert.ToInt32((req.Deleted) ? 1 : 0) + " ";
                    if (!string.IsNullOrEmpty(lastSyncDate))
                        req.Query = query + "and Cat1_Categories.date_modified >='" + lastSyncDate + "'";
                    else
                        req.Query = query;
                    var list = SuiteWrapper.GetList<PullCategories>(req).GetAwaiter().GetResult();
                    var pullResponse = PullResponse<PullCategories>.pullResponse(list, methodName, deleted, lastSyncDate);
                    SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pullResponse));

                    return Request.CreateResponse(HttpStatusCode.OK, pullResponse);
                }
                else
                {
                    SuiteWrapper.WriteTraceLog("Invalid Pull request method name : " + methodName);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Method name '" + methodName + "' not found.");
                }
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while pulling the data is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpGet]
        [ActionName("GetDropdownMasterData")]
        public HttpResponseMessage GetDropdownMasterData()
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'GetDropdownMasterData'");
            List<GetDropDownMasterData_Result> data = new List<GetDropDownMasterData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {

                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.GetDropDownMasterData().ToList();
                }
                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while pulling the data is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

    }
}
