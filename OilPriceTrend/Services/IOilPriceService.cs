using OilPriceTrend.Models;

namespace OilPriceTrend.Services
{
    public interface IOilPriceService
    {
        Task<List<OilPrice>> GetOilPricesAsync(DateTime startDate, DateTime endDate);
    }
}
