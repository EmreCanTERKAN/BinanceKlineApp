using BinanceKlineApp.Dtos;

namespace BinanceKlineApp.Interfaces;
public interface IKlineWriter
{
    Task WriteAsync(IEnumerable<KlineDto> klines, string symbol);
}
