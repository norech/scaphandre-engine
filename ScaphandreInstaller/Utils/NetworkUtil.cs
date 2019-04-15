using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ScaphandreInstaller.Utils
{
    public static class NetworkUtil
    {
        public const SecurityProtocolType Tls1_2_Protocol = (SecurityProtocolType)((SslProtocols)0x00000C00);

        public static string GetData(string uri)
        {
            ServicePointManager.MaxServicePointIdleTime = 1000;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | Tls1_2_Protocol;

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.UserAgent = "Scaphandre Engine Installer";
            request.Method = "GET";
            request.KeepAlive = true;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                    return reader.ReadToEnd();
            }
        }

        public static XDocument GetJson(string url)
        {
            return JsonConvert.DeserializeXNode("{ \"root\": " + GetData(url) + "}");
        }

        public static T GetJson<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(GetData(url));
        }

        public static void DownloadFile(string url, string path)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(new Uri(url), path);
        }
    }
}
