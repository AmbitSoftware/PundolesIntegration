using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMSIntegration.CMSModels;
using CMSIntegration.EntityModel;
using CMSIntegration.SuiteAPI;
using Newtonsoft.Json;


namespace CMSIntegration.Controller 
{
    public class AuctionController : ApiController
    {

        [HttpPost]
        [ActionName("CreateAuction")]
        //Path: /api/Auction/CreateAuction
        public HttpResponseMessage CreateAuction(PushAuctionCalendar CreateAuctionModelRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'CreateAuction' with request :" + JsonConvert.SerializeObject(CreateAuctionModelRequest));
            PushAuctionResponse pushAuctionResponse = new PushAuctionResponse();

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(CreateAuctionModelRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(CreateAuctionModelRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }

            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    auction_calendar auctionObject = new auction_calendar();
                    auctionObject.name = CreateAuctionModelRequest.Name;
                    auctionObject.description = CreateAuctionModelRequest.Description;
                    auctionObject.auction_type_c = CreateAuctionModelRequest.Auction_Type;
                    auctionObject.auction_title_c = CreateAuctionModelRequest.AuctionTitle;
                    auctionObject.start_date_c = CreateAuctionModelRequest.StartDate;
                    auctionObject.end_date_c = CreateAuctionModelRequest.EndDate;
                    auctionObject.viewing_c = CreateAuctionModelRequest.Viewing;
                    auctionObject.city_description_c = CreateAuctionModelRequest.CityDescription;
                    auctionObject.buyer_commission_in_c = CreateAuctionModelRequest.Buyer_commission;
                    auctionObject.auction_number_c = CreateAuctionModelRequest.AuctionNumber;
                    auctionObject.invoice_description_c = CreateAuctionModelRequest.InvoiceDescription;
                    auctionObject.am_customer_id_c = CreateAuctionModelRequest.AmCustomer;
                    auctionObject.currency_type_c = CreateAuctionModelRequest.CurrencyType;
                    auctionObject.created_date = DateTime.Now;
                    auctionObject.modified_date = DateTime.Now;
                    auctionObject.createdby_id = CreateAuctionModelRequest.createdby_id;
                    auctionObject.modifiedby_id = CreateAuctionModelRequest.createdby_id;
                    context.auction_calendar.Add(auctionObject);
                    context.SaveChanges();

                    pushAuctionResponse.id = auctionObject.id;
                    pushAuctionResponse.status = "Success";

                    SuiteWrapper.WriteTraceLog("auction is successfully created with response :" + JsonConvert.SerializeObject(pushAuctionResponse));
                    return Request.CreateResponse(HttpStatusCode.OK, pushAuctionResponse);
                }
            }
            catch (Exception ex)
            {
                pushAuctionResponse.id = null;
                pushAuctionResponse.status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while creating auction is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(pushAuctionResponse));
            }
        }

        [HttpPost]
        [ActionName("UpdateAuction")]
        //Path: /api/Auction/UpdateAuction
        public HttpResponseMessage UpdateAuction(UpdateAuctionCalendar updateAuctionModelRequest)
        {
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'UpdateAuction' with request :" + JsonConvert.SerializeObject(updateAuctionModelRequest));

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(updateAuctionModelRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(updateAuctionModelRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.auction_calendar.AsNoTracking().FirstOrDefault(m => m.id == updateAuctionModelRequest.Id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("Auction doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Auction doesn't exist."); ;
                    }

                    UpdEntity.name = updateAuctionModelRequest.Name;
                    UpdEntity.description = updateAuctionModelRequest.Description;
                    UpdEntity.auction_type_c = updateAuctionModelRequest.AuctionType;
                    UpdEntity.auction_title_c = updateAuctionModelRequest.AuctionTitle;
                    UpdEntity.start_date_c = updateAuctionModelRequest.StartDate;
                    UpdEntity.end_date_c = updateAuctionModelRequest.EndDate;
                    UpdEntity.viewing_c = updateAuctionModelRequest.Viewing;
                    UpdEntity.city_description_c = updateAuctionModelRequest.CityDescription;
                    UpdEntity.buyer_commission_in_c = updateAuctionModelRequest.BuyerCommission;
                    UpdEntity.auction_number_c = updateAuctionModelRequest.AuctionNumber;
                    UpdEntity.invoice_description_c = updateAuctionModelRequest.InvoiceDescription;
                    UpdEntity.am_customer_id_c = updateAuctionModelRequest.AmCustomer;
                    UpdEntity.currency_type_c = updateAuctionModelRequest.CurrencyType;

                    UpdEntity.modified_date = DateTime.Now;
                    UpdEntity.modifiedby_id = updateAuctionModelRequest.createdby_id;

                    context.Entry(UpdEntity).State = EntityState.Modified;

                    context.SaveChanges();

                    SuiteWrapper.WriteTraceLog("Auction is Successfully updated.");
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                }
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while updating the Auction is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString()); ;
            }
        }

        [ActionName("GetAuction")]
        //Path: /api/Auction/GetAuctions
        public HttpResponseMessage GetAuctions()
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'GetAuction'");
            List<GetAuctionsData_Result> data = new List<GetAuctionsData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.GetAuctionsData().ToList();
                }

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Pulling the auction is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpGet]
        [ActionName("ViewAuction")]
        //Path: /api/Auction/ViewAuction
        public HttpResponseMessage ViewAuction(int auction_id)
        {
            //Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'ViewAuction'");
            List<ViewAuctionData_Result> data = new List<ViewAuctionData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.auction_calendar.AsNoTracking().FirstOrDefault(m => m.id == auction_id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("Auction doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Auction doesn't exist."); ;
                    }

                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.ViewAuctionData(auction_id).ToList();
                }

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data[0]);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception whiile Pulling the user is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
    }
}