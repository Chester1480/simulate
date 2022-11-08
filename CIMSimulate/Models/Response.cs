namespace CIMSimulate.Models
{
    public class Response
    {
        public string code { get; set; }
        public string message { get; set; }
        public dynamic Data { get; set; } 
    }

    public class CIMResponse
    {
        public string ServiceProviderResult { get; set; }
        public string rtnMessage { get; set; }
    }
}
