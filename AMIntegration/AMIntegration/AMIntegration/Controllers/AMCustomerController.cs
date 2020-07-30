using System;
using System.Collections.Generic;
using System.Web.Http;
using AMIntegration.Modules.Customer;
using AMIntegration.Modules.Auction;
using AMIntegration.Modules.Registration;
using AMIntegration.Common;

namespace AMIntegration.Controllers
{
    public class AMCustomerController : ApiController
    {
        string suitesessionId = string.Empty;
        CommonClass common = new CommonClass();

        public bool GetCustomer(string logId, DateTime startDate, DateTime endDate)
        {
            var customer = new GetCustomer();
            var resp = customer.GetListofAMCustomer(startDate, endDate, logId);
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Customer sync ended", Name = "GetCustomer", Entity_c = "Customer" }, logId);
            SuiteWrapper.WriteTraceLog("GetCustomer", "---------------- CUSTOMER SYNC END ------------------------- ");
            if (resp.StatusCode == 5)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public List<AMCustomer.AMAuction> PushAuction(string logId, string auctionId)
        {
            var auction = new PushAuction();
            var resp = auction.PushListofAMAuction(auctionId, logId);
           
            if (resp.StatusCode == 5)
            {
                return resp.List;
            }

            return null;

        }

        public bool GetRegistration(string logId, DateTime startDate, DateTime endDate)
        {
            var registration = new GetRegistration();
            var resp =  registration.GetListofAMRegistration(logId);
            
                 var respPast = registration.GetListofAMRegistrationPast(startDate, endDate, logId);
            SuiteWrapper.WriteTraceLog("GetRegistration", "---------------- REGISTRATION SYNC END ------------------------- ");
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Registration sync end", Name = "GetRegistration", Entity_c = "Registration" }, logId);

            if (resp.StatusCode==5 || respPast.StatusCode==5)
            {
                return true;
            }
            return false;
        } 
    }
}
