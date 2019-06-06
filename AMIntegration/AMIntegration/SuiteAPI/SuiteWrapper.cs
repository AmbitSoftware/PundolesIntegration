using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace AMIntegration
{
    public class SuiteWrapper
    {
        #region Class Memeber data
        static string _apiUrl;
        public string apiUrl
        {
            get
            {
                if (_apiUrl == null) _apiUrl = ConfigurationManager.AppSettings["SuiteUrl"];
                return _apiUrl;
            }
        }

        static string _LogConnectionString;
        string logConnectionString
        {
            get
            {
                if (_LogConnectionString == null)
                {
                    string dbLogConnectionString = Convert.ToString(ConfigurationManager.AppSettings["DBLogConnectionString"]);
                    string logDBPassword = Convert.ToString(ConfigurationManager.AppSettings["LogDBPassword"]);
                    string securityKey = Convert.ToString(ConfigurationManager.AppSettings["SecurityKey"]);
                    string decryptedPassword = Decrypt(securityKey, logDBPassword);
                    _LogConnectionString = string.Format(dbLogConnectionString, decryptedPassword);
                }
                return _LogConnectionString;
            }
        }

        static string _ConnectionString;
        string connectionString
        {
            get
            {
                if (_ConnectionString == null)
                {
                    string dbConnectionString = Convert.ToString(ConfigurationManager.AppSettings["DBConnectionString"]);
                    string dbPassword = Convert.ToString(ConfigurationManager.AppSettings["DBPassword"]);
                    string securityKey = Convert.ToString(ConfigurationManager.AppSettings["SecurityKey"]);
                    string decryptedPassword = Decrypt(securityKey, dbPassword);
                    _ConnectionString = string.Format(dbConnectionString, decryptedPassword);
                }
                return _ConnectionString;
            }
        }

        static string _apiUsername;
        string apiUserName
        {
            get
            {
                if (_apiUsername == null) _apiUsername = ConfigurationManager.AppSettings["SuiteUsername"];
                return _apiUsername;
            }
        }

        static string _apiPassword;
        string apiPassword
        {
            get
            {
                if (_apiPassword == null) _apiPassword = ConfigurationManager.AppSettings["SuitePassword"];
                return _apiPassword;
            }
        }

        public static string Log_Filename = ConfigurationManager.AppSettings["TraceLogFileName"] + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".log";
        public static string logMessageName = ConfigurationManager.AppSettings["Log_Filename"] + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".log";

        public static string traceLogPath = GetTraceLogPath();

        public static string LogMessagePath = GetLogMessagePath();
        #endregion

        #region Common Methods

        private static byte[][] GetHashKeys(string key)
        {
            byte[][] result = new byte[2][];
            Encoding enc = Encoding.UTF8;

            SHA256 sha2 = new SHA256CryptoServiceProvider();

            byte[] rawKey = enc.GetBytes(key);
            byte[] rawIV = enc.GetBytes(key);

            byte[] hashKey = sha2.ComputeHash(rawKey);
            byte[] hashIV = sha2.ComputeHash(rawIV);

            Array.Resize(ref hashIV, 16);

            result[0] = hashKey;
            result[1] = hashIV;

            return result;
        }
        public static string Decrypt(string key, string data)
        {
            string decData = null;
            byte[][] keys = GetHashKeys(key);

            try
            {
                decData = DecryptStringFromBytes_Aes(data, keys[0], keys[1]);
            }
            catch (Exception ex)
            {
                WriteTraceLog("Decrypt","Exception while decrypting password" + ex.ToString());
                throw;
            }

            return decData;
        }
        private static string DecryptStringFromBytes_Aes(string cipherTextString, byte[] Key, byte[] IV)
        {
            byte[] cipherText = Convert.FromBase64String(cipherTextString);

            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt =
                            new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        public async Task<SetRelationShipResponse> DeleteRelationship(SetRelationShipRequest req)
        {
            string method = "set_relationship";
            var set_relationshipDetails = new
            {
                session = req.session,
                module_name = req.module_name,
                module_id = req.module_id,
                link_field_name = req.link_field_name,
                related_ids = req.related_ids.ToArray(),
                name_value_list = req.name_value_list.ToArray(),
                //delete = req.delete,
                delete_array = req.delete_array
            };
            var jstr = await CallApi(method, set_relationshipDetails).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<SetRelationShipResponse>(jstr);
        }
        public static string GetTraceLogPath()
        {
            string Key = "";
            string strTracePath = Convert.ToString(ConfigurationManager.AppSettings["Log_File_Path"]);
            string filePath = strTracePath + Log_Filename;
            return Key = filePath;
        }
        public static string GetLogMessagePath()
        {
            string Key = "";
            string strLogMessagePath = Convert.ToString(ConfigurationManager.AppSettings["Log_MessagePath"]);   
            string filePath = strLogMessagePath + logMessageName;
            return Key = filePath;
        }
        public static bool WriteTraceLog(string functionname, string ErrorMsg)
        {
            bool rval = true;
            if (!Directory.Exists(Convert.ToString(ConfigurationManager.AppSettings["Log_File_Path"])))
                Directory.CreateDirectory(Convert.ToString(ConfigurationManager.AppSettings["Log_File_Path"]));

            if (Convert.ToString(ConfigurationManager.AppSettings["IsTraceEnable"]) == "0") return rval;
            try
            {
                FileStream file = File.Open(traceLogPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter swLogFile = new StreamWriter(file);
                string logMessage = Environment.NewLine + "[" + DateTime.Now + "] " + functionname + " -  " + ErrorMsg;
                swLogFile.WriteLine(logMessage);
                swLogFile.Close();
                swLogFile.Dispose();
                file.Close();
                file.Dispose();

            }
            catch (Exception)
            {

            }
            return rval;
        }
        public static bool WriteLogMessages(string ErrorMsg)
        {
            bool rval = true;
            if (Convert.ToString(ConfigurationManager.AppSettings["IsLogMessageEnable"]) == "0") return rval;
            try
            {
                File.AppendAllText(LogMessagePath, Environment.NewLine + "[" + DateTime.Now + "]" + " " + ErrorMsg);
            }
            catch (Exception ex)
            {
                SuiteWrapper.WriteTraceLog("WriteLogMessages", "Error Message : " + ex.Message);
            }
            return rval;
        }
        public async Task<string> Login()
        {
            string method = "login";
            var loginDetails = new
            {
                user_auth = new
                {
                    user_name = apiUserName,
                    //password = CreateMD5(apiPassword)
                    password = apiPassword
                }
            };
            string jstr = await CallApi(method, loginDetails).ConfigureAwait(false);
            dynamic res = JsonConvert.DeserializeObject(jstr);
            if (res.id == null)
            {
                throw new Exception(res.name.Value + "-" + res.description.Value + " method:" + method);
            }
            return res.id.Value;
        }
        public bool CreateDatabaseLog(string ReferenceID, string Channel, string Status, string serviceName)
        {
            bool rval = true;
            SqlConnection cn = new SqlConnection(logConnectionString);
            try
            {
                cn.Open();
                string iQuery = "INSERT INTO WeCare_Logs(ReferenceID,Channel,Date,Status,LogFileName,ServiceName)"
                                 + " VALUES('" + ReferenceID + "', '" + Channel + "', GETDATE(), '" + Status + "', '" + Log_Filename + "','" + serviceName + "')";
                SqlCommand cmd = new SqlCommand(iQuery, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                rval = false;
                WriteTraceLog("CreateDatabaseLog","Database communication/connection exception-" + ex.ToString());
            }
            return rval;
        }
        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection(connectionString);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cn.Close();
            }
            catch (Exception ex)
            {
                dt = null;
                WriteTraceLog("GetData","Database communication/connection exception-" + ex.ToString());
            }
            return dt;
        }

        public void UpdateData(string query)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                WriteTraceLog("UpdateData","Database communication/connection exception-" + ex.ToString());
            }
        }
        public async Task<bool> Logout(string sessionId)
        {
            try
            {
                string method = "logout";
                var logoutDetails = new
                {
                    session = sessionId
                };
                string jstr = await CallApi(method, logoutDetails);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void WriteApplicationLog(string Message, string actionName, string referenceNo)
        {
            var appLog = new EventLog("Application");
            appLog.Source = "WeCareService";
            appLog.WriteEntry("RRN:" + referenceNo + ", ActionName:" + actionName + ", Exception is: " + Message);
        }

        public async Task<SuiteReadEntryListResponse> GetList(SuiteReadEntryListRequest req)
        {
            string method = "get_entry_list";
            var get_entryDetails = new
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
            var jstr = await CallApi(method, get_entryDetails).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<SuiteReadEntryListResponse>(jstr);
        }

        public async Task<List<T>> GetList<T>(SuiteReadEntryListRequest req) where T :
            SugarCrmModels.EntityBase
        {
            var resp = await GetList(req).ConfigureAwait(false);
            var strList = Convert.ToString(resp.EntityList);
            return ToObjects<T>(strList);
        }

        public static List<T> ToObjects<T>(string json)
     where T : SugarCrmModels.EntityBase
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
        private async Task<string> CallApi(string method, object dObj)
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
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error handling result." + ex.ToString());
            }
            return null;
        }

        private async Task<string> CallApi1(string method, object dObj)
        {
            var json = "{\"session\":\"sci4ejgpsoqr7m2bt6fchgbk41\",\"module_name\":\"Users\",\"query\":\"\",\"order_by\":\"\",\"offset\":0,\"select_fields\":[\"id\",\"user_name\",\"staff_id_c\",\"user_group_c\",\"last_login_date_c\",\"email1\",\"date_entered\",\"date_modified\",\"status\"],\"link_name_to_fields_array\":[[\"Key\":\"name\",\"Value\":\"email_addresses\"],[\"Key\":\"value\",\"Value\":[\"id\",\"email_address\"]]],\"max_results\":10,\"deleted\":false,\"favorites\":false}";

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
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error handling result." + ex.ToString());
            }
            return null;
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

        public bool ValidateRequest(object instance, out string StrResp)
        {
            SuiteWrapper.WriteTraceLog("ValidateRequest","Validate Request Start Date:" + DateTime.Now);
            bool RetVal = true;
            StrResp = string.Empty;
            var context = new ValidationContext(instance, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(instance, context, results, true);
            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    StrResp = StrResp + validationResult.ErrorMessage + ",";
                }

                StrResp = StrResp.TrimEnd(',');
                RetVal = false;
            }
            SuiteWrapper.WriteTraceLog("ValidateRequest","Validate Request End Date:" + DateTime.Now);
            return RetVal;
        }

        #region Create

        public async Task<InsertResponse> Insert(InsertRequest req)
        {
            string method = "set_entry";
            if (req.SelectFields == null || req.SelectFields.Count == 0)
            {
                req.SelectFields = GetAllJsonPropertyNames(req.Entity);
            }
            var set_entryDetails = new
            {
                session = req.SessionId,
                module_name = req.ModuleName,
                name_value_list = EntityToNameValueList(Serialize(req.Entity, req.Entity.GetType()),
                                        req.SelectFields),
            };
            var jstr = await CallApi(method, set_entryDetails).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<InsertResponse>(jstr);
        }

        public async Task<ReadEntryListResponse> GetModuleFields(SuiteReadEntryListRequest req)
        {
            string method = "get_module_fields";
            var get_module_fieldsDetails = new
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
            var jstr = await CallApi(method, get_module_fieldsDetails).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReadEntryListResponse>(jstr);
        }

        public async Task<List<T>> GetModuleFields<T>(SuiteReadEntryListRequest req) where T :
            SugarCrmModels.EntityBase
        {
            var resp = await GetModuleFields(req).ConfigureAwait(false);
            var strList = Convert.ToString(resp.EntityList);
            return ToObjects<T>(strList);
        }

        public async Task<SetRelationShipResponse> SetRelationship(SetRelationShipRequest req)
        {
            string method = "set_relationship";
            var set_relationshipDetails = new
            {
                session = req.session,
                module_name = req.module_name,
                module_id = req.module_id,
                link_field_name = req.link_field_name,
                related_ids = req.related_ids.ToArray(),
                name_value_list = req.name_value_list.ToArray(),
                delete = req.delete,
            };
            var jstr = await CallApi(method, set_relationshipDetails).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<SetRelationShipResponse>(jstr);

        }

       
        public async Task<ReadEntryListResponse> GetRelationship<T>(GetRelationshipsRequest req) where T : SugarCrmModels.EntityBase
        {
            string method = "get_relationships";
            var get_relationshipsDetails = new
            {
                session = req.session,
                module_name = req.module_name,
                module_id = req.module_id,
                link_field_name = req.link_field_name,
                related_module_query = req.related_module_query,
                related_fields = req.related_fields.ToArray(),
                related_module_link_name_to_fields_array = req.related_module_link_name_to_fields_array.ToArray(),
                deleted = req.deleted,
                order_by = req.order_by,
                offset = req.offset,
                limit = req.limit
            };
            var jstr = await CallApi(method, get_relationshipsDetails).ConfigureAwait(false);
            var resp = JsonConvert.DeserializeObject<ReadEntryListResponse>(jstr);
            return resp;
        }

       

        public async Task<InsertResponse> Update(InsertRequest req)
        {
            string method = "set_entry";
            if (req.SelectFields == null || req.SelectFields.Count == 0)
            {
                req.SelectFields = GetAllJsonPropertyNames(req.Entity);
            }
            var set_entryDetails = new
            {
                session = req.SessionId,
                module_name = req.ModuleName,
                name_value_list = EntityToNameValueList(Serialize(req.Entity, req.Entity.GetType()),
                                        req.SelectFields),
            };
            var jstr = await CallApi(method, set_entryDetails).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<InsertResponse>(jstr);
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
            string json = JsonConvert.SerializeObject(obj, type, Formatting.Indented, jsonSettings);
            return JObject.Parse(json);
        }

        #endregion

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

        #endregion
    }
}