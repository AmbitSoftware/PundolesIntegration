//using Cryptography;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Protocols;
using AMIntegration.Test.AMIntegrationService;
using System.DirectoryServices.AccountManagement;




namespace AMIntegration.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestATMCDM();

        }
        
        private static void TestATMCDM()
        {
            try
            {
                var svc = new AMIntegrationService.AMIntegration();
                var req = new GetListofAMCustomerRequest();
                req.ReferenceID = "465656";
                req.Channel = "IB";                
                var resp = svc.GetListofAMCustomer(req);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}