using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Okex.Net;

namespace ACE_Risk_Management_System
{
    internal class ExchangeApi
    {
        private string API_KEY = "4b2e4e2c-4296-4ff7-b714-ffe360d60e49";
        private string API_SECRET = "24849462222F599EFE7CB187CF7D56AE";
        private string API_PASSPHRASE = "FuckYouAndYourMumA-2";


        private OkexClient client;

        public event UpdateBalanceHandler UpdateBalance;
        public delegate void UpdateBalanceHandler(decimal value);

        public ExchangeApi()
        {
            client = new OkexClient();
            client.SetApiCredentials(API_KEY, API_SECRET, API_PASSPHRASE);
        }

        // Get balance of assets in nzd
        public decimal getBalance()
        {
            return client.GetAccountBalance().Data.TotalEquity;
        }

        // Make event auto update balance data
        public void SubscribeToBalanceUpdates(bool unsubscribe = false)
        {
            var sock = new OkexSocketClient();
            sock.SetApiCredentials(API_KEY, API_SECRET, API_PASSPHRASE);
            sock.SubscribeToBalanceAndPositionUpdates((data) =>
             {
                 if (data != null)
                 {
                     UpdateBalance(data.AdjustedEquity == null? -1: (decimal)data.AdjustedEquity);
                 }
             });
        }
        /// Checks the status of an order given the id
        /// </summary>
        /// <param name="OrderId">Exchange Id of the order</param>
        /// <returns>true if the order is active, false if inactive</returns>
        public bool CheckOrderStatus(long OrderId)
        {

            var positions = client.GetAccountPositions(Okex.Net.Enums.OkexInstrumentType.Margin);
            var order = positions.Data.ToList().Find((pos) => pos.TradeId == OrderId);
            if (order != null)
            {
                return true;
            }
            return false;
        }

        public List<string> getTickers()
        {
            var list = new List<string>();
            var data = client.GetTickers(Okex.Net.Enums.OkexInstrumentType.Any);
            if (data.Error != null)
            {
                return new List<string>();
            }
            foreach (var item in data.Data)
            {
                list.Add(item.Instrument);
            }
            return list;
        }
    }
}
