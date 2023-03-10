using CIMSimulate.Models;
using CIMSimulate.Service.UtilS;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace CIMSimulate.Controllers
{
    public class GeenTransController : Controller
    {
        private HttpService _httpService;
        private readonly string url = "";
        public GeenTransController(IServiceProvider service)
        {
            _httpService = service.GetService<HttpService>();
        }
        public IActionResult Index()
        {
            return View();
        }

        #region 派車交管API

        public class AgvDispatchRequest
        {
            public int vehicle { get; set; } //車輛代碼(必填)

            public int destination { get; set; }//目標點位(必填)

            public string status { get; set; } 
        }

        /// <summary>
        /// 車輛派發
        /// url: http://172.20.39.237:3004/agv/dispatch
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agv/dispatch")]
        public async Task<IActionResult> dispatch(AgvDispatchRequest request)
        {
            Dictionary<string, object> statusList = new Dictionary<string, object>() {
                {"ispatched","回應正常" },
                {"pending","無法派車" },
                {"arrived","已抵達" },
                {"other","例外狀況" },
            };
            var parameters = new
            {
                request.vehicle,
                request.destination,
            };

            var result = await _httpService.HttpPostAsync(url, parameters);
            //var obj = await JsonSerializer.DeserializeAsync<dynamic>(result);
            //if (result)
            //{

            //}

            if (string.IsNullOrEmpty(request.status))
            {
                var response = new
                {
                    status = "ispatched",
                    errmsg = statusList["ispatched"]
                };
                return Ok(response);
            }
            else
            {
                var response = new
                {
                    status = request.status,
                    errmsg = statusList[request.status]
                };
                return Ok(response);
            }
        }

        public class AgvInfoRequest
        {
            public int vehicle { get; set; } //車輛代碼(必填)

            public int destination { get; set; }//目標點位(必填)
        }

        /// <summary>
        /// 查詢車輛現在狀態
        /// url: http://172.20.39.237:3004/agv/info
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("agv/info")]
        public IActionResult info(string status)
        {

            Dictionary<string, object> statusList = new Dictionary<string, object>() {
                {"IDLE","待命" },
                {"WAIT","規劃中" },
                {"TRACHING","車輛移動中" },
                {"TRAFFIC","交管等待中" },
                {"ERROR","異常" },
            };
            if (string.IsNullOrEmpty(status))
            {
                var response = new
                {
                    status = "IDLE",
                    errmsg = statusList["IDLE"]
                };
                return Ok(response);
            }
            else
            {
                var response = new
                {
                    status = status,
                    errmsg = statusList[status]
                };
                return Ok(response);
            }
        }


        #endregion

        #region 線邊貨架管理API

        public class RackGetEmptyRequest
        {
            public string origin { get; set; } //車輛代碼(必填)
            public string group { get; set; } //群組代碼(必填)
            public string rack { get; set; } //貨架代碼(選填，可指定貨架)
            public string task { get; set; } //供單編號
            public string station { get; set; }//來源站點
        }

        /// <summary>
        /// 取得貨架上空儲位
        /// url: http://172.20.39.237:3003/rack/getEmpty
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("rack/getEmpty")]
        public IActionResult getEmpty(RackGetEmptyRequest request)
        {
            return Ok();
        }


        public class RackMoveInRequest
        {
            public string origin { get; set; } //車輛代碼(必填)
            public string cell { get; set; } //儲位代碼(必填)
            public string tag { get; set; } //物料編號(RFID)
            public string lot { get; set; } //貨批編號
            public string task { get; set; }//供單編號
            public string station { get; set; }//來源站點
        }

        /// <summary>
        /// 物料資訊入倉(寫入)
        /// url: http://172.20.39.237:3003/rack/moveIn
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("rack/getEmpty")]
        public IActionResult moveIn(RackGetEmptyRequest request)
        {
            return Ok();
        }

        public class RackFindProductRequest
        {
            public string origin { get; set; } //車輛代碼(必填)
            public string group { get; set; } //群組代碼(必填)
            public string rack { get; set; } //貨架代碼(選填，可指定貨架)
            public string tag { get; set; } //物料編號(RFID)(必填)
            public string type { get; set; }//物料形態(選填，可指定)
        }

        /// <summary>
        /// 以物料資訊查詢所在物料所在儲格資訊
        /// url: http://172.20.39.237:3003/rack/findProduct
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("rack/getEmpty")]
        public IActionResult findProduct(RackFindProductRequest request)
        {
            return Ok();
        }
        //TODO:未完成 > 下次從moveout開始
        #endregion


        #region 車載料架管理系統的API (目前場內壓測暫無使用到，後續是否使用待確認)
        #endregion

        [Route("rack/GetMacAddress")]
        [HttpPost]
        public async Task<dynamic> GetMacAddress()
        {

            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine(
                    "Name: " + nic.Name +
                    " Found MAC Address: " + nic.GetPhysicalAddress() +
                    " Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            List<string> macList = new List<string>();
            foreach (var nic in nics)
            {
                // 因為電腦中可能有很多的網卡(包含虛擬的網卡)，
                // 我只需要 Ethernet 網卡的 MAC
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    macList.Add(nic.GetPhysicalAddress().ToString());
                }
            }
            return macAddress;
        }

    }
}
