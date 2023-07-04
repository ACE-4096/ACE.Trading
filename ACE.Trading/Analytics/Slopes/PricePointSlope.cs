using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ACE.Trading.Data;
using ACE.Trading.OpenAi.Formatting;

namespace ACE.Trading.Analytics.Slopes
{
    public class PricePointSlope
    {
        // average price for the slope
        private decimal _avgPrice;
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getAvgPrice
        {
            get { return _avgPrice; }
        }

        // Price change since last 
        private decimal _deltaPrice;
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getDeltaPrice
        {
            get { return _deltaPrice; }
        }

        // Linear slope variables: y = mx + c
        // gradient - ( (slope close price - slope open price ) / (number of price points in slope) )
        private decimal _slopeGradient; // m
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getGradient
        {
            get { return _slopeGradient; }
        }

        // open price for the first element in the slope
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getConstant // c
        {
            get { return _openPrice; }
        }

        // x is the index of the pricepoint

        private List<PricePoint> slopePricePoints;
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public List<PricePoint> getSlopePoints
        {
            get
            {
                return slopePricePoints;
            }
        }

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public int numOfPricePoints
        {
            get { return slopePricePoints.Count; }
        }

        // extra data
        private decimal _openPrice;

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getOpenPrice
        {
            get { return _openPrice; }
        }

        private decimal _closePrice;

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getClosePrice
        {
            get { return _closePrice; }
        }

        private decimal _highPrice;
        
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getHighPrice
        {
            get { return _highPrice; }
        }

        private decimal _lowPrice;
        
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getLowPrice
        {
            get { return _lowPrice; }
        }

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public PricePoint getLatestPoint
        {
            get { return slopePricePoints.Last(); }
        }

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public decimal getVolume
        {
            get { return volume; }
        }

        private decimal volume;

        // time of price point
        private long _openTimeUtc;
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public DateTime openTimeUtc { get { return DateTimeOffset.FromUnixTimeMilliseconds(_openTimeUtc).DateTime; } set { _openTimeUtc = ((DateTimeOffset)value).ToUnixTimeMilliseconds(); } }
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public long OpenTimeUnix { get { return _openTimeUtc; } }
        private long _closeTimeUtc;
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public DateTime closeTimeUtc
        { get { return DateTimeOffset.FromUnixTimeMilliseconds(_closeTimeUtc).DateTime; } set { _closeTimeUtc = ((DateTimeOffset)value).ToUnixTimeMilliseconds(); } }
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public long CloseTimeUnix { get { return _closeTimeUtc; } }
        public PricePointSlope(List<PricePoint> slopePricePoints)
        {
            this.slopePricePoints = slopePricePoints;
            reCalc();
        }
        private void reCalc()
        {


            //slopePricePoints.Sort(sortTime_oldestFirst);
            if (slopePricePoints == null) return;

            //setting up vars
            var first = slopePricePoints.First();
            _openPrice = first.openPrice;
            openTimeUtc = first.timeUtc;

            var last = slopePricePoints.Last();
            _closePrice = last.closePrice;
            closeTimeUtc = last.timeUtc;

            // linear equation vars
            _deltaPrice = _openPrice - _closePrice;
            _slopeGradient = _deltaPrice / slopePricePoints.Count;

            // Calc avg, High and Low prices

            decimal avg = slopePricePoints[0].avgPrice, high = slopePricePoints[0].highPrice, low = slopePricePoints[0].lowPrice;
            decimal volume = slopePricePoints[0].volume;
            for (int i = 1; i < slopePricePoints.Count; i++)
            {
                low = slopePricePoints[i].lowPrice < low ? slopePricePoints[i].lowPrice : low;
                high = slopePricePoints[i].highPrice > high ? slopePricePoints[i].highPrice : high;
                avg += slopePricePoints[i].avgPrice;
                volume+= slopePricePoints[i].volume;
            }
            _highPrice = high;
            _lowPrice = low;
            _avgPrice = avg / slopePricePoints.Count;
        }

        private long getAvgVolume()
        {

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

        public string ToString(int SlopeNum)
        {
            return string.Format(MinLanguage.Encoding.slopeFormat, SlopeNum, OpenTimeUnix, _openPrice, CloseTimeUnix, _closePrice, _slopeGradient, volume  );
        }
    }
}
