using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_Risk_Management_System.DataTypes
{
    public enum OrderType
    {
        TakeProfit,
        StopLoss
    }
    public class OrderInfo
    {
        /// <summary>
        /// Order type relative to the trade as a whole, Stop-Loss or Take-Profit
        /// </summary>
        [JsonProperty]
        public OrderType orderType { get; set; } = OrderType.StopLoss;

        /// <summary>
        /// Changes between Stop and Limit order Types
        /// </summary>
        [JsonProperty]
        public bool isLimit { get; set; } = true;

        /// <summary>
        /// Limit or stop trigger price depending on the isLimit boolean
        /// </summary>
        [JsonProperty]
        public decimal TriggerPrice { get; set; }

        /// <summary>
        /// Total Quantity of this order
        /// </summary>
        [JsonProperty]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Ratio of quantity on this order vs total in the trade
        /// </summary>
        [JsonIgnore]
        public decimal QuantityRatio { get; set; } = 100;
        
        public OrderInfo(OrderType Type, bool isLimitOrder, decimal TriggerPrice, decimal Qty)
        {
            orderType = Type;
            isLimit = isLimitOrder;
            this.TriggerPrice = TriggerPrice;
            Quantity = Qty;
        }


    }
}
