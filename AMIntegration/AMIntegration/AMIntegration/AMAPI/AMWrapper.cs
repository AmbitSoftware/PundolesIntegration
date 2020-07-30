using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AMIntegration
{
    public class AMWrapper
    {
        #region Class Memeber data
        static string _UATapiUrl;
      

        public string apiUrl
        {
            get
            {
                if (_UATapiUrl == null) _UATapiUrl = ConfigurationManager.AppSettings["AMUrl"];
                return _UATapiUrl;
            }
        }


        static string _apiAccessToken;
        public string apiAccessToken
        {
            get
            {
                if (_apiAccessToken == null) _apiAccessToken = ConfigurationManager.AppSettings["AccessToken"];
                return _apiAccessToken;
            }
        }

        public static List<T> ToObjects<T>(string json)
    where T : AMCustomer.EntityBase
        {
            var data = (System.Collections.IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(T)));
            var jarr = JArray.Parse(json);
            foreach (var jobject in jarr.Children<JObject>())
            {
                object tempObject = Deserialize<T>(jobject.ToString());
                data.Add(tempObject);
            }

            return data as List<T>;
        }

        public static T Deserialize<T>(string content)
        {
            var settings = new JsonSerializerSettings();
            DeserializerExceptionsContractResolver resolver = DeserializerExceptionsContractResolver.Instance;
            resolver.JsonToDeserialize = content;
            settings.ContractResolver = resolver;

            return JsonConvert.DeserializeObject<T>(content, settings);
        }

        public async Task<string> GetAMList<T>(ReadEntryListRequest req, string apiEndpoint) where T : AMCustomer.EntityBase
        {
            var resp = await GetAMList(req, apiEndpoint).ConfigureAwait(false);
            var strList = Convert.ToString(resp);
            return strList;
        }

        public async Task<string> GetAMList(ReadEntryListRequest req, string apiEndpoint)
        {
            string method = "GET";
            var dObj = new
            {
                Authorization = $"Bearer {req.AccessToken}",
                module_name = req.ModuleName
            };

            var jstr = await CallApi(method, dObj, apiEndpoint).ConfigureAwait(false);
            return jstr;
        }

        private async Task<string> CallApi(string method, object dObj, string APIURL)
        {
            var json = JsonConvert.SerializeObject(dObj);
            var values = new Dictionary<string, string>
                {
                   { "method", method },
                   { "input_type", "JSON" },
                   { "response_type", "JSON" },
                   { "rest_data",  json},
                };
            var client = new HttpClient();          
            client.DefaultRequestHeaders.Add("Authorization",$"Bearer {apiAccessToken}"); 

            try
            {
                var response = await client.GetAsync(APIURL).ConfigureAwait(false);
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error handling result." + ex.ToString());
            }
        }

        public async Task<string> PostDataToAM(Dictionary<string, string> PostData, string URL) 
        {
            var resp = await PostDatatoAM(PostData, URL).ConfigureAwait(false);          
            return resp;
        }

        public async Task<string> PostDatatoAM(Dictionary<string, string> PostData, string URL)
        {
           
            var jstr = await CallApiForPOST( PostData, URL).ConfigureAwait(false);
            return jstr;            
        }

        private async Task<string> CallApiForPOST(object PostData, string URL)
        {
            var jsondata = JsonConvert.SerializeObject(PostData).ToString();

            var values = new Dictionary<string, string>
                {             
                   { "rest_data",  jsondata},
                };

            var client = new HttpClient();         
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiAccessToken}");         
            var content = new StringContent(jsondata.ToString(), Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(URL, content).ConfigureAwait(false);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error handling result." + ex.ToString());
            }           
        }
        #endregion
    }
}