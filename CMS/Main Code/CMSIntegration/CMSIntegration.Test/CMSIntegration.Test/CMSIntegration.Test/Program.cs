using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CMSIntegration.Test.CMSModels;


namespace CMSIntegration.Test
{
    class Program
    {
        static HttpClient client = new HttpClient();

        public static void Main()
        {
            //Program objpro = new Program();
            //objpro.PostEvent_data();
            client.BaseAddress = new Uri("http://localhost:49610/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var url = GetModuleDetails("test").GetAwaiter().GetResult();

            // Push Artists
            //var url = PushArtists().GetAwaiter().GetResult();

            // Pull Master Data
            //var url = PullMasterData().GetAwaiter().GetResult();

            var url = UpdateItem().GetAwaiter().GetResult();

            //var PushArtists1 = PushArtists().GetAwaiter().GetResult();

            //var PushAuctions1 = PushAuctions().GetAwaiter().GetResult();

            //var PushSymbols1 = PushSymbols().GetAwaiter().GetResult();

            //var PushLocations1 = PushLocations().GetAwaiter().GetResult();

            //var PushCategories1 = PushCategories().GetAwaiter().GetResult();

            //var PullMasterData1 = PullMasterData().GetAwaiter().GetResult();

            //var UploadLotNumber1 = UploadLotNumber().GetAwaiter().GetResult();

            //var MoveItem1 = MoveItem().GetAwaiter().GetResult();

            //var MergeItem1 = MergeItem().GetAwaiter().GetResult();

            //var NewItem1 = NewItem().GetAwaiter().GetResult();

            //var DeleteItem1 = DeleteItem().GetAwaiter().GetResult();

            //var CreateContact1 = CreateContact().GetAwaiter().GetResult();

            //var PullContacts1 = PullContacts().GetAwaiter().GetResult();

            //var GetDropdownMasterData1 = GetDropdownMasterData().GetAwaiter().GetResult();

            //var UpdateItem1 = UpdateItem().GetAwaiter().GetResult();

            //var UpdateContact1 = UpdateContact().GetAwaiter().GetResult();

            //var ViewContact1 = ViewContact().GetAwaiter().GetResult();

            //var Authentication1 = Authentication().GetAwaiter().GetResult();

            //var pass = CreateMD5("admin");
            //var pass1 = CreateMD5("pavan@1234");
            // var pass2 = CreateMD5("abhijit@1234");
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

        public static async Task<string> Authentication()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var result = client.UploadString("http://MYAPP/token", "grant_type=password&username=ankit&password=admin");


            // Serialize our concrete class into a JSON String
            // var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(obj));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            // var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await client. ("token", httpContent);
            //var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> ViewContact()
        {

            HttpResponseMessage response = await client.GetAsync(
               "api/Contact/ViewContact?contact_id=3");
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> DeleteItem()
        {

            HttpResponseMessage response = await client.GetAsync(
               "api/Item/DeleteItem?itemID=2f5737a0-e729-07ce-25ea-5cdbbd972109");
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> UpdateContact()
        {

            var obj = new ContactModel();
            foreach (var item in obj.GetType().GetProperties())
            {
                dynamic value = null;
                if (item.PropertyType == typeof(DateTime))
                {
                    value = DateTime.Today;
                }
                else if (item.PropertyType == typeof(string))
                {
                    value = item.Name + "_test";
                }
                else if (item.PropertyType == typeof(decimal))
                {
                    value = Convert.ToDecimal("134.00");
                }
                else if (item.PropertyType == typeof(int) || item.PropertyType == typeof(Nullable<int>))
                {
                    value = 1;
                }
                else if (item.PropertyType == typeof(bool))
                {
                    value = true;
                }
                item.SetValue(obj, value);
            }

            obj.contact_id = 3;

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(obj));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Contact/UpdateContact", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }



        public static async Task<string> UpdateItem()
        {

            var obj = new UpdateItem();
            //foreach (var item in obj.GetType().GetProperties())
            //{
            //    dynamic value = null;
            //    if (item.PropertyType == typeof(DateTime))
            //    {
            //        value = DateTime.Today;
            //    }
            //    else if (item.PropertyType == typeof(string))
            //    {
            //        value = item.Name + "_test";
            //    }
            //    else if (item.PropertyType == typeof(decimal))
            //    {
            //        value = Convert.ToDecimal("134.00");
            //    }
            //    else if (item.PropertyType == typeof(int) || item.PropertyType == typeof(Nullable<int>))
            //    {
            //        value = 1;
            //    }
            //    else if (item.PropertyType == typeof(bool))
            //    {
            //        value = true;
            //    }
            //    item.SetValue(obj, value);
            //}

            obj.ItemId = "abee79d7-9726-a959-b01e-5d1f54fe80a5";
            obj.HighEstimate = Convert.ToDecimal("56.54");
            obj.LowEstimate = Convert.ToDecimal("22");
            obj.CommisionRate = Convert.ToInt32("1");
            obj.Reserve = Convert.ToDecimal("2");

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(obj));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Item/UpdateItem", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
        public static async Task<string> GetDropdownMasterData()
        {

            HttpResponseMessage response = await client.GetAsync(
               "api/SyncMasters/GetDropdownMasterData");
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }


        public static async Task<string> PullContacts()
        {

            HttpResponseMessage response = await client.GetAsync(
               "api/Contact/PullContacts");
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> CreateContact()
        {

            var obj = new ContactModel();
            foreach (var item in obj.GetType().GetProperties())
            {
                dynamic value = null;
                if (item.PropertyType == typeof(DateTime))
                {
                    value = DateTime.Today;
                }
                else if (item.PropertyType == typeof(string))
                {
                    value = item.Name + "_test";
                }
                else if (item.PropertyType == typeof(decimal))
                {
                    value = Convert.ToDecimal("134.00");
                }
                else if (item.PropertyType == typeof(int) || item.PropertyType == typeof(Nullable<int>))
                {
                    value = 1;
                }
                else if (item.PropertyType == typeof(bool))
                {
                    value = true;
                }
                item.SetValue(obj, value);
            }

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(obj));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Contact/CreateContact", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> NewItem()
        {

            CMSModels.NewItem artist = new CMSModels.NewItem();
            artist.ItemNumber = "Item 123456";
            artist.ContactID = "79d9c";
            artist.CategoryID = "1d8fa0ad-7a94-58d4-8776-5cdd0705ad57";

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(artist));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Item/NewItem", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> MergeItem()
        {

            CMSModels.MergeItem artist = new CMSModels.MergeItem();
            artist.Id = "2f5737a0-e729-07ce-25ea-5cdbbd972109";
            artist.Symbol = "6efa081d-d3a2-3caf-85aa-5ce669d878f3";

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(artist));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Item/MergeItem", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> MoveItem()
        {

            CMSModels.MoveItem artist = new CMSModels.MoveItem();
            artist.Id = "2f5737a0-e729-07ce-25ea-5cdbbd972109";
            artist.CategoryID = "1d8fa0ad-7a94-58d4-8776-5cdd0705ad98";
            artist.ItemNumber = "checked";





            //artist = new CMSModels.Artist();
            //artist.id = null;
            //artist.Name = "Mahesh";
            //artist.DateEntered = DateTime.Now;
            //artist.DateModified = DateTime.Now;
            //artist.Description = "Mahesh Description";

            //listArtists.Add(artist);

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(artist));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Item/MoveItem", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
        public static async Task<string> UploadLotNumber()
        {
            List<CMSModels.UploadLotNumber> listArtists = new List<CMSModels.UploadLotNumber>();

            CMSModels.UploadLotNumber artist = new CMSModels.UploadLotNumber();
            artist.Id = "2f5737a0-e729-07ce-25ea-5cdbbd972109";
            artist.LotNumber = "645465";

            listArtists.Add(artist);

            artist = new CMSModels.UploadLotNumber();
            artist.Id = "6efa081d-d3a2-3caf-85aa-5ce669d878f3";
            artist.LotNumber = "11111";

            listArtists.Add(artist);

            //artist = new CMSModels.Artist();
            //artist.id = null;
            //artist.Name = "Mahesh";
            //artist.DateEntered = DateTime.Now;
            //artist.DateModified = DateTime.Now;
            //artist.Description = "Mahesh Description";

            //listArtists.Add(artist);

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(listArtists));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Item/UploadLotNumber", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> PushArtists()
        {
            List<CMSModels.PushArtist> listArtists = new List<CMSModels.PushArtist>();

            CMSModels.PushArtist artist = new CMSModels.PushArtist();
            artist.Id = "28a0037d-cb79-6de7-dff3-5ce78a90516b";
            artist.Name = "Artist 1 Test";
            artist.Description = "Description 1";
            artist.YearOfBirth = "2010";
            artist.YearOfDeth = "2019";

            listArtists.Add(artist);

            artist = new CMSModels.PushArtist();
            artist.Id = "";
            artist.Name = "Artist 2 Test";
            artist.Description = "Description New 1";
            artist.YearOfBirth = "2010";
            artist.YearOfDeth = "2019";

            listArtists.Add(artist);

            //artist = new CMSModels.Artist();
            //artist.id = null;
            //artist.Name = "Mahesh";
            //artist.DateEntered = DateTime.Now;
            //artist.DateModified = DateTime.Now;
            //artist.Description = "Mahesh Description";

            //listArtists.Add(artist);

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(listArtists));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/SyncMasters/PushArtists", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> PushAuctions()
        {
            List<CMSModels.PushAuctionCalendar> listArtists = new List<CMSModels.PushAuctionCalendar>();

            CMSModels.PushAuctionCalendar artist = new CMSModels.PushAuctionCalendar();
            artist.Id = "1e138dcc-f241-1dc9-8a50-5ce779c48d9f";
            artist.Description = "Description";
            artist.AuctionTitle = "Art auction title New";
            artist.StartDate = DateTime.Now;
            artist.EndDate = DateTime.Now;
            artist.CityDescription = "Chichwad Desc New";
            artist.AuctionNumber = "ART00721";

            listArtists.Add(artist);

            artist = new CMSModels.PushAuctionCalendar();
            artist.Id = "37733d0b-1d6c-2470-781a-5ce67d2a55f2";
            artist.Description = "Description";
            artist.AuctionTitle = "Art auction title New";
            artist.StartDate = DateTime.Now;
            artist.EndDate = DateTime.Now;
            artist.CityDescription = "Viman Nagar New";
            artist.AuctionNumber = "123323";

            listArtists.Add(artist);

            // Serialize our concrete class into a JSON String
            var stringPayload = JsonConvert.SerializeObject(listArtists);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/SyncMasters/PushAuctions", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> PushSymbols()
        {
            List<CMSModels.PushSymbols> listArtists = new List<CMSModels.PushSymbols>();

            CMSModels.PushSymbols artist = new CMSModels.PushSymbols();
            artist.Id = "d1ac7c6a-1ff3-9f3a-3bb1-5ce7c01c6e6e";
            artist.Code = "Code 1";
            artist.Description = "Description New";


            listArtists.Add(artist);

            artist = new CMSModels.PushSymbols();
            artist.Id = "e25233fc-a6aa-e38a-8e36-5ce7c0d572ac";
            artist.Code = "Code 2";
            artist.Description = "Description 2";

            listArtists.Add(artist);

            // Serialize our concrete class into a JSON String
            var stringPayload = JsonConvert.SerializeObject(listArtists);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/SyncMasters/PushSymbols", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> PushLocations()
        {
            List<CMSModels.PushLocation> listArtists = new List<CMSModels.PushLocation>();

            CMSModels.PushLocation artist = new CMSModels.PushLocation();
            artist.Id = "2814c335-2dee-f8d7-5788-5ce7c4851b4b";
            artist.Name = "Loc 1";


            listArtists.Add(artist);

            artist = new CMSModels.PushLocation();
            artist.Id = "7514fbdb-cec1-0262-daf5-5ce7c40aa3ba";
            artist.Name = "Loc 2";

            listArtists.Add(artist);

            // Serialize our concrete class into a JSON String
            var stringPayload = JsonConvert.SerializeObject(listArtists);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/SyncMasters/PushLocations", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public static async Task<string> PushCategories()
        {
            List<CMSModels.PushCategories> listArtists = new List<CMSModels.PushCategories>();

            CMSModels.PushCategories artist = new CMSModels.PushCategories();
            artist.Id = "1d8fa0ad-7a94-58d4-8776-5cdd0705ad57";
            artist.Code = "Code 1";
            artist.Name = "Name 1";


            listArtists.Add(artist);

            artist = new CMSModels.PushCategories();
            artist.Id = "b0bf5be2-be26-5ffa-16e1-5ce7c86007bb";
            artist.Code = "Code 2";
            artist.Name = "Name 2";

            listArtists.Add(artist);

            // Serialize our concrete class into a JSON String
            var stringPayload = JsonConvert.SerializeObject(listArtists);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/SyncMasters/PushCategories", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        static async Task<string> PullMasterData()
        {
            HttpResponseMessage response = await client.GetAsync(
                "api/SyncMasters/PullMasterData?methodName=Artists&lastSyncDate=" + DateTime.Now.AddDays(-3).ToString() + "");

            var result = await response.Content.ReadAsStringAsync();


            return result;
        }

        static async Task<string> GetModuleDetails(string moduleName)
        {
            HttpResponseMessage response = await client.GetAsync(
                "api/SyncMasters/GetModulesDetails");

            var result = await response.Content.ReadAsStringAsync();


            return result;
        }

        public void PostEvent_data() //Adding Event  
        {
            using (var client = new WebClient())
            {
                AuctionCalendar objAuctCal = new AuctionCalendar(); //Setting parameter to post  
                //objAuctCal.Id = "1";
                objAuctCal.Name = "PushedCalendar";
                objAuctCal.Description = "PushedCalendar";
                objAuctCal.StartDate = DateTime.Now;
                objAuctCal.AuctionTitle = "TestTitle";
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString("http://localhost:49610/api/SyncMasters/PushAuctions", JsonConvert.SerializeObject(objAuctCal));
                Console.WriteLine(result);
            }
        }
    }
}
