using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.Trading.Data;

namespace ACE.Trading.Analytics.Slopes
{
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
        public List<PricePoint> getSlopePoints
        {
            get
            {
                return slopePricePoints;
            }
        }

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
}
