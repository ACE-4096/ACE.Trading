using ACE.Trading.Data;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ACE.Trading.Data
{

    public class SymbolData
    {

        // Properties

        // Symbol 
        [JsonProperty("Symbol")]
        private string _symbol;
        public string getSymbol
        {
            get { return _symbol; }
        }


        [JsonProperty("PriceTrackerHistory")]
        public List<Price> priceTrackerHistory { get; set; }


        // Price Point History
        [JsonProperty("PricePointHistory")]
        private List<PricePoint> pricePoints;

        /// <summary>
        /// Gets the latest price
        /// </summary>
        public decimal getLatestPrice
        {
            get
            {
                if (priceHistory != null && priceHistory.Count > 0)
                {
                    priceHistory.Sort(DataHandling.sortTime_latestFirst);
                    return priceHistory.First().averagePrice;
                }
                return -1.0m; // NO DATA ERROR
            }
        }

        public PricePoint[] getPriceHistory
        {
            get { return pricePoints.ToArray(); }
        }

        // Price Point History
        private List<PricePointSlope> slopeHistory;
        public PricePointSlope[] getSlopeHistory
        {
            get { return slopeHistory.ToArray(); }
        }

        // Funcs
        public SymbolData(string symbol)
        {
            _symbol = symbol;
            pricePoints = new List<PricePoint>();
            priceTrackerHistory = new List<Price>();
        }
        public void AddPricePoint(PricePoint p)
        {
            //priceHistory.Sort(sortTime_earliestFirst);
            var p2 = priceHistory.First();

            if (p2 != null)
            {
                p.deltaPrice = p.avgPrice - p2.avgPrice;
            }
            priceHistory.Add(p);

        }
        public void calcSlopes()
        {

        }


        /// <summary>
        /// Gets the latest price
        /// </summary>
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

    }
    public struct Price
    {
        [JsonProperty("AveragePrice")]
        public decimal averagePrice { get; set; }

        [JsonProperty("PrevAveragePrice")]
        public decimal prevAveragePrice { get; set; }

        [JsonProperty("TimeUtc")]
        public DateTime timeUtc { get; set; }

        [JsonProperty("DeltaPrice")]
        public decimal? _deltaPrice { get; set; }

        /// <summary>
        /// Gets the price change since the previous average price
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal getDeltaPrice
        {
            get { if (_deltaPrice.HasValue) { return _deltaPrice.Value; } else { return prevAveragePrice - averagePrice; } }
        }
    }
}
    public class PricePoint
    {

        // Price change since last 
        public decimal deltaPrice;

        // extra data
        public decimal openPrice;
        public decimal closePrice;
        public decimal highPrice;
        public decimal lowPrice;

        // time of price point
        public DateTime timeUtc;

        // average price for the price point
        public decimal avgPrice
        {
            get { return ((highPrice + lowPrice + openPrice + closePrice) / 4.0m); }
        }
    }

    public class PricePointSlope
    {
        // average price for the slope
        private decimal _avgPrice;
        public decimal getAvgPrice
        {
            get { return _avgPrice; }
        }

        // Price change since last 
        private decimal _deltaPrice;
        public decimal getDeltaPrice
        {
            get { return _deltaPrice; }
        }

        // Linear slope variables: y = mx + c
        // gradient - ( (slope close price - slope open price ) / (number of price points in slope) )
        private decimal _slopeGradient; // m
        public decimal getGradient
        {
            get { return _slopeGradient; }
        }

        // open price for the first element in the slope
        public decimal getConstant // c
        {
            get { return _openPrice; }
        }

        // x is the index of the pricepoint

        private List<PricePoint> slopePricePoints;

        public int numOfPricePoints
        {
            get { return slopePricePoints.Count; }
        }

        // extra data
        private decimal _openPrice;
        public decimal getOpenPrice
        {
            get { return _openPrice; }
        }

        private decimal _closePrice;
        public decimal getClosePrice
        {
            get { return _closePrice; }
        }

        private decimal _highPrice;
        public decimal getHighPrice
        {
            get { return _highPrice; }
        }

        private decimal _lowPrice;
        public decimal getLowPrice
        {
            get { return _lowPrice; }
        }

        public PricePoint getLatestPoint
        {
            get { return slopePricePoints.Last(); }
        }

        // time of price point
        private DateTime _openTimeUtc;
        public DateTime getOpenTimeUtc
        {
            get { return _openTimeUtc; }
        }

        private DateTime _closeTimeUtc;
        public DateTime getCloseTimeUtc
        {
            get { return _closeTimeUtc; }
        }

        public PricePointSlope(List<PricePoint> slopePricePoints)
        {
            this.slopePricePoints = slopePricePoints;
            reCalc();
        }
        private void reCalc()
        {


            //slopePricePoints.Sort(sortTime_oldestFirst);


            //setting up vars
            var first = slopePricePoints.First();
            _openPrice = first.openPrice;
            _openTimeUtc = first.timeUtc;

            var last = slopePricePoints.Last();
            _closePrice = last.closePrice;
            _closeTimeUtc = last.timeUtc;

            // linear equation vars
            _deltaPrice = _openPrice - _closePrice;
            _slopeGradient = _deltaPrice / slopePricePoints.Count;

            // Calc avg, High and Low prices

            decimal avg = slopePricePoints[0].avgPrice, high = slopePricePoints[0].highPrice, low = slopePricePoints[0].lowPrice;

            for (int i = 1; i < slopePricePoints.Count; i++)
            {
                low = slopePricePoints[i].lowPrice < low ? slopePricePoints[i].lowPrice : low;
                high = slopePricePoints[i].highPrice > high ? slopePricePoints[i].highPrice : high;
                avg += slopePricePoints[i].avgPrice;
            }
            _highPrice = high;
            _lowPrice = low;
            _avgPrice = avg / slopePricePoints.Count;
        }

        internal PricePointSlope(PricePoint first)
        {
            slopePricePoints = new List<PricePoint>();
            slopePricePoints.Add(first);
        }
        internal void AddPoint(PricePoint point)
        {
            slopePricePoints.Add(point);
            reCalc();
        }

    }

    public class SymbolData
    {
        
    }
}