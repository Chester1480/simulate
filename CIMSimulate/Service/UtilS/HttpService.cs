using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace CIMSimulate.Service.UtilS
{
    public class HttpService
    {
        /// <summary>
        /// Post Async
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters">dynamic</param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public async Task<string> HttpPostAsync(string url, dynamic parameters, string applicationType = "application/json")
        {
            using (var client = new HttpClient())
            {
                var jsonStr = JsonConvert.SerializeObject(parameters);
                HttpContent content = new StringContent(jsonStr);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(applicationType);
                HttpResponseMessage response = await client.PostAsync(url, content);
                try
                {
                    response.EnsureSuccessStatusCode();//用來拋出異常的
                }
                catch (Exception ex)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) // 404
                    {
                        throw new Exception("url 404");
                    }
                    else
                    {
                        throw new Exception(ex.ToString());
                    }
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        /// <summary>
        /// Post Async
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpcontent"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public async Task<string> HttpPostAsync(string url, HttpContent httpcontent, string applicationType = "application/json")
        {
            using (var client = new HttpClient())
            {
                httpcontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(applicationType);
                HttpResponseMessage response = await client.PostAsync(url, httpcontent);
                try
                {
                    response.EnsureSuccessStatusCode();//用來拋出異常的
                }
                catch (Exception ex)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) // 404
                    {
                        throw new Exception("url 404");
                    }
                    else
                    {
                        throw new Exception(ex.ToString());
                    }
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        /// <summary>
        /// Post Async
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="parameters"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public async Task<string> HttpPostAsync(string url, string token, dynamic parameters, string applicationType = "application/json")
        {
            using (var client = new HttpClient())
            {
                var jsonStr = JsonConvert.SerializeObject(parameters);
                HttpContent content = new StringContent(jsonStr);
                content.Headers.Add("Auth-Token", token);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(applicationType);
                HttpResponseMessage response = await client.PostAsync(url, content);
                try
                {
                    response.EnsureSuccessStatusCode();//用來拋出異常的
                }
                catch (Exception ex)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) // 404
                    {
                        throw new Exception("url 404");
                    }
                    else
                    {
                        throw new Exception(ex.ToString());
                    }
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        /// <summary>
        /// Http Get Async
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<string> HttpGetAsync(string url, dynamic parameters, string applicationType = "application/json")
        {
            using (var client = new HttpClient())
            {
                var jsonStr = JsonConvert.SerializeObject(parameters);
                HttpContent content = new StringContent(jsonStr);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(applicationType);
                HttpResponseMessage response = await client.PostAsync(url, content);
                try
                {
                    response.EnsureSuccessStatusCode();//用來拋出異常的
                }
                catch (HttpRequestException ex)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) // 404
                    {
                        throw new Exception("url 404");
                    }
                    else
                    {
                        throw new Exception(ex.ToString());
                    }
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        /// <summary>
        ///  Http Get Async
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="parameters"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public async Task<string> HttpGetAsync(string url, string token, dynamic parameters, string applicationType = "application/json")
        {
            using (var client = new HttpClient())
            {
                var jsonStr = JsonConvert.SerializeObject(parameters);
                HttpContent content = new StringContent(jsonStr);
                content.Headers.Add("Auth-Token", token);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(applicationType);
                HttpResponseMessage response = await client.PostAsync(url, content);
                try
                {
                    response.EnsureSuccessStatusCode();//用來拋出異常的
                }
                catch (HttpRequestException ex)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) // 404
                    {
                        throw new Exception("url 404");
                    }
                    else
                    {
                        throw new Exception(ex.ToString());
                    }
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        /// <summary>
        /// Http Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public string HttpPost(string url, dynamic parameters, string applicationType = "application/json")
        {
            using (var client = new HttpClient())
            {
                var jsonStr = JsonConvert.SerializeObject(parameters);
                HttpContent content = new StringContent(jsonStr);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(applicationType);
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                try
                {
                    response.EnsureSuccessStatusCode();//用來拋出異常的
                }
                catch (Exception ex)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound) // 404
                    {
                        throw new Exception("url 404");
                    }
                    else
                    {
                        throw new Exception(ex.InnerException.ToString());
                    }
                }
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
        }

        /// <summary>
        /// HttpWebRequest Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="url_param"></param>
        /// <returns></returns>
        public string HttpRequest(string url, Dictionary<string, string> url_param)
        {
            string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            url += "?" + string.Join("&", url_param.Select(kvp => HttpUtility.UrlEncode(kvp.Key) + "=" + HttpUtility.UrlEncode(kvp.Value)));
            //Console.WriteLine(ts + " WM debug url :" + url);
            string post_body = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bs = Encoding.UTF8.GetBytes(post_body);
            request.ContentLength = bs.Length;
            request.GetRequestStream().Write(bs, 0, bs.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd();
            //Console.WriteLine(ts + " WM debug result: " + result);
            return result;
        }


        /// <summary>
        /// SOAP WebRequest POST
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public string SOAPWebRequest(string apiUrl, string xmlString)
        { 
            XmlDocument xml = new XmlDocument();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);

            request.ContentType = "text/xml; charset=utf-8";
            request.Method = "POST";
            Console.WriteLine("xml: " + xmlString);
            xml.LoadXml(xmlString);

            using (Stream stream = request.GetRequestStream())
            {
                xml.Save(stream);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        var result = sr.ReadToEnd();
                        Console.WriteLine("response result: " + result);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error message: " + ex.Message);
                Console.WriteLine("error stackTrace: " + ex.StackTrace);
                throw ex;
            }
       
        }
    }

}
