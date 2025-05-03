using BinanceKlineApp.Interfaces;

namespace BinanceKlineApp.Aggregators;
internal class KlineAggregator(
    IKlineService klineService,
    IKlineWriter klineWriter)
{
    public async Task RunAsync(string symbol, DateTime startTime, DateTime endTime)
    {
        var klines = await klineService.GetKlinesAsync(symbol, startTime, endTime);
        await klineWriter.WriteAsync(klines, symbol);
    }
}
