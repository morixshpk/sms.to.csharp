﻿using sms.to.Models;
using System.Net.Http;
using System.Text.Json;

namespace sms.to
{
    public static class ApiClient
    {
        private static HttpClient _httpClient;
        private static Config _config;

        public static void Init(Config config)
        {
            config.Validate();

            _config = config;

            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://api.sms.to")
            };
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config.ApiKey}");
        }

        internal static TResponse Send<TRequest, TResponse>(TRequest req, string method) where TRequest : class where TResponse : class
        {
            TResponse data = null;

            var jsonRequest = JsonSerializer.Serialize(req);
            var jsonContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            using (var response = _httpClient.PostAsync(method, jsonContent).Result)
            {
                response.EnsureSuccessStatusCode();
                var responseBody = response.Content.ReadAsStringAsync().Result;
                data = JsonSerializer.Deserialize<TResponse>(responseBody);
            }
            return data;
        }

        internal static SingleSMSResponse SendSMS(SingleSMSRequest req)
        {
            if (string.IsNullOrEmpty(req.SenderId))
                req.SenderId = _config.SenderId;

            return Send<SingleSMSRequest, SingleSMSResponse>(req, "sms/send");
        }

        internal static EstimatedResponse EstimateSingleSMS(SingleSMSRequest req)
        {
            if (string.IsNullOrEmpty(req.SenderId))
                req.SenderId = _config.SenderId;

            return Send<SingleSMSRequest, EstimatedResponse>(req, "sms/estimate");
        }
    }
}