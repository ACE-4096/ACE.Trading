using ACE.Trading.Data;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ACE.Trading.Analytics.Slopes;
using Binance.Net.Objects.Models;
using Binance.Net.Interfaces;
using ScottPlot;
using System.Reflection.Metadata.Ecma335;

namespace ACE.Trading.Data
{

    public class SymbolData
    {

        // Properties

        // Symbol 
        [JsonProperty("Symbol")]
        private string _symbol;

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public string getSymbol
        {
            get { return _symbol; }
        }


        // Price Point History
        [JsonProperty("PricePointHistory")]
        private List<PricePoint> pricePoints;

        /// <summary>
        /// Gets the latest price
        /// </summary>
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getLatestPrice
        {
            get
            {
                if (pricePoints != null && pricePoints.Count > 0)
                {
                    pricePoints.Sort(DataHandling.sortTime_latestFirst);
                    return pricePoints.First().avgPrice;
                }
                return -1.0m; // NO DATA ERROR
            }
        }


        [Newtonsoft.Json.JsonIgnoreAttribute]
        public List<PricePoint> getPriceHistory
        {
            get { return pricePoints; }
        }

        // Price Point History
        [JsonProperty("SlopeHistory")]
        private List<PricePointSlope> slopeHistory = new List<PricePointSlope>();


        [Newtonsoft.Json.JsonIgnoreAttribute]
        public PricePointSlope[] getSlopeHistory
        {
            get { return slopeHistory == null ? null : slopeHistory.ToArray(); }
        }

        // Funcs
        public SymbolData(string symbol)
        {
            _symbol = symbol;
            pricePoints = new List<PricePoint>();
        }
        public void AddPricePoint(PricePoint p)
        {
            //priceHistory.Sort(sortTime_earliestFirst);
            if (pricePoints.Count == 0)
            {
                p.deltaPrice = 0;
            }
            else
            {
                var p2 = pricePoints.First();
                if (p2 != null)
                {
                    p.deltaPrice = p.avgPrice - p2.avgPrice;
                }
            }
            pricePoints.Add(p);
        }
        public void calcSlopes()
        {

        }


    }
    public class PricePoint : ScottPlot.IOHLC
    {

        // Price change since last 
        public decimal deltaPrice;
        // extra data
        public decimal openPrice;
        public decimal closePrice;
        public decimal highPrice;
        public decimal lowPrice;

        // time of price point
        [Newtonsoft.Json.JsonIgnore]
        public DateTime timeUtc { get { return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeUtc).DateTime;  } set { unixTimeUtc = ((DateTimeOffset)value).ToUnixTimeMilliseconds(); } }
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
        public DateTime DateTime { get=>timeUtc; set => _ = value; }

        public static List<PricePoint> FromBinanceKline(IEnumerable<IBinanceKline> data) 
        {
            List<PricePoint> pricePoints = new List<PricePoint>();
            foreach(var x in data)
            {
                PricePoint pricePoint = new PricePoint();
                pricePoint.timeUtc = x.OpenTime;
                pricePoint.lowPrice = x.LowPrice;
                pricePoint.highPrice = x.HighPrice;
                pricePoint.closePrice = x.ClosePrice;
                pricePoint.openPrice = x.OpenPrice;
                pricePoints.Add(pricePoint);
            }
            return pricePoints;
        }

    }


}