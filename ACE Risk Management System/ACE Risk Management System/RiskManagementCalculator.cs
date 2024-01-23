using ACE_Risk_Management_System.DataTypes;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_Risk_Management_System
{
    internal static class RiskManagementCalculator
    {
        /// <summary>
        /// Calculates total stopped out loss in dollars
        /// </summary>
        /// <param name="ti">TradeInfo of interest</param>
        /// <returns>Total Minimum Loss if Stopped Out</returns>
        public static decimal CalculateLoss(TradeInfo ti)
        {
            var stopOrders = ti.getOrders(OrderType.StopLoss);

            decimal loss = 0;
            foreach(var stop in stopOrders)
            {
                var lossPercentage = Math.Round(((100 / ti.EntryPrice) * Math.Abs(ti.EntryPrice - stop.TriggerPrice)) - Properties.Settings.Default.Fees, 4);
                loss += lossPercentage * ((ti.TotalQty * ti.EntryPrice) / 100);

            }
            return loss;
        }

        /// <summary>
        /// Calculates the total maximum profit from the trade of interest
        /// </summary>
        /// <param name="ti">TradeInfo of interest</param>
        /// <returns>Total Gain In Dollars</returns>
        public static decimal CalculateGain(TradeInfo ti)
        {
            var takeOrders = ti.getOrders(OrderType.TakeProfit);

            decimal gain = 0;
            foreach (var profitOrder in takeOrders)
            {
                var gainPecentage = Math.Round(((100 / ti.EntryPrice) * (Math.Abs(ti.EntryPrice - profitOrder.TriggerPrice))) - Properties.Settings.Default.Fees, 4);
                gain += gainPecentage * ((ti.TotalQty * ti.EntryPrice) / 100);
            }
            return gain;
        }

        public static decimal CalculateTargetQuantity(TradeInfo ti)
        {
            var stop = ti.getOrders(OrderType.StopLoss).First();

            return ((Properties.Settings.Default.Risk/100) * new ExchangeApi().getBalance()) / Math.Abs(ti.EntryPrice - stop.TriggerPrice);
            //return ((( / lossPercentage) * 100) / ti.EntryPrice);
        }
    }
}
