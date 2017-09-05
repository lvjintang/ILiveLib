using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using System.Text.RegularExpressions;

namespace ILiveLib.Remoting
{
    public class DDnsOray
    {
        private string ServerUrl = "Phddns60.oray.net";
        private string AppId = "16190";
        private string AppSecret = "4293761936";
        private string AppVersion = "14988";

        private string ip="127.0.0.1";
        public void Start()
        {
            ILiveDebug.Instance.WriteLine("DDnsOray:Start");
            string i = this.GetIP();
            ILiveDebug.Instance.WriteLine("DDnsOrayIP:" + i);
            if (!string.IsNullOrEmpty(i))
            {
                if (this.ip!=i)
                {
                    this.ip = i;

                    Crestron.SimplSharp.Net.Http.HttpClientRequest request = new Crestron.SimplSharp.Net.Http.HttpClientRequest();
                    request.Url.Parse("http://13867911360:jtang1989@ddns.oray.com/ph/update?hostname=jtang2016.51vip.biz");

                    string auth="Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("13867911360:jtang1989"));
                    //request.Header.SetHeaderValue("Authorization", auth);
                    request.Header.SetHeaderValue("UserAgent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)");
                    request.Header.SetHeaderValue("Host", "ddns.oray.com");
                    request.RequestType = Crestron.SimplSharp.Net.Http.RequestType.Get;


//User-Agent:Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36
                    //request.KeepAlive = true;
                    //request.RequestType = Crestron.SimplSharp.Net.Http.RequestType.Get;
                    
                  // string html=  client.ThisClient.Get("http://ddns.oray.com/ph/update?hostname=jtang2016.51vip.biz" + this.ip);
                 //   HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create("http://ddns.oray.com/ph/update?hostname=jtang2016.51vip.biz");

                    foreach (Crestron.SimplSharp.Net.Http.HttpHeader item in request.Header)
                    {
                        ILiveDebug.Instance.WriteLine("HttpClientRequest:" + item.Name + ":" + item.Value);

                    }
                    //string up = "http://13867911360:jtang1989@ddns.oray.com/ph/update?hostname=jtang2016.51vip.biz&myip=" + ip;
                    Crestron.SimplSharp.Net.Http.HttpClient hclient = new Crestron.SimplSharp.Net.Http.HttpClient();

                    ILiveDebug.Instance.WriteLine("DDnsOrayUpdate:"+auth);
                    Crestron.SimplSharp.Net.Http.HttpClientResponse res = hclient.Dispatch(request);
                    ILiveDebug.Instance.WriteLine("DDnsOrayUpdate:4");
                    //hclient.Accept
                    //hclient.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
                    //string html = hclient.Get(up, Encoding.UTF8);

                    foreach (Crestron.SimplSharp.Net.Http.HttpHeader item in res.Header)
                    {
                        ILiveDebug.Instance.WriteLine("HttpClientResponse:" + item.Name + ":" + item.Value);

	}
                    ILiveDebug.Instance.WriteLine("DDnsOrayUpdate:" + res.ContentString);

                }
            }
        }
        public string GetIP()
        {
            string str = this.GetIPHtml();

            string p = "\\d+\\.\\d+\\.\\d+\\.\\d*";
            Regex regex = new Regex(p, RegexOptions.IgnoreCase);
            Match mh = regex.Match(str);
            if (mh != null)
            {
                return regex.Match(str).Value;
            }
            return string.Empty;

        }
   

        private string GetIPHtml()
        {
            string str = "http://ddns.oray.com/checkip";
            Crestron.SimplSharp.Net.Http.HttpClient client = new Crestron.SimplSharp.Net.Http.HttpClient();
            string html = client.Get(str, Encoding.UTF8);
            return html;
        }

    }
}