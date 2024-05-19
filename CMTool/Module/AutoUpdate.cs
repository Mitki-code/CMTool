using Downloader;
using Octokit;
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

namespace CMTool.Module
{
    internal class AutoUpdate
    {
        private static GitHubClient client = new(new ProductHeaderValue("CMTool"));
        private static Credentials tokenAuth = new("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");
        internal static string newVer = "";
        internal static string newUrl = "";

        internal static async Task<bool> Check(string appVersion)
        {
            client.Credentials = tokenAuth;
            var releases = await client.Repository.Release.GetAll("Mitki-code", "CMTool");
            var latest = releases[0];
            newVer = latest.Name;
            newUrl = latest.Assets[0].BrowserDownloadUrl;

            if (latest.Name != "v"+ appVersion || latest.Name != appVersion)
                return true;
            return false;
        }

        internal static async Task Down()
        {
            var downloader = new DownloadService();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(newUrl);
            response.EnsureSuccessStatusCode();
            string responseUri = response.RequestMessage.RequestUri.ToString();

            await downloader.DownloadFileTaskAsync(responseUri, AppDomain.CurrentDomain.BaseDirectory + @"temp");
        }
    }
}
