using System.Collections.Generic;

namespace AMIntegration
{
    public class PushListofSuiteAuctionResponse : AMIntegrationRes
    {
        public List<AMCustomer.AMAuction> List { get; set; }
    }
}