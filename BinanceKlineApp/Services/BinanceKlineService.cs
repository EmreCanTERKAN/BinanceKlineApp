using Binance.Net.Clients;
using Binance.Net.Enums;
using BinanceKlineApp.Dtos;
using BinanceKlineApp.Interfaces;
using CryptoExchange.Net.Authentication;
using DotNetEnv;

internal class BinanceKlineService : IKlineService
{
    public async Task<IEnumerable<KlineDto>> GetKlinesAsync(string symbol, DateTime startTime, DateTime endTime)
    {
        Env.Load(Path.Combine(AppContext.BaseDirectory, ".env"));
        var apiKey = Environment.GetEnvironmentVariable("BINANCE_API_KEY")!;
        var apiSecret = Environment.GetEnvironmentVariable("BINANCE_API_SECRET")!;

        var client = new BinanceRestClient(optionsDelegate: options =>
        {
            options.ApiCredentials = new ApiCredentials(apiKey, apiSecret);
        });

        var resultList = new List<KlineDto>();
        var fetchTime = startTime;

        while (fetchTime < endTime)
        {
            var result = await client.SpotApi.ExchangeData.GetKlinesAsync(
                symbol,
                KlineInterval.OneMinute,
                fetchTime,
                endTime,
                limit: 1000
            );

            if (!result.Success)
                throw new Exception(result.Error?.Message);

            var data = result.Data.ToList();

            if (data.Count == 0)
                break;

            resultList.AddRange(data.Select(k => new KlineDto
            {
                OpenTime = k.OpenTime,
                Open = k.OpenPrice,
                High = k.HighPrice,
                Low = k.LowPrice,
                Close = k.ClosePrice,
                Volume = k.Volume
            }));

            fetchTime = data.Last().OpenTime.AddMinutes(1);
        }

        return resultList;
    }
}
