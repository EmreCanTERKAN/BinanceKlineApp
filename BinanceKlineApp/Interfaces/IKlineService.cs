using BinanceKlineApp.Dtos;

namespace BinanceKlineApp.Interfaces;
public interface IKlineService
{
    Task<IEnumerable<KlineDto>> GetKlinesAsync(string symbol, DateTime startTime, DateTime endTime);
}
