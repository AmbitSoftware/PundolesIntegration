
using System.Collections.Generic;

namespace AMIntegration
{
    public class GetListofSuiteContactResponse : AMIntegrationRes
    {
        public string ReferenceID { get; set; }
        public List<AMCustomer.AMCustomer> List { get; set; }
    }
}