//using Alpaca.Markets;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.SubAccountData;
using OpenAI_API.Models;
using ACE.Trading.Data;
using ACE.Trading.Data.Collection;
using ACE.Trading.OpenAi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Traders
{
    internal class StandardTrader 
    {

        #region Settings

        #endregion

        #region Properties
        private string _symbol { get; set; }
        private Model _model { get; set; }


        // Max spend
        private decimal _allocatedSpend { get; set; }
        internal decimal AllocatedSpend { get { return _allocatedSpend; } }

        // Max spend on low prediction accuracy
        private decimal _restrictedSpend { get; set; }

        private bool _enabled { get; set; }
        private bool Enabled { get { return _enabled; } set { _enabled = value; if (!_enabled && _holding) sellAll(new BinanceHandler()); } }
        private bool _holding { get; set; }
        private bool isHolding { get { return _holding; } }

        private decimal avgProfit = 0.0m;

        #endregion

        #region Variables
        private BinanceOrder? _buyOrder { get; set; } = null;
        private BinanceOrder? _sellOrder { get; set; } = null;
        #endregion

        internal StandardTrader(string symbol, Model model, decimal allocatedSpend)
        {
            _symbol = symbol;
            _model = model;
            _allocatedSpend = allocatedSpend;
            _restrictedSpend = 0.5m * allocatedSpend;
        }
        internal StandardTrader(string symbol, Model model, decimal allocatedSpend, decimal restrictedSpend)
        {
            _symbol = symbol;
            _model = model;
            _allocatedSpend = allocatedSpend;
            _restrictedSpend = restrictedSpend;
        }

        // Main trader funcs
        internal bool start()
        {
            if (_symbol == null || _model == null || _allocatedSpend == 0.0m)
                return false;

            new Thread(new ThreadStart(runLoop)).Start();
            return true;
        }
        private async void runLoop()
        {
            Predictions p = new Predictions();
            BinanceHandler BH = new BinanceHandler();
            while (_enabled)
            {

                //predict price based on latest info
                List<PricePoint> predictedPrices = await p.predict(_symbol, _model);


                // assess weather current predictions are accurate
                decimal avgAccuracy = (decimal)Analytics.Predictions.getAccuracyLevel(_symbol);
                decimal spend = _restrictedSpend;
                if (avgAccuracy > 75)
                {
                    spend = _allocatedSpend;
                }

                // decide weather to sell or buy
                if (isUpwardTrend(predictedPrices) && !_holding)
                {
                    // BUY
                    buyAll(BH, spend);
                }
                else if (_holding && isDownwardTrend(predictedPrices))
                {
                    // SELL
                    sellAll(BH);
                }
                Thread.Sleep(1000);
            }
        }

        // buy / sell - market
        private async void buyAll(BinanceHandler BH, decimal spend)
        {
            decimal amount = spend / DataCache.GetSymbolData(_symbol).getLatestPrice;
            string orderID = await BH.BuyAndCollectData(_symbol, amount);
            var order = await BH.getOrderAsync(_symbol, orderID);
            _buyOrder = order.Data;
            if (_buyOrder.Status == Binance.Net.Enums.OrderStatus.Filled)
            {
                _holding = true;
            }
        }
        private async void sellAll(BinanceHandler BH)
        {
            string orderID = await BH.SellAndCollectData(_symbol, _buyOrder.QuantityFilled);
            var order = await BH.getOrderAsync(_symbol, orderID);
            _sellOrder = order.Data;
            if (_sellOrder.Status == Binance.Net.Enums.OrderStatus.Filled)
            {
                _holding = false;
                calcProfit();
            }
        }

        // Helpers
        private void calcProfit()
        {
            decimal averageRevenue = _sellOrder.AverageFillPrice.Value * _sellOrder.QuantityFilled - _buyOrder.AverageFillPrice.Value * _buyOrder.QuantityFilled;
            decimal fee = averageRevenue * 0.07500m;
            decimal profit = averageRevenue - fee;
            avgProfit += profit;
        }
        private bool isDownwardTrend(List<PricePoint> predictedPrices)
        {
            predictedPrices.Sort(DataHandling.sortTime_earliestFirst);
            if (predictedPrices.First().avgPrice < predictedPrices.Last().avgPrice)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isUpwardTrend(List<PricePoint> predictedPrices)
        {
            predictedPrices.Sort(DataHandling.sortTime_earliestFirst);
            if (predictedPrices.First().avgPrice > predictedPrices.Last().avgPrice)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
