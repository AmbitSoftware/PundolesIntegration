using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using CMSIntegration.CMSModels;
using CMSIntegration.EntityModel;
using CMSIntegration.SuiteAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMSIntegration.Controller
{
    [RoutePrefix("Contact")]
    public class ContactController : ApiController
    {
        [HttpPost]
        [ActionName("CreateContact")]
        //Path: /api/Contact/CreateContact
        public HttpResponseMessage CreateContact(ContactModel ContactModelRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'MoveItem' with request :" + JsonConvert.SerializeObject(ContactModelRequest));
            PushContactResponse pushContactResponse = new PushContactResponse();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    contact contactObject = new contact();
                    contactObject.salutation = ContactModelRequest.salutation;
                    contactObject.first_name = ContactModelRequest.first_name;
                    contactObject.last_name = ContactModelRequest.last_name;
                    contactObject.company_name = ContactModelRequest.company_name;
                    contactObject.contact_type = ContactModelRequest.contact_type;
                    contactObject.client_number = ContactModelRequest.client_number;
                    contactObject.interest_id = ContactModelRequest.interest_id;
                    contactObject.category_id = ContactModelRequest.category_id;
                    contactObject.customer_category_id = ContactModelRequest.customer_category_id;
                    contactObject.level_id = ContactModelRequest.level_id;
                    contactObject.catalogue_id = ContactModelRequest.catalogue_id;
                    contactObject.marital_status_id = ContactModelRequest.marital_status_id;
                    contactObject.marriage_anniversary_date = ContactModelRequest.marriage_anniversary_date;
                    contactObject.am_customer_id = ContactModelRequest.am_customer_id;
                    contactObject.approval_status_id = ContactModelRequest.approval_status_id;
                    contactObject.authorized_to_bid_id = ContactModelRequest.authorized_to_bid_id;
                    contactObject.email = ContactModelRequest.email;
                    contactObject.phone = ContactModelRequest.phone;
                    contactObject.fax = ContactModelRequest.fax;
                    contactObject.mobile = ContactModelRequest.mobile;
                    contactObject.other_phone = ContactModelRequest.other_phone;
                    contactObject.clients_vat_tin_no = ContactModelRequest.clients_vat_tin_no;
                    contactObject.aadhar_number = ContactModelRequest.aadhar_number;
                    contactObject.pan_no = ContactModelRequest.pan_no;
                    contactObject.date_created = DateTime.Now;
                    contactObject.date_modified = DateTime.Now;
                    contactObject.createdby_id = ContactModelRequest.createdby_id;
                    contactObject.modifiedby_id = ContactModelRequest.createdby_id;
                    context.contacts.Add(contactObject);

                    context.SaveChanges();

                    pushContactResponse.contactid = contactObject.contact_id;
                    pushContactResponse.status = "Success";

                    SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pushContactResponse));
                    return Request.CreateResponse(HttpStatusCode.OK, pushContactResponse);
                }
            }
            catch (Exception ex)
            {
                pushContactResponse.contactid = null;
                pushContactResponse.status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while pushing data is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(pushContactResponse));
            }
        }

        [HttpGet]
        [ActionName("PullContacts")]
        //Path: /api/Contact/PullContacts
        public HttpResponseMessage PullContacts(int? nextOffSet = null)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called PullClientData");
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

            try
            {
                var req = new SuiteAPI.ReadEntryListRequest();
                req.SessionId = sessionId;
                req.MethodName = "get_entry_list";
                req.ModuleName = "Contacts";
                req.MaxResults = SuiteWrapper.maxPullResults;
                if (nextOffSet != null && nextOffSet > 0)
                    req.Offset = nextOffSet;
                var obj = new Dictionary<string, object>()
                {
                     { "name", "contacts_add1_addresses_1" },
                     { "value", new List<string> { "id" , "primary_address_street_c", "primary_address_city_c", "primary_address_state_c", "primary_address_postalcode_c", "primary_address_country_c" } }

                };

                var arr = new ArrayList();
                arr.Add(obj);

                req.LinkNameToFieldsArray = arr;


                PropertyInfo[] props = typeof(PullContact).GetProperties();
                req.SelectFields = SuiteAPI.SuiteWrapper.GetFieldList(props);
                var list = SuiteWrapper.GetList<PullContact>(req).GetAwaiter().GetResult();
                var pullResponse = PullResponse<PullContact>.pullResponseforContact(list);


                // Append address with the contacts
                if (list.relationship_list.Length > 0)
                {
                    for (int i = 0; i < list.relationship_list.Length; i++)
                    {
                        if (list.relationship_list[i].link_list.Length > 0)
                        {
                            List<Addresses> Addresses = new List<Addresses>();
                            for (int j = 0; j < list.relationship_list[i].link_list[0].records.Length; j++)
                            {
                                var response = SuiteWrapper.GetListObjectConvertedforRelationShip<Addresses>(list.relationship_list[i].link_list[0].records[j].Entity);
                                Addresses.Add(response[0]);
                            }
                            pullResponse.data[i].ListOfAddresses = Addresses;
                        }

                    }
                }



                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(pullResponse));
                return Request.CreateResponse(HttpStatusCode.OK, pullResponse);
            }



            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while pulling data is : " + ex.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }



        [HttpPost]
        [ActionName("UpdateContact")]
        //Path: /api/Contact/UpdateContact
        public HttpResponseMessage UpdateContact(ContactModel contactRequest)
        {
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'UpdateContact' with request :" + JsonConvert.SerializeObject(contactRequest));

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(contactRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(contactRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }

            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.contacts.AsNoTracking().FirstOrDefault(m => m.contact_id == contactRequest.contact_id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("Contact doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Contact doesn't exist."); ;
                    }

                    UpdEntity.salutation = contactRequest.salutation;
                    UpdEntity.first_name = contactRequest.first_name;
                    UpdEntity.last_name = contactRequest.last_name;
                    UpdEntity.company_name = contactRequest.company_name;
                    UpdEntity.contact_type = contactRequest.contact_type;
                    UpdEntity.client_number = contactRequest.client_number;
                    UpdEntity.interest_id = contactRequest.interest_id;
                    UpdEntity.category_id = contactRequest.category_id;
                    UpdEntity.customer_category_id = contactRequest.customer_category_id;
                    UpdEntity.level_id = contactRequest.level_id;
                    UpdEntity.catalogue_id = contactRequest.catalogue_id;
                    UpdEntity.marital_status_id = contactRequest.marital_status_id;
                    UpdEntity.marriage_anniversary_date = contactRequest.marriage_anniversary_date;
                    UpdEntity.am_customer_id = contactRequest.am_customer_id;
                    UpdEntity.approval_status_id = contactRequest.approval_status_id;
                    UpdEntity.authorized_to_bid_id = contactRequest.authorized_to_bid_id;
                    UpdEntity.email = contactRequest.email;
                    UpdEntity.phone = contactRequest.phone;
                    UpdEntity.fax = contactRequest.fax;
                    UpdEntity.mobile = contactRequest.mobile;
                    UpdEntity.other_phone = contactRequest.other_phone;
                    UpdEntity.clients_vat_tin_no = contactRequest.clients_vat_tin_no;
                    UpdEntity.aadhar_number = contactRequest.aadhar_number;
                    UpdEntity.pan_no = contactRequest.pan_no;
                    UpdEntity.date_modified = DateTime.Now;
                    UpdEntity.modifiedby_id = contactRequest.modifiedby_id;
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

        [HttpGet]
        [ActionName("ViewContact")]
        //Path: /api/Contact/ViewContact
        public HttpResponseMessage ViewContact(int contact_id)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'GetDropdownMasterData'");
            List<ViewContactData_Result> data = new List<ViewContactData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.contacts.AsNoTracking().FirstOrDefault(m => m.contact_id == contact_id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("Contact doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Contact doesn't exist."); ;
                    }

                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.ViewContactData(contact_id).ToList();
                }

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data[0]);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Pulling the contact is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpGet]
        [ActionName("GetContacts")]
        //Path: /api/Contact/GetContacts
        public HttpResponseMessage GetContacts()
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'GetContacts'");
            List<GetContactsData_Result> data = new List<GetContactsData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.GetContactsData().ToList();
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
    }
}
