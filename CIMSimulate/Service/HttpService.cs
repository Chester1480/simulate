using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace CIMSimulate.Service
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
    }

}
