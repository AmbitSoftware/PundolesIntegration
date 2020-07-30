using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSIntegration.Test.CMSModels
{
    public class PullAuctionCalendar
    {

        public string Id { get; set; }


        public string Description { get; set; }


        public string AuctionTitle { get; set; }


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        public string CityDescription { get; set; }

        public string AuctionNumber { get; set; }
    }

    public class PushAuctionCalendar
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "auction_title_c")]
        public string AuctionTitle { get; set; }

        [JsonProperty(PropertyName = "start_date_c")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date_c")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "city_description_c")]
        public string CityDescription { get; set; }

        [JsonProperty(PropertyName = "auction_number_c")]
        public string AuctionNumber { get; set; }
    }

    public class AuctionCalendar
    {

        public string Id { get; set; }


        public string Name { get; set; }


        public DateTime? DateEntered { get; set; }


        public DateTime? DateModified { get; set; }


        public string ModifiedUserId { get; set; }


        public string CreatedBy { get; set; }


        public string Description { get; set; }


        public string Deleted { get; set; }


        public string AssignedUserId { get; set; }


        public string AuctionType { get; set; }


        public string AuctionTitle { get; set; }

        public DateTime? StartDate { get; set; }


        public DateTime? EndDate { get; set; }

        public string Viewing { get; set; }


        public string CityDescription { get; set; }


        public string BuyerCommission { get; set; }


        public string AuctionNumber { get; set; }


        public string InvoiceDescription { get; set; }


        public string AmCustomer { get; set; }


        public string CurrencyType { get; set; }

    }
}
