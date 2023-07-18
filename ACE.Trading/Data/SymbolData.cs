using ACE.Trading.Data;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ACE.Trading.Analytics.Slopes;
using Binance.Net.Objects.Models;
using Binance.Net.Interfaces;
using ScottPlot;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using ACE.Trading.Data.Collection;

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
    
}