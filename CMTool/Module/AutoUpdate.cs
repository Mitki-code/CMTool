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
        internal static string newVer = "";
        internal static string newUrl = "";

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
            Version version = new Version(FileIO.Version);
            var postFileList = await GetWebResponse("https://e.coding.net/open-api/?Action=DescribeArtifactRepositoryFileList&action=DescribeArtifactRepositoryFileList",
                "application/json", "{\n  \"Project\": \"CMTool\",\n  \"Repository\": \"release\",\n  \"ContinuationToken\": \"\",\n  \"PageSize\": 1000\n}", ParameterType.RequestBody);
            foreach (var postFile in postFileList["Response"]["Data"]["InstanceSet"])
            {
                Version postVersion = new Version(postFile["VersionName"].ToString());
                if (postVersion > version)
                {
                    var postFileUrl = await GetWebResponse("https://e.coding.net/open-api/?Action=DescribeArtifactFileDownloadUrl&action=DescribeArtifactFileDownloadUrl",
                    "application/json", "{\n  \"ProjectId\": \"13081751\",\n  \"Repository\": \"release\",\n  \"Package\": \"CMTool.rar\",\n  \"PackageVersion\": " + postFile["VersionName"] + ",\n  \"FileName\": \"CMTool.exe\",\n  \"Timeout\\\"\": \"600\"\n}", ParameterType.RequestBody);

                    newVer = postVersion.ToString();
                    newUrl = postFileUrl["Response"]["Url"].ToString();
                    return true;
                }
            }
            return false;

        }

        internal static async Task Down()
        {
            var downloader = new DownloadService();

            await downloader.DownloadFileTaskAsync(newUrl, AppDomain.CurrentDomain.BaseDirectory + @"temp.exe");
        }
    }
}
