using AMIntegration.AMAPI;
using AMIntegration.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMIntegration.Modules.Auction
{
    public class PushAuction
    {
        string suitesessionId = string.Empty;
        CommonClass common = new CommonClass();

        #region Get Data from Suite and Push to AM

        public PushListofSuiteAuctionResponse PushListofAMAuction(string auctionId, string logId)
        {
            SuiteWrapper.WriteTraceLog("PushAMAuction", "---------------- AUCTION SYNC START ------------------------- ");
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Push Auction to AM sync started", Name = "PushAuction", Entity_c = "Auction" }, logId);

            var amWrapper = new AMWrapper();
            var suiteWrapper = new SuiteWrapper();
            

            var ret = new PushListofSuiteAuctionResponse();

            ret.List = new List<AMCustomer.AMAuction>();
            try
            {
                suitesessionId = suiteWrapper.Login().GetAwaiter().GetResult();

                var requestAuction = new SuiteReadEntryListRequest();
                requestAuction.SessionId = suitesessionId;
                requestAuction.ModuleName = "AC1_Auction_Calendar";
                requestAuction.Query = $"ac1_auction_calendar_cstm.auction_number_c in ('{string.Join("','", auctionId.Split(','))}')";
                requestAuction.SelectFields = new List<string> {
                    "id",
                    "name",
                    "auction_number_c",
                    "auction_type_c",
                    "currency_type_c",
                    "start_date_c",
                    "invoice_description_c",
                    "viewing_c",
                    "city_description_c",
                    "city_c",
                    };

                var auction_List = suiteWrapper.GetList(requestAuction).GetAwaiter().GetResult();

                if (auction_List.Count > 0)
                {
                    foreach (var item in auction_List.EntityList.Children())
                    {

                        var itemProperties = item.Children<JProperty>();

                        var values = new Dictionary<string, string>
                                    {
                                        { "method", "POST" },
                                        { "input_type", "JSON" },
                                        { "response_type", "JSON" },
                                        { "integration_id", itemProperties.FirstOrDefault(xx => xx.Name == "id").Value.ToString()},
                                    };

                        if (!string.IsNullOrEmpty(itemProperties.FirstOrDefault(xx => xx.Name == "name").Value.ToString()))
                            values.Add("title", itemProperties.FirstOrDefault(xx => xx.Name == "name").Value.ToString());

                        values.Add("auction_code", itemProperties.FirstOrDefault(xx => xx.Name == "auction_number_c").Value.ToString());

                        var auction_type_c = GetAuctionType(itemProperties.FirstOrDefault(xx => xx.Name == "auction_type_c").Value.ToString());
                        values.Add("auction_type", auction_type_c);


                        var currency_type_c = GetCurrencyCode(itemProperties.FirstOrDefault(xx => xx.Name == "currency_type_c").Value.ToString());
                        values.Add("currency_code", currency_type_c);

                        values.Add("location_name", itemProperties.FirstOrDefault(xx => xx.Name == "city_c").Value.ToString());

                        string start_date_c = Convert.ToDateTime(itemProperties.FirstOrDefault(xx => xx.Name == "start_date_c").Value.ToString()).ToString("yyyy-MM-dd'T'HH:mm'Z'");

                        values.Add("time_start", start_date_c);

                        values.Add("description", itemProperties.FirstOrDefault(xx => xx.Name == "invoice_description_c").Value.ToString());

                        values.Add("viewing_information", itemProperties.FirstOrDefault(xx => xx.Name == "viewing_c").Value.ToString());

                        values.Add("location_description", itemProperties.FirstOrDefault(xx => xx.Name == "city_description_c").Value.ToString());

                        var result_Auction = amWrapper.PostDataToAM(values, $"{amWrapper.apiUrl}/v1/auction/").GetAwaiter().GetResult();


                        var responseData = JObject.Parse(result_Auction).SelectToken("response");
                        string AMId;
                        if (responseData != null)
                        {
                            AMId = responseData.SelectToken("row_id").ToString();
                            SuiteWrapper.WriteTraceLog("PushAMAuction", "Auction Inserted to AM successfully for Id :" + AMId);
                            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Auction Inserted to AM successfully for Id :" + itemProperties.FirstOrDefault(xx => xx.Name == "id").Value.ToString(), Name = "PushAMAuction", Entity_c = "Auction" }, logId);
                            ret.StatusCode = ClsConstant.SuccessCode;
                            ret.Message = ClsConstant.SuccessMessage;
                        }
                        else
                        {
                            var message = JObject.Parse(result_Auction).SelectToken("error");
                            AMId = "";
                            SuiteWrapper.WriteTraceLog("PushAMAuction", "Error Message : " + message);
                            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error Message : " + message, Name = "PushAMAuction", Entity_c = "Auction" }, logId);

                            ret.StatusCode = ClsConstant.ServerErrorCode;
                            ret.Message = ClsConstant.ServerErrorMsg;
                        }

                        AMCustomer.AMAuction result = new AMCustomer.AMAuction();
                        result.SuiteId = itemProperties.FirstOrDefault(xx => xx.Name == "id").Value.ToString();
                        result.Row_id = AMId;
                        ret.List.Add(result);
                    }
                }
                else
                {
                    common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "No any Auction exist for period", Name = "PushAMAuction", Entity_c = "Auction" }, logId);
                    SuiteWrapper.WriteTraceLog("PushAMAuction", "No any Auction exist for period");
                }
            }
            catch (Exception ex)
            {
                common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Error Message : " + ex.Message, Name = "PushAMAuction", Entity_c = "Auction" }, logId);
                ret.StatusCode = ClsConstant.ServerErrorCode;
                ret.Message = ClsConstant.ServerErrorMsg;

                return ret;
            }
            SuiteWrapper.WriteTraceLog("PushAMAuction", "---------------- AUCTION SYNC END ------------------------- ");
            common.InsertSuiteAmIntegrationLogDetail(new CreateTraceLogRequest() { Description = "Push Auction to AM sync ended", Name = "PushAMAuction", Entity_c = "Auction" }, logId);
            return ret;
        }

        private string GetCurrencyCode(string val)
        {
            var result = "";
            switch (val)
            {
                case "Indian_Rupees":
                    result = "INR";
                    break;
                case "US_Dollar":
                    result = "USD";
                    break;
                case "Canadian_Dollar":
                    result = "CAD";
                    break;
            }
            return result;
        }

        private string GetAuctionType(string val)
        {
            var result = "";
            switch (val)
            {
                case "Timed_Auction":
                    result = "timed";
                    break;
                case "Live_Auction":
                    result = "live";
                    break;
            }
            return result;
        }
        #endregion
    }
}