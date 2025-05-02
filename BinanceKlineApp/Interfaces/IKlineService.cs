using BinanceKlineApp.Dtos;

namespace BinanceKlineApp.Interfaces;
public interface IKlineService
{
    Task<List<KlineDto>> GetKlinesAsync(string symbol, DateTime startTime, DateTime endTime);
}
