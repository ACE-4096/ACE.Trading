using ACE.Trading.Data;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ACE.Trading.Analytics.Slopes;

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
                if (pricePoints != null && pricePoints.Count > 0)
                {
                    pricePoints.Sort(DataHandling.sortTime_latestFirst);
                    return pricePoints.First().avgPrice;
                }
                return -1.0m; // NO DATA ERROR
            }
        }

        public List<PricePoint> getPriceHistory
        {
            get { return pricePoints; }
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
        }
        public void AddPricePoint(PricePoint p)
        {
            //priceHistory.Sort(sortTime_earliestFirst);
            var p2 = pricePoints.First();

            if (p2 != null)
            {
                p.deltaPrice = p.avgPrice - p2.avgPrice;
            }
            pricePoints.Add(p);

        }
        public void calcSlopes()
        {

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

        public decimal lastKnownPrice;

        // average price for the price point
        public decimal avgPrice
        {
            get { return ((highPrice + lowPrice + openPrice + closePrice) / 4.0m); }
        }
    }


}