using CMSIntegration.CMSModels;
using CMSIntegration.EntityModel;
using CMSIntegration.SuiteAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
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
                if (!string.IsNullOrEmpty(updateItemRequest.ClientID))
                {
                    var getRequest = new SuiteAPI.ReadEntryListRequest();
                    getRequest.SessionId = sessionId;
                    getRequest.MethodName = "get_entry_list";
                    getRequest.ModuleName = "Contacts";
                    getRequest.Query = "Contacts.id='" + updateItemRequest.ClientID + "'";
                    getRequest.SelectFields = new List<string>
                    {
                        "id",
                        "client_number_c"
                    };
                    var contactdetail = SuiteWrapper.GetList<GeneralContact>(getRequest).GetAwaiter().GetResult();
                    var listObjectConverted = SuiteWrapper.GetListObjectConverted<GeneralContact>(contactdetail);
                    if (listObjectConverted != null && listObjectConverted.Count > 0)
                    {
                        updateItemRequest.ClientNumber = listObjectConverted[0].ClientNumber;
                    }
                    else
                    {
                        SuiteWrapper.WriteTraceLog("Invalid contact!");
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid contact!");
                    }
                }



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
            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(newItemRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(newItemRequest) + " is : " + outputMessage);
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
                var getRequest = new SuiteAPI.ReadEntryListRequest();
                getRequest.SessionId = sessionId;
                getRequest.MethodName = "get_entry_list";
                getRequest.ModuleName = "Contacts";
                getRequest.Query = "Contacts.id='" + newItemRequest.ContactID + "'";
                getRequest.SelectFields = new List<string>
                {
                    "id",
                    "client_number_c"
                };
                var contactdetail = SuiteWrapper.GetList<GeneralContact>(getRequest).GetAwaiter().GetResult();
                var listObjectConverted = SuiteWrapper.GetListObjectConverted<GeneralContact>(contactdetail);
                if (listObjectConverted != null && listObjectConverted.Count > 0)
                {
                    newItemRequest.ClientNumber = listObjectConverted[0].ClientNumber;
                }
                else
                {
                    SuiteWrapper.WriteTraceLog("Invalid contact!");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid contact!");
                }


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

        [HttpGet]
        [ActionName("DeleteItem")]
        public HttpResponseMessage DeleteItem(string itemID)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'DeleteItem' with request :" + JsonConvert.SerializeObject(itemID));

            var sessionId = "0";
            string outputMessage = string.Empty;
            if (string.IsNullOrEmpty(itemID))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("itemID should not be null or emplty");
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "itemID should not be null or emplty"); ;
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
                req.SelectFields = new List<string>()
                {
                    "id",
                    "deleted"
                };
                req.Entity = new Dictionary<string, string>()
                {
                     { "id", itemID },
                     { "deleted", "1" },
                };
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

        [HttpGet]
        [ActionName("GetItems")]
        //Path: /api/Item/GetItems
        public HttpResponseMessage GetItems()
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'GetItems'");
            List<GetItemsData_Result> data = new List<GetItemsData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.GetItemsData().ToList();
                }

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Pulling the contact is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpPost]
        [ActionName("CreateProduct")]
        //Path: /api/Item/CreateItem
        public HttpResponseMessage CreateItem(ItemModel ItemModelRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'MoveItem' with request :" + JsonConvert.SerializeObject(ItemModelRequest));
            PushItemResponse pushItemResponse = new PushItemResponse();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    EntityModel.Item itemObject = new EntityModel.Item();
                    //itemObject.id = ItemModelRequest.id;
                    itemObject.name = ItemModelRequest.name;
                    itemObject.date_entered = ItemModelRequest.date_entered;
                    itemObject.date_modified = ItemModelRequest.date_modified;
                    itemObject.modified_user_id = ItemModelRequest.modified_user_id;
                    itemObject.created_by = ItemModelRequest.created_by;
                    itemObject.description = ItemModelRequest.description;
                    itemObject.deleted = Convert.ToBoolean(ItemModelRequest.deleted);
                    itemObject.assigned_user_id = ItemModelRequest.assigned_user_id;
                    itemObject.title = ItemModelRequest.title;
                    itemObject.lot_number = ItemModelRequest.lot_number;
                    itemObject.item_name = ItemModelRequest.item_name;
                    itemObject.item_size = ItemModelRequest.item_size;
                    itemObject.basic_description = ItemModelRequest.basic_description;
                    itemObject.name = ItemModelRequest.name;
                    itemObject.author = ItemModelRequest.author;
                    itemObject.region = ItemModelRequest.region;
                    itemObject.medium = ItemModelRequest.medium;
                    itemObject.region_and_date = ItemModelRequest.region_and_date;
                    itemObject.year_of_birth = ItemModelRequest.year_of_birth;
                    itemObject.school_general_description = ItemModelRequest.school_general_description;
                    itemObject.marked_stamped_dated = ItemModelRequest.marked_stamped_dated;
                    itemObject.date_of_painting = ItemModelRequest.date_of_painting;
                    itemObject.title_2 = ItemModelRequest.title_2;
                    itemObject.year_of_death = ItemModelRequest.year_of_death;
                    itemObject.item_date = ItemModelRequest.item_date;
                    itemObject.signature_details = ItemModelRequest.signature_details;
                    itemObject.low_resolution_image = ItemModelRequest.low_resolution_image;
                    itemObject.artist_school = ItemModelRequest.artist_school;
                    itemObject.photography = ItemModelRequest.photography;
                    itemObject.hsn_sacode = ItemModelRequest.hsn_sacode;
                    itemObject.high_estimate = Convert.ToDecimal(ItemModelRequest.high_estimate);
                    itemObject.commission_rate = Convert.ToInt32(ItemModelRequest.commission_rate);
                    itemObject.reserve = Convert.ToDecimal(ItemModelRequest.reserve);
                    itemObject.asi_registration_number = ItemModelRequest.asi_registration_number;
                    itemObject.base_rate = Convert.ToDecimal(ItemModelRequest.base_rate);
                    itemObject.low_estimate = Convert.ToDecimal(ItemModelRequest.low_estimate);
                    itemObject.ac1_auctionalendar_id = ItemModelRequest.ac1_auctionalendar_id;
                    itemObject.cat1ategories_id = ItemModelRequest.cat1ategories_id;
                    itemObject.loc_location_id = ItemModelRequest.loc_location_id;
                    //itemObject.id = ItemModelRequest.ItemId;
                    itemObject.contact_id = ItemModelRequest.contact_id;
                    itemObject.add1_addresses_id = ItemModelRequest.add1_addresses_id;
                    itemObject.a1_artists_id = ItemModelRequest.a1_artists_id;
                    context.Items.Add(itemObject);

                    context.SaveChanges();

                    pushItemResponse.itemid = Convert.ToInt32(itemObject.id);
                    pushItemResponse.status = "Success";

                    SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushItemResponse));
                    return Request.CreateResponse(HttpStatusCode.OK, pushItemResponse);
                }
            }
            catch (Exception ex)
            {
                pushItemResponse.itemid = null;
                pushItemResponse.status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while pushing data is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(pushItemResponse));
            }
        }

        [HttpPost]
        [ActionName("UpdateProduct")]
        //Path: /api/Contact/UpdateContact
        public HttpResponseMessage UpdateItem(ItemModel itemRequest)
        {
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'UpdateContact' with request :" + JsonConvert.SerializeObject(itemRequest));

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(itemRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(itemRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }

            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.Items.AsNoTracking().FirstOrDefault(m => m.id == itemRequest.id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("Contact doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Contact doesn't exist."); ;
                    }

                    //UpdEntity.id = itemRequest.id;
                    UpdEntity.name = itemRequest.name;
                    UpdEntity.date_entered = itemRequest.date_entered;
                    UpdEntity.date_modified = itemRequest.date_modified;
                    UpdEntity.modified_user_id = itemRequest.modified_user_id;
                    UpdEntity.created_by = itemRequest.created_by;
                    UpdEntity.description = itemRequest.description;
                    UpdEntity.deleted = Convert.ToBoolean(itemRequest.deleted);
                    UpdEntity.assigned_user_id = itemRequest.assigned_user_id;
                    UpdEntity.title = itemRequest.title;
                    UpdEntity.lot_number = itemRequest.lot_number;
                    UpdEntity.item_name = itemRequest.item_name;
                    UpdEntity.item_size = itemRequest.item_size;
                    UpdEntity.basic_description = itemRequest.basic_description;
                    UpdEntity.name = itemRequest.name;
                    UpdEntity.author = itemRequest.author;
                    UpdEntity.region = itemRequest.region;
                    UpdEntity.medium = itemRequest.medium;
                    UpdEntity.region_and_date = itemRequest.region_and_date;
                    UpdEntity.year_of_birth = itemRequest.year_of_birth;
                    UpdEntity.school_general_description = itemRequest.school_general_description;
                    UpdEntity.marked_stamped_dated = itemRequest.marked_stamped_dated;
                    UpdEntity.date_of_painting = itemRequest.date_of_painting;
                    UpdEntity.title_2 = itemRequest.title_2;
                    UpdEntity.year_of_death = itemRequest.year_of_death;
                    UpdEntity.item_date = itemRequest.item_date;
                    UpdEntity.signature_details = itemRequest.signature_details;
                    UpdEntity.low_resolution_image = itemRequest.low_resolution_image;
                    UpdEntity.artist_school = itemRequest.artist_school;
                    UpdEntity.photography = itemRequest.photography;
                    UpdEntity.hsn_sacode = itemRequest.hsn_sacode;
                    UpdEntity.high_estimate = Convert.ToDecimal(itemRequest.high_estimate);
                    UpdEntity.commission_rate = Convert.ToInt32(itemRequest.commission_rate);
                    UpdEntity.reserve = Convert.ToDecimal(itemRequest.reserve);
                    UpdEntity.asi_registration_number = itemRequest.asi_registration_number;
                    UpdEntity.base_rate = Convert.ToDecimal(itemRequest.base_rate);
                    UpdEntity.low_estimate = Convert.ToDecimal(itemRequest.low_estimate);
                    UpdEntity.ac1_auctionalendar_id = itemRequest.ac1_auctionalendar_id;
                    UpdEntity.cat1ategories_id = itemRequest.cat1ategories_id;
                    UpdEntity.loc_location_id = itemRequest.loc_location_id;
                    //UpdEntity.id = itemRequest.ItemId;
                    UpdEntity.contact_id = itemRequest.contact_id;
                    UpdEntity.add1_addresses_id = itemRequest.add1_addresses_id;
                    UpdEntity.a1_artists_id = itemRequest.a1_artists_id;
                    context.Entry(UpdEntity).State = EntityState.Modified;

                    context.SaveChanges();

                    SuiteWrapper.WriteTraceLog("Successfully called");
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                }
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while updating the contact is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString()); ;
            }
        }

    }
}
