using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
