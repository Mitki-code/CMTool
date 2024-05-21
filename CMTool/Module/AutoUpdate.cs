using Downloader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace CMTool.Module
{
    internal class AutoUpdate
    {
        //private static GitHubClient client = new(new ProductHeaderValue("CMTool"));
        //private static Credentials tokenAuth = new("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");
        internal static string newVer = "";
        internal static string newUrl = "";

        //Coding 令牌 token be8d9d53c53b86ed60df077bb0aadc66fe7a4b8f

        //private static RestRequest Header()
        //{
        //    var request = new RestRequest();
        //    request.Method = Method.Post;
        //    request.AddHeader("Authorization", "token be8d9d53c53b86ed60df077bb0aadc66fe7a4b8f");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");
        //    return request;
        //}

        private static async Task<JObject> GetWebResponse(string url,string name,object value,ParameterType type)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("Authorization", "token be8d9d53c53b86ed60df077bb0aadc66fe7a4b8f");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddParameter(name,value,type);
            var response = await client.PostAsync(request);
            return JObject.Parse(response.Content);
        }

        internal static async Task<bool> Check(string appVersion)
        {
            var postProject = await GetWebResponse("https://e.coding.net/open-api/?Action=DescribeProjectByName&action=DescribeProjectByName",
                "application/json", "{\n  \"ProjectName\": \"CMTool\"\n}", ParameterType.RequestBody);
            var response = await GetWebResponse("https://e.coding.net/open-api/?Action=DescribeArtifactRepositoryList&action=DescribeArtifactRepositoryList",
                "application/json", "{\n  \"PageNumber\": 1,\n  \"PageSize\": 10,\n  \"ProjectId\": "+ postProject["Response"]["Project"]["Id"] + ",\n  \"Type\": 2\n}", ParameterType.RequestBody);


            return false;

        }

        internal static async Task Down()
        {
            var downloader = new DownloadService();

            await downloader.DownloadFileTaskAsync(newVer, AppDomain.CurrentDomain.BaseDirectory + @"temp");
        }
    }
}
