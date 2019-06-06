using CMSIntegration.CMSModels;
using CMSIntegration.EntityModel;
using CMSIntegration.SuiteAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMSIntegration.Controller
{
    [Authorize]
    [RoutePrefix("Artist")]
    public class ArtistController : ApiController
    {
        [HttpPost]
        [ActionName("CreateArtist")]
        //Path: /api/Artist/CreateArtist
        public HttpResponseMessage CreateArtist(CreateArtist createArtistRequest)
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'CreateArtist' with request :" + JsonConvert.SerializeObject(createArtistRequest));
            CreateArtistResponse createArtistResponse = new CreateArtistResponse();

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(createArtistRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(createArtistRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }

            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    artist artistObject = new artist();
                    artistObject.name = createArtistRequest.name;
                    artistObject.description = createArtistRequest.description;
                    artistObject.year_of_birth_c = createArtistRequest.year_of_birth_c;
                    artistObject.year_of_death_c = createArtistRequest.year_of_death_c;
                    artistObject.status = createArtistRequest.status;
                    artistObject.created_date = DateTime.Now;
                    artistObject.modified_date = DateTime.Now;
                    artistObject.createdby_id = createArtistRequest.createdby_id;
                    artistObject.modifiedby_id = createArtistRequest.createdby_id;                
                    context.artists.Add(artistObject);

                    context.SaveChanges();

                    createArtistResponse.id = artistObject.id;
                    createArtistResponse.status = "Success";

                    SuiteWrapper.WriteTraceLog("user is successfully created with response :" + JsonConvert.SerializeObject(createArtistResponse));
                    return Request.CreateResponse(HttpStatusCode.OK, createArtistResponse);
                }
            }
            catch (Exception ex)
            {
                createArtistResponse.id = null;
                createArtistResponse.status = ex.Message.ToString();

                SuiteWrapper.WriteTraceLog("Exception while creating artist is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(createArtistResponse));
            }
        }

        [HttpPost]
        [ActionName("UpdateArtist")]
        //Path: /api/Artist/UpdateArtist
        public HttpResponseMessage UpdateArtist(UpdateArtist updateArtistRequest)
        {
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'UpdateArtist' with request :" + JsonConvert.SerializeObject(updateArtistRequest));

            string outputMessage = string.Empty;
            if (!SuiteWrapper.ValidateRequest(updateArtistRequest, out outputMessage))
            {
                //Trace Log
                SuiteWrapper.WriteTraceLog("Exception while validating request for " + JsonConvert.SerializeObject(updateArtistRequest) + " is : " + outputMessage);
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, outputMessage); ;
            }

            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    var UpdEntity = context.artists.AsNoTracking().FirstOrDefault(m => m.id == updateArtistRequest.id);
                    if (UpdEntity == null)
                    {
                        SuiteWrapper.WriteTraceLog("Artist doesn't exist.");
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Artist doesn't exist."); ;
                    }


                    UpdEntity.name = updateArtistRequest.name;
                    UpdEntity.description = updateArtistRequest.description;
                    UpdEntity.year_of_birth_c = updateArtistRequest.year_of_birth_c;
                    UpdEntity.year_of_death_c = updateArtistRequest.year_of_death_c;
                    UpdEntity.status = updateArtistRequest.status;
                    UpdEntity.modified_date = DateTime.Now;
                    UpdEntity.modifiedby_id = updateArtistRequest.modifiedby_id;

                    context.Entry(UpdEntity).State = EntityState.Modified;

                    context.SaveChanges();

                    SuiteWrapper.WriteTraceLog("Artist is Successfully updated.");
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                }
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while updating the artist is : " + ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString()); ;
            }
        }

        [HttpGet]
        [ActionName("GetArtists")]
        //Path: /api/Artist/GetArtists
        public HttpResponseMessage GetArtists()
        {
            // Trace Log
            File.AppendAllText(SuiteWrapper.traceLogPath, Environment.NewLine + Environment.NewLine);
            SuiteWrapper.WriteTraceLog("Called 'GetArtists'");
            List<GetArtistsData_Result> data = new List<GetArtistsData_Result>();
            try
            {
                using (PundolesEntities context = new PundolesEntities())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    data = context.GetArtistsData().ToList();
                }

                SuiteWrapper.WriteTraceLog("Successfully called with response:" + JsonConvert.SerializeObject(data));
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("Exception while Pulling the artists is " + ex.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
    }
}
