using System.Collections.Generic;

namespace AMIntegration
{
    public class GetListofAMAuctionResponse : AMIntegrationRes
    {
        public string ReferenceID { get; set; }
        public List<AMCustomer.AMAuction> List { get; set; }
    }
}