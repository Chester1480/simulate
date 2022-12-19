namespace CIMSimulate.Service.UtilS
{
    public class ConfigService
    {
        private IConfiguration _config;
        public ConfigService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 取得MCS DB ConnectionString
        /// </summary>
        /// <returns></returns>
        public string GetMCSConString()
        {
            var conString = _config["DBConnection:MCS_ConStr"];
            return conString;
        }

        /// <summary>
        ///  取得IPC DB ConnectionString
        /// </summary>
        /// <returns></returns>
        public string GetIPCConString()
        {
            var conString = _config["DBConnection:IPC_ConStr"];
            return conString;
        }
    }
}
