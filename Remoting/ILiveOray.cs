using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;

namespace ILiveLib
{
    public class ILiveOray
    {
        private string url = "http://ddns.oray.com/ph/update?hostname={0}";

        public void UpdateHost(string user, string pass,string host)
        {
            String querys = string.Format(url, host);
            Crestron.SimplSharp.Net.Http.HttpClient client = new Crestron.SimplSharp.Net.Http.HttpClient();

            string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(user + ":" + pass));
            HttpClientRequest request = this.GetRequest(querys, auth);


            try
            {
                ILiveDebug.Instance.WriteLine("ILiveOray:" + querys);
                HttpClientResponse httpResponse = client.Dispatch(request);
                httpResponse.Encoding = Encoding.UTF8;
                string html = httpResponse.ContentString;
                ILiveDebug.Instance.WriteLine(html);
            }
            catch (Exception ex)
            {

                ILiveDebug.Instance.WriteLine("ex:" + ex.Message);
            }
        }

        public HttpClientRequest GetRequest(string aUrl,string auth)
        {
            HttpClientRequest httpClientRequest = new HttpClientRequest();
            httpClientRequest.Url.Parse(aUrl);
            httpClientRequest.Header.SetHeaderValue("Host", "ddns.oray.com");
            httpClientRequest.Header.SetHeaderValue("Authorization",auth);
            httpClientRequest.Header.SetHeaderValue("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1");
            httpClientRequest.Header.SetHeaderValue("Upgrade-Insecure-Requests", "1");
            httpClientRequest.KeepAlive = true;
            return httpClientRequest;
        }
    }
}