using BinanceKlineApp.Dtos;
using BinanceKlineApp.Interfaces;
using System.Globalization;
using System.Text;

namespace BinanceKlineApp.Services;
public class CsvKlineWriter : IKlineWriter
{
    private readonly string _basePath = "Output";

    public async Task WriteAsync(IEnumerable<KlineDto> klines, string symbol)
    {
        var filePath = Path.Combine(_basePath, $"{symbol}_klines.csv");
        Directory.CreateDirectory(_basePath);

        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("OpenTime,Open,High,Low,Close,Volume");

        foreach (var kline in klines)
        {
            stringBuilder.AppendLine(string.Join(",",
                kline.OpenTime.ToString("o"),
                kline.Open.ToString(CultureInfo.InvariantCulture),
                kline.High.ToString(CultureInfo.InvariantCulture),
                kline.Low.ToString(CultureInfo.InvariantCulture),
                kline.Close.ToString(CultureInfo.InvariantCulture),
                kline.Volume.ToString(CultureInfo.InvariantCulture)));
        }

        await File.WriteAllTextAsync(filePath, stringBuilder.ToString());
    }
}
