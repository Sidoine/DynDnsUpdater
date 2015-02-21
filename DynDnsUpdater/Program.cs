using System;
using System.Net;
using System.Text;
using System.Web;

namespace DynDnsUpdater
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceUrl = args[0];
            var hostName = args[1];
            var userName = args[2];
            var password = args[3];
            
            var webClient = new WebClient();
            var ip = webClient.DownloadString("http://sidoine.net/cgi-bin/ip.pl");
            ip = ip.Trim(new[] {'\r', '\n', ' '});
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password));
            webClient.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
            var url = string.Format("http://{0}/nic/update/?hostname={1}&myip={2}&system=dyndns",
                serviceUrl, HttpUtility.UrlEncode(hostName), HttpUtility.UrlEncode(ip));
            var result = webClient.DownloadString(url);
            Console.Write(result);
        }
    }
}
