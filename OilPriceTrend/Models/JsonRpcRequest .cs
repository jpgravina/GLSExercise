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
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
