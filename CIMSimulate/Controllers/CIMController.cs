using CIMSimulate.Models;
using CIMSimulate.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CIMSimulate.Controllers
{
    public class CIMController : Controller
    {

        private SoapService _soapService;
        private HttpService _httpService;
        private FileService _fileService;
        private IHostingEnvironment _environment;

        public CIMController(IServiceProvider service)
        {
            _soapService = service.GetService<SoapService>()!;
            _httpService = service.GetService<HttpService>()!;
            _fileService = service.GetService<FileService>()!;
            _environment = service.GetService<IHostingEnvironment>()!;
        }

        public IActionResult Index()
        {
            return View();
        }  

        public async Task<CIMResponse> Action(CIMRequeast request)
        {
            string wwwPath = _environment.WebRootPath;
            var obj = await _fileService.ReadAsync("");
            //string funcName = request.funcName;
            //string paraMessage = request.paraMessage;

            //if (string.IsNullOrEmpty(funcName))
            //{
            //    return  new CIMResponse
            //    {
            //        ServiceProviderResult = "false",
            //        rtnMessage = "funcName 參數不可為空",
            //    };
            //}

            //if (string.IsNullOrEmpty(paraMessage))
            //{
            //    return  new CIMResponse
            //    {
            //        ServiceProviderResult = "false",
            //        rtnMessage = "",
            //    };
            //}

            return new CIMResponse
            {
                ServiceProviderResult = "1",
                rtnMessage = "執行成功",
            };


        }

        private async Task<bool> AGVStatus()
        {
            return true;
        }

        /// <summary>
        /// 派工到mcs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<dynamic> AGVDispatch(dynamic request)
        {
            #region
            //{
            //    "TIMESTAMP":"yyyy/MM/dd HH:mm:ss",
            //    "TASKNAME":"XXXX",
            //    "LOTID":"XXX",
            //    "FROMLOCATION":"E0119953",
            //    "TOLOCATION":"RACK2",
            //    "TYPE":"UNLOAD",
            //    "PORTID":"PORT4"
            //    "ENDTRAY":"Y or N"
            //}
            #endregion
            string strPara = request.strPara;
            if (string.IsNullOrEmpty(strPara))
            {
                return new
                {
                    AGVDispatchResult = "true",
                    replyMessage = new
                    {
                        TIMESTAMP = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        ERRMSG = "strPara 不可為空"
                    },
                };
            }
            dynamic dynamicObject = JsonConvert.DeserializeObject<ExpandoObject>(strPara);
            var propertyInfos = dynamicObject.GetType().GetProperties();

            List<string> parametersCheck =new List<string>(){
               "TIMESTAMP", //"yyyy/MM/dd HH:mm:ss",
               "TASKNAME",//"XXXX",
               "LOTID",//"XXX",
               "FROMLOCATION",//"E0119953",
               "TOLOCATION",//"RACK2",
               "TYPE",//"UNLOAD",
               "PORTID",//"PORT4"
               "ENDTRAY",//"Y or N"

            };

            foreach (var propertyInfo in propertyInfos)
            {
                var itemToRemove = parametersCheck.Single(r => r == propertyInfo);
                parametersCheck.Remove(itemToRemove);
            }
            //大於0 代表傳遞的參數有少
            if(parametersCheck.Count > 0)
            {

            }

            return new
            {
                AGVDispatchResult="true",
                replyMessage = new {
                    TIMESTAMP = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), 
                    ERRMSG = ""
                },
            };
        }


    }
}
