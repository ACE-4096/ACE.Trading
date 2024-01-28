using ACE_Risk_Management_System.DataTypes;
using ACE_Risk_Management_System.UserControls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_Risk_Management_System
{
    internal class TradeManager: IDisposable
    {
        private const string TradesFileLocation = "C:\\Users\\Toby\\OneDrive\\Documents\\New FoIder";
        private List<TradeInfo> trades = new List<TradeInfo>();
        private Panel orderPanel, tradesPanel;

        private ExchangeApi Api;

        private int currentTrade = -1;
        public event UpdateCalcsHandler OnCalcsUpdated;
        public delegate void UpdateCalcsHandler(string lossText, string gainText, string qtyText);

        public event PriceUpdateHandler OnEntryPriceUpdated;
        public delegate void PriceUpdateHandler (decimal newPrice);

        public TradeManager(Panel orderPanel, Panel tradesPanel/*, SecureKey key (Encryption key)*/) 
        {
            Api = new ExchangeApi();
            this.orderPanel = orderPanel;
            this.tradesPanel = tradesPanel;
            Load();
        }


        // Orders and Drawing
        public void createOrder(OrderType type, decimal triggerPrice, decimal quantity)
        {
            if (quantity == -1)
            {
                quantity = trades[currentTrade].TotalQty;
            }
            var oi = new OrderInfo(type, true, triggerPrice, quantity);
            //oi.QuantityRatio = (100 / trades[currentTrade].TotalQty) * oi.Quantity;
            oi.Quantity = (trades[currentTrade].TotalQty / 100) * oi.QuantityRatio;
            trades[currentTrade].Orders.Add(oi);
            reDrawOrders();
        }
        public void reDrawOrders()
        {
            orderPanel.Controls.Clear();
            var slOrders = trades[currentTrade].getOrders(OrderType.StopLoss);
            for (int i = 0; i < slOrders.Count; i++)
            {
                var ctl = new OrderControl($"Stop-Loss {i + 1}:", slOrders[i].TriggerPrice,
                    slOrders[i].QuantityRatio, 0);
                ctl.OnDataChanged += Ctl_OnDataChanged;
                ctl.LabelColour = Color.Red;
                orderPanel.Controls.Add(ctl);
                orderPanel.Controls[orderPanel.Controls.Count - 1].Location = new Point(3, 3 + (27 * (orderPanel.Controls.Count - 1)));
            }
            var tpOrders = trades[currentTrade].getOrders(OrderType.TakeProfit);
            for (int i = 0; i < tpOrders.Count; i++)
            {
                var ctl = new OrderControl($"Take-Profit {i + 1}:", tpOrders[i].TriggerPrice,
                    tpOrders[i].QuantityRatio, tpOrders[i].Quantity);
                ctl.OnDataChanged += Ctl_OnDataChanged;
                ctl.LabelColour = Color.Green;
                orderPanel.Controls.Add(ctl);
                orderPanel.Controls[orderPanel.Controls.Count - 1].Location = new Point(3, 3 + (27 * (orderPanel.Controls.Count - 1)));
            }
        }
        private void Ctl_OnDataChanged(OrderControl oc)
        {

            trades[currentTrade].Orders[orderPanel.Controls.IndexOf(oc)].TriggerPrice = oc.Limit;
            trades[currentTrade].Orders[orderPanel.Controls.IndexOf(oc)].QuantityRatio = oc.Percentage;
            trades[currentTrade].Orders[orderPanel.Controls.IndexOf(oc)].Quantity = (trades[currentTrade].TotalQty / 100) * oc.Percentage;
            oc.Value = RiskManagementCalculator.CalculateGain(trades[currentTrade], orderPanel.Controls.IndexOf(oc));
            trades[currentTrade].TotalQty = RiskManagementCalculator.CalculateTargetQuantity(trades[currentTrade]);

            var loss = RiskManagementCalculator.CalculateLoss(trades[currentTrade]);
            var gain = RiskManagementCalculator.CalculateGain(trades[currentTrade]);
            var bal = Api.getBalance();

            OnCalcsUpdated($"${Math.Round(loss, 4)} ({Math.Round((100 / bal) * loss, 4)}%)",
                $"${Math.Round(gain, 4)} ({Math.Round((100 / bal) * gain, 4)}%)",
                trades[currentTrade].TotalQty.ToString());
        }

        // Trades

        public TradeInfo? createTrade(string Ticker)
        {
            TradeInfo ti = new TradeInfo(Ticker);
            ti.ID = trades.Count;
            if (trades.FindAll(x => x.ID == ti.ID).Count == 0)
            {
                trades.Add(ti);
                return ti;
            }
            return null; // trade allready exists
        }

        public void Add(TradeInfo ti)
        {
            ti.Status = TradeStatus.Inactive;
            trades.Add(ti);
            Save();
        }
        public (bool success, TradeInfo ti) Remove(int tradeId)
        {
            var tmp = trades.Find(t => t.ID == tradeId);
            return (trades.Remove(tmp), tmp);

        }

        public TradeInfo? getTrade(int id = -1)
        {
            id = (id == -1 ? currentTrade : id);
            return trades.Find((ti) => ti.ID == id);
        }

        public bool UpdateTrade(TradeInfo ti)
        {
            try
            {
                int index = trades.FindIndex((t) => t.ID == ti.ID);
                trades.RemoveAt(index);
                trades.Insert(index, ti);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Someone else is accessing your trades?... Error Message: " + ex.Message);
                return false;
            }

        }
        public bool RemoveTp()
        {
            if (trades[currentTrade].Orders.Count > 2)
            {
                trades[currentTrade].Orders.RemoveAt(trades[currentTrade].Orders.Count - 1);
                reDrawOrders();
                return true;
            }
            return false;
        }
        public void UpdateFromExchangeData()
        {
           foreach (var trade in trades)
            {
                if (trade.Status != TradeStatus.Active)
                    continue;
                foreach(var order in trade.Orders)
                {
                    if (order.OrderId == -1) 
                        continue;
                    Api.CheckOrderStatus(order.OrderId);
                }
            }
        }

        // Data Storage
        private void Load()
        {
            // Encryption
            try
            {
                if (!File.Exists(TradesFileLocation))
                {
                    trades = new List<TradeInfo>();
                    currentTrade = trades.Count;
                    return;
                }
                trades = JsonConvert.DeserializeObject<List<TradeInfo>>(File.ReadAllText(TradesFileLocation));
                trades = (trades == null ? trades = new List<TradeInfo>() : trades);
                currentTrade = trades.Count;
                UpdateFromExchangeData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Someone else is accessing your trades?... Error Message: " + ex.Message); 
            }
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(TradesFileLocation, JsonConvert.SerializeObject(trades));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Someone else is accessing your trades?... Error Message: " + ex.Message);
            }
        }
        public void Dispose()
        {
            Save();
            trades.Clear();
        }

        internal void createAndShowTrade(string text)
        {
            var trade = createTrade(text);
            if (OnEntryPriceUpdated != null)
            {
                OnEntryPriceUpdated(trade.EntryPrice);
            }
            reDrawOrders();
        }
    }
}
