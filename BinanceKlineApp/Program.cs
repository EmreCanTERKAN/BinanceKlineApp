using BinanceKlineApp.Aggregators;
using BinanceKlineApp.Interfaces;
using BinanceKlineApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<IKlineService, BinanceKlineService>();
        builder.Services.AddSingleton<IKlineWriter, CsvKlineWriter>();
        builder.Services.AddSingleton<KlineAggregator>();

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var aggregator = scope.ServiceProvider.GetRequiredService<KlineAggregator>();

        await aggregator.RunAsync(symbol: "SOLUSDT", startTime: DateTime.UtcNow.AddHours(-2), endTime: DateTime.UtcNow);

    }
}
