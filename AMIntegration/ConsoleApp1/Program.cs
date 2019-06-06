using System;
using ConsoleApp1.AMIntegrationService;

namespace ConsoleApp1
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
                
                var svc = new AMIntegration();
                var req = new GetListofAMCustomerRequest();
                req.ReferenceID = "465656";
                req.Channel = "IB";

                var resp = svc.GetListofAMCustomer(req, new DateTime(2001,1,1), new DateTime(2019,10,10));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
