namespace OilPriceTrend.Models
{
    public class JsonRpcRequest
    {
        public int Id { get; set; }
        public string Jsonrpc { get; set; }
        public string Method { get; set; }
        public Params Params { get; set; }
    }

    public class Params
    {
        public string StartDateISO8601 { get; set; }
        public string EndDateISO8601 { get; set; }
    }
}
