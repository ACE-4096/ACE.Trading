using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ACE_Risk_Management_System.DataTypes
{
    public enum TradeStatus
    {
        Active,
        Inactive,
        StoppedOut,
        Cancelled
    }
    public class TradeInfo
    {
        [JsonProperty]
        public int ID { get; set; } = -1;
        [JsonProperty]
        public string Ticker { get; set; } = "";
        [JsonProperty]
        public TradeStatus Status { get; set; }

        [JsonProperty]
        public decimal TotalQty { get; set; }

        [JsonProperty]
        public decimal EntryPrice { get; set; }

        [JsonProperty]
        public DateTime EntryTime { get; set; }



        // Orders
        [JsonProperty]
        public List<OrderInfo> Orders = new List<OrderInfo>();

        // Order Helpers
        public List<OrderInfo> getOrders(OrderType type)
        {
            return Orders.FindAll(order => order.orderType == type);
        }

        public TradeInfo(string ticker)
        {
            this.Ticker = ticker;
            TotalQty = 1;
        }
    }
}
