using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSIntegration.Test.CMSModels
{
    public class ContactModel
    {
        public int contact_id { get; set; }

        public string salutation { get; set; }

        public string name { get; set; }

        public string contact_type { get; set; }

        public string client_number { get; set; }

        public string interest_id { get; set; }

        public string category_id { get; set; }

        public string customer_category_id { get; set; }

        public string level_id { get; set; }

        public string catalogue_id { get; set; }

        public string marital_status_id { get; set; }

        public Nullable<System.DateTime> marriage_anniversary_date { get; set; }

        public string am_customer_id { get; set; }

        public string approval_status_id { get; set; }

        public string authorized_to_bid_id { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string fax { get; set; }

        public string mobile { get; set; }

        public string other_phone { get; set; }

        public string clients_vat_tin_no { get; set; }

        public string aadhar_number { get; set; }

        public string pan_no { get; set; }

        public Nullable<System.DateTime> date_created { get; set; }

        public Nullable<System.DateTime> date_modified { get; set; }
    }
}
