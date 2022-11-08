using System.Dynamic;

namespace CIMSimulate.Models
{
    /// <summary>
    /// 範例用
    /// </summary>
    public class Requeast
    {
        public string url { get; set; }
        public ExpandoObject parameters { get; set; }
        public Response response { get; set; }
    }

    public class CIMRequeast
    {
        public string funcName { get; set; }
        public string paraMessage { get; set; }
        public Setting setting { get; set; }
        public Response response { get; set; }
    }
    public class Setting
    {
        public string mode { get; set; } //0 錯誤 1 正確 2 逾時 
    }
}
