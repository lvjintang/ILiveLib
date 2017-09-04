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
        private string url = "http://{0}:{1}@ddns.oray.com/ph/update?hostname={2}";

        public void UpdateHost(string user, string pass,string host)
        {
            String querys = string.Format(url,user,pass,host);

            Crestron.SimplSharp.Net.Http.HttpClient client = new Crestron.SimplSharp.Net.Http.HttpClient();

            HttpClientRequest request = this.GetRequest(url);


            try
            {
                HttpClientResponse httpResponse = client.Dispatch(request);
                ILiveDebug.Instance.WriteLine("ILiveOray:" + querys);
                httpResponse.Encoding = Encoding.UTF8;
                string html = httpResponse.ContentString;
                ILiveDebug.Instance.WriteLine(html);
            }
            catch (Exception ex)
            {

                ILiveDebug.Instance.WriteLine("ex:" + ex.Message);
            }
        }

        public HttpClientRequest GetRequest(string aUrl)
        {
            HttpClientRequest httpClientRequest = new HttpClientRequest();
            httpClientRequest.Url.Parse(aUrl);
            httpClientRequest.Header.SetHeaderValue("Host", "ddns.oray.com");
            //httpClientRequest.Header.SetHeaderValue("Authorization", APPCode);
            httpClientRequest.KeepAlive = true;
            return httpClientRequest;
        }
    }
}