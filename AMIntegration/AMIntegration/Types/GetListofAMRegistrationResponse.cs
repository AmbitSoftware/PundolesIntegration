
using System.Collections.Generic;

namespace AMIntegration
{
    public class GetListofAMRegistrationResponse : AMIntegrationRes
    {
        public string ReferenceID { get; set; }
        public List<AMCustomer.AMRegistration> List { get; set; }
    }
}