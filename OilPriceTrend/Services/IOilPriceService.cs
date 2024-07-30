using OilPriceTrend.Models;

namespace OilPriceTrend.Services
{
    public interface IOilPriceService
    {
        List<OilPrice> GetOilPrices(DateTime startDate, DateTime endDate);
    }
}
