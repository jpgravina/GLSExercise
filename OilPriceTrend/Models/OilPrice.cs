namespace OilPriceTrend.Models
{
    public class OilPriceResponse
    {
        public string JsonRpc { get; set; }
        public int Id { get; set; }
        public OilPriceResult Result { get; set; }
    }

    public class OilPriceResult
    {
        public List<OilPrice> Prices { get; set; }
    }

    public class OilPrice
    {
        public string DateISO8601 { get; set; }
        public decimal Price { get; set; }
    }

}
