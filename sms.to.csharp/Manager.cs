using System.Net.Http;
using sms.to.csharp.Models;
using System.Text.Json;
using System;

namespace sms.to.csharp
{
    public static class Manager
    {
        private static HttpClient _httpClient;
        internal static InitConfigs configs;

        public static void Init(InitConfigs configuatation)
        {
            configuatation.Validate();

            configs = configuatation;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(configs.ApiUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configs.ApiKey}");
        }

        internal static TResponse Send<TRequest, TResponse>(TRequest reqModel, string endpoint) where TRequest : class where TResponse : class
        {
            TResponse data = null;

            string jsonRequest = JsonSerializer.Serialize(reqModel);
            StringContent jsonContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = _httpClient.PostAsync(endpoint, jsonContent).Result)
            {
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                data = JsonSerializer.Deserialize<TResponse>(responseBody);
            }
            return data;
        }


        public static SingleSMSResponse SendSMS(SingleSMSRequest model)
        {
            model.Validate();
            var data = Send<SingleSMSRequest, SingleSMSResponse>(model, SMSTOEndpoints.SEND);
            var estimatedData = Send<SingleSMSRequest, EstimatedResponse>(model, SMSTOEndpoints.ESTIMATED);
            data.EstimatedSmsCost = estimatedData.EstimatedCost;
            return data;
        }

        public static CampaignSMSResponse SendCampaignSMS(CamapignSMSRequest model)
        {
            model.Validate();
            return Send<CamapignSMSRequest, CampaignSMSResponse>(model, SMSTOEndpoints.SEND);
        }

        public static PersonalizedSMSResponse SendPersonalizedSMS(PersonalizedSMSRequest model)
        {
            model.Validate();
            return Send<PersonalizedSMSRequest, PersonalizedSMSResponse>(model, SMSTOEndpoints.SEND);
        }

        public static SingleSMSResponse SendFlashSMS(SingleSMSRequest model)
        {
            model.Validate();
            return Send<SingleSMSRequest, SingleSMSResponse>(model, SMSTOEndpoints.FLASH_SMS_SEND);
        }

        public static ScheduleSMSResponse ScheduleSMS(ScheduleSMSRequest model)
        {
            model.Validate();
            return Send<ScheduleSMSRequest, ScheduleSMSResponse>(model, SMSTOEndpoints.SEND);
        }

        public static EstimatedResponse EstimateSingleSms(SingleSMSRequest model)
        {
            model.Validate();
            return Send<SingleSMSRequest, EstimatedResponse>(model, SMSTOEndpoints.ESTIMATED);
        }

        public static EstimatedResponse EstimateCampaignSms(CamapignSMSRequest model)
        {
            model.Validate();
            return Send<CamapignSMSRequest, EstimatedResponse>(model, SMSTOEndpoints.ESTIMATED);
        }

        public static EstimatedResponse EstimatePersonalizedSms(PersonalizedSMSRequest model)
        {
            model.Validate();
            return Send<PersonalizedSMSRequest, EstimatedResponse>(model, SMSTOEndpoints.ESTIMATED);
        }
    }
}