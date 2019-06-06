using CMSIntegration.CMSModels;
using CMSIntegration.SuiteAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace CMSIntegration.Controller
{
    [RoutePrefix("Item")]
    public class ItemController : ApiController
    {
        [HttpPost]
        [ActionName("UploadLotNumber")]
        public HttpResponseMessage UploadLotNumber(UploadLotNumber[] uploadLotNumberRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'UploadLotNumber' with request :" + JsonConvert.SerializeObject(uploadLotNumberRequest));
            var sessionId = "0";
            string outputMessage = string.Empty;
            foreach (var data in uploadLotNumberRequest)
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

            PushResponseforMultipleEntries<UploadLotNumber> pushResponse = new PushResponseforMultipleEntries<UploadLotNumber>();
            var pushResponseResult = pushResponse.BuildResponse(sessionId, uploadLotNumberRequest, "Itm1_Item");

            SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseResult));
            return Request.CreateResponse(HttpStatusCode.OK, pushResponseResult);
        }

        [HttpPost]
        [ActionName("MoveItem")]
        public HttpResponseMessage MoveItem(MoveItem moveItemRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'MoveItem' with request :" + JsonConvert.SerializeObject(moveItemRequest));
            var sessionId = "0";
            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(moveItemRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(moveItemRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
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

            PushResponseforSingleEntry pushResponseforSingleEntry = new PushResponseforSingleEntry();
            try
            {
                var req = new AddUpdateSingleEntryRequest();
                req.SessionId = sessionId;
                req.ModuleName = "Itm1_Item";
                req.Entity = moveItemRequest;
                var response = SuiteWrapper.AddUpdateSingleEntry(req).GetAwaiter().GetResult();
                pushResponseforSingleEntry.Id = response.Id;
                pushResponseforSingleEntry.Status = "Success";

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseforSingleEntry));
                return Request.CreateResponse(HttpStatusCode.OK, pushResponseforSingleEntry);
            }
            catch (Exception ex)
            {
                pushResponseforSingleEntry.Id = null;
                pushResponseforSingleEntry.Status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while pushing data is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, pushResponseforSingleEntry);

            }
        }

        [HttpPost]
        [ActionName("MergeItem")]
        public HttpResponseMessage MergeItem(MergeItem mergeItemRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'MergeItem' with request :" + JsonConvert.SerializeObject(mergeItemRequest));
            var sessionId = "0";
            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(mergeItemRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(mergeItemRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
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

            PushResponseforSingleEntry pushResponseforSingleEntry = new PushResponseforSingleEntry();
            try
            {
                var req = new AddUpdateSingleEntryRequest();
                req.SessionId = sessionId;
                req.ModuleName = "Itm1_Item";
                req.Entity = mergeItemRequest;
                var response = SuiteWrapper.AddUpdateSingleEntry(req).GetAwaiter().GetResult();
                pushResponseforSingleEntry.Id = response.Id;
                pushResponseforSingleEntry.Status = "Success";

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseforSingleEntry));
                return Request.CreateResponse(HttpStatusCode.OK, pushResponseforSingleEntry);
            }
            catch (Exception ex)
            {
                pushResponseforSingleEntry.Id = null;
                pushResponseforSingleEntry.Status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while Pushing data is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, pushResponseforSingleEntry);

            }
        }

        [HttpPost]
        [ActionName("UpdateItem")]
        public HttpResponseMessage UpdateItem(UpdateItem updateItemRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'UpdateItem' with request :" + JsonConvert.SerializeObject(updateItemRequest));
            var sessionId = "0";
            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(updateItemRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(updateItemRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
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

            PushResponseforSingleEntry pushResponseforSingleEntry = new PushResponseforSingleEntry();
            try
            {
                var req = new AddUpdateSingleEntryRequest();
                req.SessionId = sessionId;
                req.ModuleName = "Itm1_Item";
                req.Entity = updateItemRequest;
                var response = SuiteWrapper.AddUpdateSingleEntry(req).GetAwaiter().GetResult();
                pushResponseforSingleEntry.Id = response.Id;
                pushResponseforSingleEntry.Status = "Success";

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseforSingleEntry));
                return Request.CreateResponse(HttpStatusCode.OK, pushResponseforSingleEntry);
            }
            catch (Exception ex)
            {
                pushResponseforSingleEntry.Id = null;
                pushResponseforSingleEntry.Status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while pushing data is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, pushResponseforSingleEntry);

            }
        }

        [HttpPost]
        [ActionName("NewItem")]
        public HttpResponseMessage NewItem(NewItem newItemRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'NewItem' with request :" + JsonConvert.SerializeObject(newItemRequest));
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

            PushResponseforSingleEntry pushResponseforSingleEntry = new PushResponseforSingleEntry();
            try
            {

                var req = new AddUpdateSingleEntryRequest();
                req.SessionId = sessionId;
                req.ModuleName = "Itm1_Item";
                req.Entity = newItemRequest;
                var response = SuiteWrapper.AddUpdateSingleEntry(req).GetAwaiter().GetResult();

                pushResponseforSingleEntry.Id = response.Id;
                pushResponseforSingleEntry.Status = "Success";

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushResponseforSingleEntry));
                return Request.CreateResponse(HttpStatusCode.OK, pushResponseforSingleEntry);
            }
            catch (Exception ex)
            {
                pushResponseforSingleEntry.Id = null;
                pushResponseforSingleEntry.Status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while pulling data is : " + ex.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, pushResponseforSingleEntry);
            }
        }
    }
}
