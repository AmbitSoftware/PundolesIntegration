//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMSIntegration.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class auction_calendar
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<int> deleted { get; set; }
        public string auction_type_c { get; set; }
        public string auction_title_c { get; set; }
        public Nullable<System.DateTime> start_date_c { get; set; }
        public Nullable<System.DateTime> end_date_c { get; set; }
        public string viewing_c { get; set; }
        public string city_description_c { get; set; }
        public string buyer_commission_in_c { get; set; }
        public string auction_number_c { get; set; }
        public string invoice_description_c { get; set; }
        public string am_customer_id_c { get; set; }
        public string currency_type_c { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> createdby_id { get; set; }
        public Nullable<int> modifiedby_id { get; set; }
    }
}