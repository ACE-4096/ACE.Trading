using ACE.Trading.Data.Collection;
using Binance.Net.Interfaces;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Data
{
    public class PricePoint : ScottPlot.IOHLC
    {

        // Price change since last 
        public decimal deltaPrice;
        // extra data
        public decimal openPrice;
        public decimal closePrice;
        public decimal highPrice;
        public decimal lowPrice;
        public decimal volume;

        // time of price point
        [Newtonsoft.Json.JsonIgnore]
        public DateTime timeUtc { get { return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeUtc).DateTime; } set { unixTimeUtc = ((DateTimeOffset)value).ToUnixTimeMilliseconds(); } }
        public long unixTimeUtc;

        private decimal _lastKnownPrice;

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal lastKnownPrice
        {
            set
            {
                if (openPrice == 0)
                    openPrice = value;
                if (lowPrice == 0)
                {
                    lowPrice = value;
                }
                else { lowPrice = lowPrice > value ? value : lowPrice; }
                if (highPrice == 0)
                {
                    highPrice = value;
                }
                else
                {
                    highPrice = highPrice < value ? value : highPrice;
                }
                closePrice = value;
                _lastKnownPrice = value;
            }
            get
            {
                return _lastKnownPrice;
            }
        }

        // average price for the price point
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal avgPrice
        {
            get { return ((highPrice + lowPrice + openPrice + closePrice) / 4.0m); }
        }

        double IOHLC.Open { get => (double)openPrice; set => _ = value; }
        double IOHLC.High { get => (double)highPrice; set => _ = value; }
        double IOHLC.Low { get => (double)lowPrice; set => _ = value; }
        double IOHLC.Close { get => (double)closePrice; set => _ = value; }
        public TimeSpan TimeSpan { get => TimeSpan.FromMinutes(1); set => _ = value; }
        public DateTime DateTime { get => timeUtc; set => _ = value; }

        public static List<PricePoint> FromBinanceKline(IEnumerable<IBinanceKline> data)
        {
            List<PricePoint> pricePoints = new List<PricePoint>();
            foreach (var x in data)
            {
                PricePoint pricePoint = new PricePoint();
                pricePoint.timeUtc = x.OpenTime;
                pricePoint.lowPrice = x.LowPrice;
                pricePoint.highPrice = x.HighPrice;
                pricePoint.closePrice = x.ClosePrice;
                pricePoint.openPrice = x.OpenPrice;
                pricePoint.volume = x.Volume;
                pricePoints.Add(pricePoint);
            }
            return pricePoints;
        }
        public async static Task<List<PricePoint>> FromBinanceApi(string symbol, DateTime start, DateTime end)
        {
            BinanceHandler handler = new BinanceHandler();
            var data = await handler.getMarketData(symbol, Binance.Net.Enums.KlineInterval.OneMinute, start, end);
            return FromBinanceKline(data.Data);
        }
        public static bool FromBinanceFile(string filename, out PricePoint[] points)
        {
            points = new PricePoint[] { };
            if (!File.Exists(filename))
                return false;

            List<PricePoint> output = new List<PricePoint>();
            string[] inputLines = File.ReadAllLines(filename);
            foreach (string line in inputLines)
            {
                string[] inputs = line.Split(',');
                if (inputs.Length < 7)
                    continue;
                PricePoint p = new PricePoint()
                {
                    timeUtc = (DateTime)DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(inputs[0])).DateTime,
                    openPrice = decimal.Parse(inputs[1]),
                    highPrice = decimal.Parse(inputs[2]),
                    lowPrice = decimal.Parse(inputs[3]),
                    closePrice = decimal.Parse(inputs[4])
                };
                p.deltaPrice = output.Count > 0 ? p.avgPrice - output.Last().avgPrice : 0;
                output.Add(p);

            }
            points = output.ToArray();
            return output.Count > 0;
        }

    }
}
