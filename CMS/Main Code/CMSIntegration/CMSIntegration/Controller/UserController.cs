using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMSIntegration.CMSModels;
using CMSIntegration.EntityModel;
using CMSIntegration.SuiteAPI;
using Newtonsoft.Json;

namespace CMSIntegration.Controller
{
    [Authorize]
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        [HttpPost]
        [ActionName("CreateUser")]
        //Path: /api/User/CreateUser
        public HttpResponseMessage CreateUser(CreateUserModel CreateUserModelRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'CreateUser' with request :" + JsonConvert.SerializeObject(CreateUserModelRequest));
            PushUserResponse pushUserResponse = new PushUserResponse();

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(CreateUserModelRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(CreateUserModelRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }

            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    user userObject = new user();
                    userObject.user_name = CreateUserModelRequest.user_name;
                    userObject.user_hash = SuiteWrapper.CreateMD5(CreateUserModelRequest.user_hash);
                    userObject.first_name = CreateUserModelRequest.first_name;
                    userObject.last_name = CreateUserModelRequest.last_name;
                    userObject.phone_home = CreateUserModelRequest.phone_home;
                    userObject.phone_mobile = CreateUserModelRequest.phone_mobile;
                    userObject.department = CreateUserModelRequest.department;
                    userObject.report_to_id = CreateUserModelRequest.report_to_id;
                    userObject.primary_email = CreateUserModelRequest.primary_email;
                    userObject.alternate_email = CreateUserModelRequest.alternate_email;
                    userObject.user_type = CreateUserModelRequest.user_type;
                    userObject.user_status = CreateUserModelRequest.user_status;
                    userObject.created_date = DateTime.Now;
                    userObject.modified_date = DateTime.Now;
                    userObject.createdby_id = CreateUserModelRequest.createdby_id;
                    userObject.modifiedby_id = CreateUserModelRequest.createdby_id;
                    context.users.Add(userObject);

                    context.SaveChanges();

                    pushUserResponse.id = userObject.id;
                    pushUserResponse.status = "Success";

                    SuiteWrapper.WriteTraceLog("user is successfully created with response :" + JsonConvert.SerializeObject(pushUserResponse));
                    return Request.CreateResponse(HttpStatusCode.OK, pushUserResponse);
                }
            }
            catch (Exception ex)
            {
                pushUserResponse.id = null;
                pushUserResponse.status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while creating user is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(pushUserResponse));
            }
        }

        [HttpPost]
        [ActionName("UpdateUser")]
        //Path: /api/User/UpdateUser
        public HttpResponseMessage UpdateUser(UpdateUserModel updateUserModelRequest)
        {
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'UpdateUser' with request :" + JsonConvert.SerializeObject(updateUserModelRequest));

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(updateUserModelRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(updateUserModelRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }

            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.users.AsNoTracking().FirstOrDefault(m => m.id == updateUserModelRequest.id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("User doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "User doesn't exist."); ;
                    }

                    UpdEntity.user_name = updateUserModelRequest.user_name;
                    if (!string.IsNullOrEmpty(updateUserModelRequest.user_hash))
                        UpdEntity.user_hash = SuiteWrapper.CreateMD5(updateUserModelRequest.user_hash);

                    UpdEntity.first_name = updateUserModelRequest.first_name;
                    UpdEntity.last_name = updateUserModelRequest.last_name;
                    UpdEntity.phone_home = updateUserModelRequest.phone_home;
                    UpdEntity.phone_mobile = updateUserModelRequest.phone_mobile;
                    UpdEntity.department = updateUserModelRequest.department;
                    UpdEntity.report_to_id = updateUserModelRequest.report_to_id;
                    UpdEntity.primary_email = updateUserModelRequest.primary_email;
                    UpdEntity.alternate_email = updateUserModelRequest.alternate_email;
                    UpdEntity.user_type = updateUserModelRequest.user_type;
                    UpdEntity.user_status = updateUserModelRequest.user_status;
                    UpdEntity.modified_date = DateTime.Now;
                    UpdEntity.modifiedby_id = updateUserModelRequest.modifiedby_id;

                    context.Entry(UpdEntity).State = EntityState.Modified;

                    context.SaveChanges();

                    SuiteWrapper.WriteTraceLog("User is Successfully updated.");
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                }
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while updating the contact is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString()); ;
            }
        }

        [HttpGet]
        [ActionName("GetUsers")]
        //Path: /api/User/GetUsers
        public HttpResponseMessage GetUsers()
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'GetUsers'");
            List<GetUsersData_Result> data = new List<GetUsersData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.GetUsersData().ToList();
                }

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Pulling the user is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpGet]
        [ActionName("ViewUser")]
        //Path: /api/User/ViewUser
        public HttpResponseMessage ViewUser(int user_id)
        {
            //Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'ViewUser'");
            List<ViewUserData_Result> data = new List<ViewUserData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.users.AsNoTracking().FirstOrDefault(m => m.id == user_id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("User doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "User doesn't exist."); ;
                    }

                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.ViewUserData(user_id).ToList();
                }

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data[0]);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception whiile Pulling the user is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

    }
}
