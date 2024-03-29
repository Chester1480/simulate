﻿using CIMSimulate.Models;
using CIMSimulate.Service.SimulateService;
using CIMSimulate.Service.UtilS;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CIMSimulate.Controllers
{
    public class CIMController : Controller
    {

        private SoapService _soapService;
        private HttpService _httpService;
        private FileService _fileService;
        private IHostingEnvironment _environment;
        private JQservice _jqservice;

        public CIMController(IServiceProvider service)
        {
            _soapService = service.GetService<SoapService>()!;
            _httpService = service.GetService<HttpService>()!;
            _fileService = service.GetService<FileService>()!;
            _environment = service.GetService<IHostingEnvironment>()!;
            _jqservice = service.GetService<JQservice>()!;
        }

        public IActionResult Index()
        {
            return View();
        }

        private string GetApiUrl()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var apiUrl = config.GetValue<string>("APIUrl:CIM");
            return apiUrl;
        }

        private string BuildAGVStatusXmldata(dynamic parameters)
        {
            //{"TIMESTAMP":"2022 / 11 / 01 18:34:41","TASKNAME":"20220915000133098777______XPDB","AGVID":"MR101","ELECTRICVOLUMN":"0.66","TASKTYPE":"EW","LOTID":"04B3UGW003","POSITION":"777","ERRMSG":""}
            string paraMessage = $@"";
            string xmlString = $@"
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">  
                  <soap:Body>  
                    <ServiceProvider  xmlns=""http://tempuri.org/"">  
                      <funcName>AGVStatus</funcName>
                      <paraMessage>{paraMessage}</paraMessage>
                      </ServiceProvider>  
                  </soap:Body>  
            </soap:Envelope>
            ";
            return xmlString;
        }
        private string BuildDockingXmldata(dynamic parameters)
        {
            string DockingCheck = parameters.DockingCheck;
            // <CMD>DockingCheck</CMD>
            // <EQPNAME>E0065821</EQPNAME><EQPID>ASE21-2190-A01</EQPID><AGVNAME>MR101</AGVNAME><AGVIP>172.20.95.41</AGVIP><PORT>1</PORT><AGVSTATUS>Put in</AGVSTATUS><rtnMessage></rtnMessage> 
            string paraMessage = $@"";
            string xmlString = $@"
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">  
                    <soap:Body>  
                    <DockCheck  xmlns=""http://tempuri.org/"">  
                      <CMD>{DockingCheck}</CMD>
                    </DockCheck>  
                    </soap:Envelope>              
                </soap:Body>  
            ";
            return xmlString;
        }

                    //[HttpPost]
                    //public async Task<CIMResponse> Action([FromBody] CIMRequest request)
                    //{

                    //    string wwwPath = _environment.WebRootPath;
                    //    var obj = await _fileService.ReadAsync("");
                    //    //string funcName = request.funcName;
                    //    //string paraMessage = request.paraMessage;

                    //    //if (string.IsNullOrEmpty(funcName))
                    //    //{
                    //    //    return  new CIMResponse
                    //    //    {
                    //    //        ServiceProviderResult = "false",
                    //    //        rtnMessage = "funcName 參數不可為空",
                    //    //    };
                    //    //}

                    //    //if (string.IsNullOrEmpty(paraMessage))
                    //    //{
                    //    //    return  new CIMResponse
                    //    //    {
                    //    //        ServiceProviderResult = "false",
                    //    //        rtnMessage = "",
                    //    //    };
                    //    //}

                    //    return new CIMResponse
                    //    {
                    //        ServiceProviderResult = "1",
                    //        rtnMessage = "執行成功",
                    //    };


                    //}

                    //public class TestModel {
                    //          public string TIMESTAMP { get; set; }
                    //}

        [HttpPost]
        public IActionResult AGVStatus([FromBody]dynamic parameters)
        {
                         
            var payload = JsonConvert.DeserializeObject<dynamic>(parameters);
            #region
            //{
            //    "TIMESTAMP": "2022/11/01 18:29:36",
            //    "TASKNAME": "20220915000133098777______XPDB",
            //    "AGVID": "MR101",
            //    "ELECTRICVOLUMN": "0.66",
            //    "TASKTYPE": "S",
            //    "LOTID": "04B3UGW003",
            //    "POSITION": "777",
            //    "ERRMSG": ""
            //}
            #endregion

            string apiUrl = GetApiUrl();

            //組xml parameters
            var serializeString = JsonConvert.SerializeObject(payload);

            string xmlString = $@"
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">  
                  <soap:Body>  
                    <ServiceProvider  xmlns=""http://tempuri.org/"">  
                      <funcName>AGVStatus</funcName>
                      <paraMessage>
                      {serializeString}
                      </paraMessage>
                      </ServiceProvider>  
                  </soap:Body>  
            </soap:Envelope>";
            //post
            var response = _httpService.SOAPWebRequest(apiUrl, xmlString);

            return Ok(new
            {
                code = "1",
                message = "執行成功",
                data = response
            });
        }

        [HttpPost]
        public IActionResult DockingStart([FromBody] dynamic parameters)
        {
            var payload = JsonConvert.DeserializeObject<dynamic>(parameters);
            string apiUrl = GetApiUrl();
            //組xml
            //var xmlParameters = BuildDockingXmldata(parameters);
            string xmlString = $@"
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">  
                    <soap:Body>  
                    <DockCheck  xmlns=""http://tempuri.org/"">  
                        <CMD>DockingStart</CMD>
                        <EQPNAME>{payload.EQPNAME+""}</EQPNAME>
                        <EQPID>{payload.EQPID + ""}</EQPID>
                        <AGVNAME>{payload.AGVNAME + ""}</AGVNAME>
                        <AGVIP>{payload.AGVIP + ""}</AGVIP>
                        <PORT>{payload.PORT + ""}</PORT>
                        <AGVSTATUS>{payload.AGVSTATUS + ""}</AGVSTATUS>
                        <rtnMessage></rtnMessage>              
                    </DockCheck>                           
                    </soap:Body>  
                </soap:Envelope>";

            var response =  _httpService.SOAPWebRequest(apiUrl, xmlString);
            return Ok(new
            {
                code = "1",
                message = "執行成功",
                data = response
            });
        }
 
        [HttpPost]
        public IActionResult DockingEnd([FromBody] dynamic parameters)
        {
            var payload = JsonConvert.DeserializeObject<dynamic>(parameters);
            string apiUrl = GetApiUrl();
            //組xml
            //var xmlParameters = BuildDockingXmldata(parameters);
            string xmlString = $@"
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">  
                    <soap:Body>  
                    <DockCheck  xmlns=""http://tempuri.org/"">  
                        <CMD>DockingEnd</CMD>
                        <EQPNAME>{payload.EQPNAME + ""}</EQPNAME>
                        <EQPID>{payload.EQPID + ""}</EQPID>
                        <AGVNAME>{payload.AGVNAME + ""}</AGVNAME>
                        <AGVIP>{payload.AGVIP + ""}</AGVIP>
                        <PORT>{payload.PORT + ""}</PORT>
                        <AGVSTATUS>{payload.AGVSTATUS + ""}</AGVSTATUS>
                        <rtnMessage></rtnMessage>   
                    </DockCheck>  
                    </soap:Body>
                </soap:Envelope>";

            var response = _httpService.SOAPWebRequest(apiUrl, xmlString);
            return Ok(new
            {
                code = "1",
                message = "執行成功",
                data = response
            });
        }

        [HttpPost]
        public IActionResult DockingCheck([FromBody] dynamic parameters)
        {
            var payload = JsonConvert.DeserializeObject<dynamic>(parameters);
            string apiUrl = GetApiUrl();
            //組xml
            //var xmlParameters = BuildDockingXmldata(parameters);
            string xmlString = $@"
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">  
                    <soap:Body>  
                    <DockCheck  xmlns=""http://tempuri.org/"">  
                        <CMD>DockingCheck</CMD>
                        <EQPNAME>{payload.EQPNAME + ""}</EQPNAME>
                        <EQPID>{payload.EQPID + ""}</EQPID>
                        <AGVNAME>{payload.AGVNAME + ""}</AGVNAME>
                        <AGVIP>{payload.AGVIP + ""}</AGVIP>
                        <PORT>{payload.PORT + ""}</PORT>
                        <AGVSTATUS>{payload.AGVSTATUS + ""}</AGVSTATUS>
                        <rtnMessage></rtnMessage>   
                    </DockCheck>                                
                    </soap:Body>  
                </soap:Envelope>";

            var response = _httpService.SOAPWebRequest(apiUrl, xmlString);
            return Ok(new
            {
                code = "1",
                message = "執行成功",
                data = response
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetLastWorkOrder()
        {
            var result = await _jqservice.GetLastWorkOrder();
            return Ok(result); 
        }

    }
}
