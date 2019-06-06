using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMSIntegration.SuiteAPI
{    
    public static class SuiteWrapper
    {      
        public static string nextURL;
        public static string apiUrl;
        public static string apiUserName;
        public static string apiPassword;
        public static int maxPullResults;
        public static string traceLogFileName = "CMSIntegration_Logs_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".log";
        public static string traceLogPath = string.Empty;

        #region Class Contructor

        static SuiteWrapper()
        {
            nextURL = ConfigurationManager.AppSettings["NextURL"];
            apiUrl = ConfigurationManager.AppSettings["SuiteUrl"];
            apiUserName = ConfigurationManager.AppSettings["SuiteUsername"];
            apiPassword = ConfigurationManager.AppSettings["SuitePassword"];
            maxPullResults = int.Parse(ConfigurationManager.AppSettings["MaxPullResult"]);
            traceLogPath = GetTraceLogPath();
        }

        #endregion


        #region Common Methods


        public static bool ValidateRequest(object obj, out string StrResp)
        {
            bool RetVal = true;
            StrResp = string.Empty;
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, context, results, true);
            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    StrResp = StrResp + validationResult.ErrorMessage + ",";
                }

                StrResp = StrResp.TrimEnd(',');
                RetVal = false;
            }

            return RetVal;
        }

        public static bool WriteTraceLog(string ErrorMsg)
        {
            bool rval = true;
            if (Convert.ToString(ConfigurationManager.AppSettings["IsTraceEnable"]) == "0") return rval;
            try
            {
                File.AppendAllText(traceLogPath, Environment.NewLine + "[" + DateTime.Now + "]" + " " + ErrorMsg);
            }
            catch (Exception)
            {

            }
            return rval;
        }

        //public static string UpdateMasterXML(string methodName, string action, string dateTime)
        //{
        //    XmlDocument xml = new XmlDocument();
        //    xml.Load(System.Web.HttpContext.Current.Server.MapPath("~/") + "SyncMaster.xml");
        //    string getLastSyncDate = string.Empty;
        //    if (action == "Get")
        //    {
        //        getLastSyncDate = Convert.ToString(xml.SelectSingleNode("//" + methodName + "/lastSyncedDate").InnerText);
        //    }
        //    if (action == "Set")
        //    {
        //        if (string.IsNullOrEmpty(dateTime))
        //            xml.SelectSingleNode("//" + methodName + "/lastSyncedDate").InnerText = "";
        //        else
        //            xml.SelectSingleNode("//" + methodName + "/lastSyncedDate").InnerText = dateTime;

        //        xml.Save(System.Web.HttpContext.Current.Server.MapPath("~/") + "SyncMaster.xml");
        //    }
        //    return getLastSyncDate;
        //}

        public static string GetTraceLogPath()
        {
            string Key = "";
            string strTracePath = Convert.ToString(ConfigurationManager.AppSettings["TraceLogFilePath"]);
            //string filePath = (Path.GetDirectoryName(Application.ExecutablePath)) + strTracePath + LogFileName;
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/") + strTracePath + traceLogFileName;
            //string filePath = "C:\\" + LogFileName;
            return Key = filePath;
        }
        public static async Task<string> Login()
        {
            string method = "login";
            var dObj = new
            {
                user_auth = new
                {
                    user_name = apiUserName,
                    password = CreateMD5(apiPassword)
                }
            };
            string jstr = await CallApi(method, dObj).ConfigureAwait(false);
            dynamic res = JsonConvert.DeserializeObject(jstr);
            if (res.id == null)
            {
                throw new Exception(res.name.Value + "-" + res.description.Value + " method:" + method);
            }
            return res.id.Value;
        }

       
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private static async Task<string> CallApi(string method, object dObj)
        {
            var json = JsonConvert.SerializeObject(dObj);
            var values = new Dictionary<string, string>
                {
                   { "method", method },
                   { "input_type", "JSON" },
                   { "response_type", "JSON" },
                   { "rest_data",  json},
                };
            var client = new System.Net.Http.HttpClient();
            var content = new SuiteAPI.MyFormUrlEncodedContent(values);
            try
            {
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error handling result." + ex.ToString());
            }
        }
        public static async Task<ReadEntryListResponse> GetModuleList(ReadEntryListRequest req)
        {
            string method = req.MethodName;
            var dObj = new
            {
                session = req.SessionId,
                module_name = req.ModuleName,
                fields = req.SelectFields.ToArray()
            };
            var jstr = await CallApi(method, dObj).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReadEntryListResponse>(jstr);
        }

        public static async Task<ReadEntryListResponse> GetList(ReadEntryListRequest req)
        {
            string method = req.MethodName;
            var dObj = new
            {
                session = req.SessionId,
                module_name = req.ModuleName,
                query = req.Query,
                order_by = req.OrderBy,
                offset = req.Offset,
                select_fields = req.SelectFields.ToArray(),
                link_name_to_fields_array = req.LinkNameToFieldsArray.ToArray(),
                max_results = req.MaxResults,
                deleted = req.Deleted,
                favorites = req.Favorites
            };
            var jstr = await CallApi(method, dObj).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReadEntryListResponse>(jstr);
        }

        public static async Task<List<T>> GetModuleList<T>(ReadEntryListRequest req)
        {
            var resp = await GetModuleList(req).ConfigureAwait(false);
            var strList = Convert.ToString(resp.EntityList);
            return ToObjects<T>(strList);
        }

        public static async Task<ReadEntryListResponse> GetList<T>(ReadEntryListRequest req)
        {
            var response = await GetList(req).ConfigureAwait(false);
            return response;
        }

        public static List<T> GetListObjectConverted<T>(ReadEntryListResponse response)
        {
            var strList = Convert.ToString(response.EntityList);
            return ToObjects<T>(strList);
        }

        public static List<T> ToObjects<T>(string json)
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
            DeserializerExceptionsContractResolver resolver =
                DeserializerExceptionsContractResolver.Instance;
            resolver.JsonToDeserialize = content;
            settings.ContractResolver = resolver;

            return JsonConvert.DeserializeObject<T>(content, settings);
        }

        public static List<string> GetFieldList(PropertyInfo[] props)
        {
            List<string> fieldList = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                foreach (object attr in prop.GetCustomAttributes(true))
                {
                    fieldList.Add((attr as JsonPropertyAttribute).PropertyName);
                }
            }
            return fieldList;
        }

        public static async Task<AddUpdateMultipleEntriesResponse> AddUpdateMultipleEntries(AddUpdateMultipleEntriesRequest req)
        {
            string method = "set_entries";
            if (req.SelectFields == null || req.SelectFields.Count == 0)
            {
                req.SelectFields = GetAllJsonPropertyNames(req.Entity);
            }

            List<object> list = new List<object>();
            foreach (var data in req.Entity)
            {
                var obje = EntityToNameValueList(Serialize(data, data.GetType()),
                                        req.SelectFields);

                list.Add(obje);
            }
            var dObj = new
            {
                session = req.SessionId,
                module_name = req.ModuleName,
                name_value_list = list
            };
            var jstr = await CallApi(method, dObj).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<AddUpdateMultipleEntriesResponse>(jstr);
        }

        public static async Task<AddUpdateSingleEntryResponse> AddUpdateSingleEntry(AddUpdateSingleEntryRequest req)
        {
            string method = "set_entry";
            if (req.SelectFields == null || req.SelectFields.Count == 0)
            {
                req.SelectFields = GetAllJsonPropertyNames(req.Entity);
            }
            var dObj = new
            {
                session = req.SessionId,
                module_name = req.ModuleName,
                name_value_list = EntityToNameValueList(Serialize(req.Entity, req.Entity.GetType()),
                                        req.SelectFields),
            };
            var jstr = await CallApi(method, dObj).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<AddUpdateSingleEntryResponse>(jstr);
        }

        public static List<string> GetAllJsonPropertyNames(Object modelInfo)
        {
            var list = new List<string>();
            foreach (PropertyInfo item in modelInfo.GetType().GetProperties())
            {
                var jp = item.GetCustomAttributes<JsonPropertyAttribute>(false).FirstOrDefault();
                if (jp != null) list.Add(jp.PropertyName);
            }
            return list;
        }

        public static Dictionary<string, object> EntityToNameValueList(JObject entity, List<string> selectFields)
        {
            var namevalueList = new Dictionary<string, object>();

            bool useSelectedFields = (selectFields != null) && (selectFields.Count > 0);
            var jproperties = entity.Properties().ToList();
            foreach (JProperty jproperty in jproperties)
            {
                string name = jproperty.Name;
                if (useSelectedFields)
                {
                    if (selectFields.All(x => x.ToLower() != name.ToLower()))
                    {
                        continue;
                    }
                }

                object value = jproperty.Value;

                //if (string.Compare("id", name, StringComparison.CurrentCultureIgnoreCase) == 0)
                //{
                //    continue;
                //}

                var namevalueDic = new Dictionary<string, object>();
                namevalueDic.Add("name", name);
                namevalueDic.Add("value", value);

                namevalueList.Add(name, namevalueDic);
            }

            return namevalueList;
        }

        public static JObject Serialize(object obj, Type type)
        {
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "yyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(obj, type, Newtonsoft.Json.Formatting.Indented, jsonSettings);
            return JObject.Parse(json);
        }
        #endregion
    }
}