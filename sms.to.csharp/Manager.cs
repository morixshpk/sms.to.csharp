﻿using System.Net.Http;
using sms.to.csharp.Models;
using System.Text.Json;
using sms.to.csharp.Services;
using System;

namespace sms.to.csharp
{
    public static class Manager
    {
        private static HttpClient _httpClient;
        internal static InitConfigs configs;

        public static void Init(InitConfigs configuatation)
        {
            configs = configuatation;
            Logger.Dir = configs.LoggsPath;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(configs.ApiUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configs.ApiKey}");
        }

        internal static TResponse Send<TRequest, TResponse>(TRequest reqModel, string endpoint) where TRequest : class where TResponse : class
        {
            TResponse data = null;
            try
            {
                string jsonRequest = JsonSerializer.Serialize(reqModel);
                StringContent jsonContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = _httpClient.PostAsync(endpoint, jsonContent).Result)
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        data = JsonSerializer.Deserialize<TResponse>(responseBody);

                        Logger.Custom(Utils.DEBUG_FILE, $" Http response status code {response.StatusCode}, response: {responseBody}");
                    }
                    catch (Exception ex)
                    {
                        Logger.Custom(Utils.ERROR_FILE, $"Error: {ex.Message} \n {ex.StackTrace}");
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Logger.Custom(Utils.ERROR_FILE, $"Error: {ex.Message}, \n {ex.StackTrace} ");
                return data;
            }
        }

        public static void SendSMS(SingleSMSRequest model)
        {
            var data = Send<SingleSMSRequest, SingleSMSResponse>(model, SMSTOEndpoints.SEND) ?? throw new Exception("Could not send sms");
        }

        public static void SendCampaignSMS(CamapignSMSRequest model)
        {
            var data = Send<CamapignSMSRequest, CampaignSMSResponse>(model, SMSTOEndpoints.SEND) ?? throw new Exception("Could not send campaign sms");
        }

        public static void SendPersonalizedSMS(PersonalizedSMSRequest model)
        {
            var data = Send<PersonalizedSMSRequest, CampaignSMSResponse>(model, SMSTOEndpoints.SEND) ?? throw new Exception("Could not send personalized sms");
        }

        public static void SendFlashSMS(SingleSMSRequest model)
        {
            var data = Send<SingleSMSRequest, SingleSMSResponse>(model, SMSTOEndpoints.FLASH_SMS_SEND) ?? throw new Exception("Could not send flash sms");
        }

        public static void ScheduleSMS(ScheduleSMSRequest model)
        {
            var data = Send<ScheduleSMSRequest, ScheduleSMSResponse>(model, SMSTOEndpoints.SEND) ?? throw new Exception("Could not schedule sms");
        }

        public static void EstimateSingleSms(SingleSMSRequest model)
        {
            var data = Send<SingleSMSRequest, EstimatedResponse>(model, SMSTOEndpoints.ESTIMATED) ?? throw new Exception("Could not estimate single sms");
            Logger.Custom(Utils.ESTIMATED_FILE, $"Estimated single sms cost: {data.EstimatedCost} for prefix {model.To.Substring(0, 4)}");
        }

        public static void EstimateCampaignSms(CamapignSMSRequest model)
        {
            var data = Send<CamapignSMSRequest, EstimatedResponse>(model, SMSTOEndpoints.ESTIMATED) ?? throw new Exception("Could not estimate campaign sms");
            Logger.Custom(Utils.ESTIMATED_FILE, $"Estimated campaign sms cost: {data.EstimatedCost} for campaign sms");
        }

        public static void EstimatePersonalizedSms(CamapignSMSRequest model)
        {
            var data = Send<CamapignSMSRequest, EstimatedResponse>(model, SMSTOEndpoints.ESTIMATED) ?? throw new Exception("Could not estimate personalized sms");
            Logger.Custom(Utils.ESTIMATED_FILE, $"Estimated personalized cost: {data.EstimatedCost} for Personalized SMS");
        }
    }
}