using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CMSIntegration.EntityModel;

namespace CMSIntegration.CMSModels
{
    public class UserModel
    {
        public user GetUser(string userName, string password)
        {
            using (PundolesEntities context = new PundolesEntities())
            {
                context.Configuration.ProxyCreationEnabled = true;
                user userManagement = context.users.FirstOrDefault(u => u.user_name == userName && u.user_hash == password);
                return userManagement;
            }
        }
    }

    public class CreateUserModel
    {
        [Required(ErrorMessage = "user_name field is required")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "user_hash or password field is required")]
        public string user_hash { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_home { get; set; }
        public string phone_mobile { get; set; }
        public string department { get; set; }
        public string primary_email { get; set; }
        public string alternate_email { get; set; }
        public Nullable<int> report_to_id { get; set; }

        [Required(ErrorMessage = "user_type field is required")]
        public string user_type { get; set; }

        [Required(ErrorMessage = "user_status field is required")]
        public string user_status { get; set; }
        public Nullable<int> createdby_id { get; set; }
    }

    public class UpdateUserModel
    {
        [Required(ErrorMessage = "id field is required")]
        public int id { get; set; }

        [Required(ErrorMessage = "user_name field is required")]
        public string user_name { get; set; }
        public string user_hash { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_home { get; set; }
        public string phone_mobile { get; set; }
        public string department { get; set; }
        public string primary_email { get; set; }
        public string alternate_email { get; set; }
        public Nullable<int> report_to_id { get; set; }

        [Required(ErrorMessage = "user_type field is required")]
        public string user_type { get; set; }

        [Required(ErrorMessage = "user_status field is required")]
        public string user_status { get; set; }
        public Nullable<int> modifiedby_id { get; set; }
    }

    public class PushUserResponse
    {
        public Nullable<int> id { get; set; }
        public string status { get; set; }
    }
}