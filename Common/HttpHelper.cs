using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.AspNetCore.Mvc;


namespace Common
{
    public static class HttpHelper
    {
        public static string WebPost(Method method, string url, string baseUrl, string dataJson)
        {
            try
            {
                var options = new RestClientOptions(url)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(baseUrl, method);
                request.AddHeader("Content-Type", "application/json");

                request.AddStringBody(dataJson, DataFormat.Json);
                var response = client.ExecuteAsync(request).Result;
                return response.Content!;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public static string WebPost_WithToken(Method method,string url, string baseUrl, string dataJson, string token)
        {
            try
            {
                var options = new RestClientOptions(url)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(baseUrl, method);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddStringBody(dataJson, DataFormat.Json);
                var response = client.ExecuteAsync(request).Result;
                return response.Content!;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public static string GetApiToken(object listResult,Method method,string base_url, string token)
        {
            try
            {
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var dataJson = JsonConvert.SerializeObject(listResult);
                var result = WebPost_WithToken(method, url_api, base_url, dataJson, token);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    
    }

}
