
using System.Collections.Generic;
using AMIntegration.AMCustomer;

namespace AMIntegration
{
    public class GetListofAMCustomerResponse : AMIntegrationRes
    {
        
        public List<AMCustomer.AMCustomer> List { get; set; }
    }
}