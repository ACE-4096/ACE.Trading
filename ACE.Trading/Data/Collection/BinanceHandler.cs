using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Net.Clients;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

namespace ACE.Trading.Data.Collection
{
    public class BinanceHandler
    {
        BinanceClient client;

        public BinanceHandler()
        {
            client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new BinanceApiCredentials("ZQ5tmZimJlRCc3AVybXEUT8oHZy6QNJdNeU0ILkjotAJypMGTrWgQVDoTvEwouSe", "nmkc0aL7rnjkTnAOtmefOk6YNrHxUow2MgZCtxhC6FgNMAAnsK95BH0Cwk6CESmZ"),
                LogLevel = LogLevel.Debug
            });
        }

        // Make Trades

        // buying
        public async Task<bool> Buy(string symbol, decimal quantity)
        {
            var response = await client.SpotApi.Trading.PlaceOrderAsync(symbol, Binance.Net.Enums.OrderSide.Buy, Binance.Net.Enums.SpotOrderType.Market, quantity);
            return response.Data.Status == OrderStatus.Filled;

        }
        public async Task<string> BuyAndCollectData(string symbol, decimal quantity)
        {
            var response = await client.SpotApi.Trading.PlaceOrderAsync(symbol, Binance.Net.Enums.OrderSide.Buy, Binance.Net.Enums.SpotOrderType.Market, quantity);
            return response.Data.OriginalClientOrderId;
        }

        // selling
        public async Task<bool> Sell(string symbol, decimal quantity)
        {
            var response = await client.SpotApi.Trading.PlaceOrderAsync(symbol, Binance.Net.Enums.OrderSide.Sell, Binance.Net.Enums.SpotOrderType.Market, quantity);
            return response.Data.Status == OrderStatus.Filled;

        }
        public async Task<string> SellAndCollectData(string symbol, decimal quantity)
        {
            var response = await client.SpotApi.Trading.PlaceOrderAsync(symbol, Binance.Net.Enums.OrderSide.Sell, Binance.Net.Enums.SpotOrderType.Market, quantity);
            return response.Data.OriginalClientOrderId;
        }

        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> getLastHour(string symbol)
        {
            return await client.SpotApi.ExchangeData.GetKlinesAsync(symbol, KlineInterval.OneMinute, DateTime.UtcNow.AddHours(-1), DateTime.UtcNow);
        }
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> getLastThreeHour(string symbol)
        {
            return await client.SpotApi.ExchangeData.GetKlinesAsync(symbol, KlineInterval.OneMinute, DateTime.UtcNow.AddHours(-3), DateTime.UtcNow);
        }
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> getMarketData(string symbol, KlineInterval interval, DateTime Start, DateTime End)
        {
            return await client.SpotApi.ExchangeData.GetKlinesAsync(symbol, interval, Start, End);
        }

        // Fetch Data

        public async Task<WebCallResult<BinanceOrder>> getOrderAsync(string symbol, string orderId)
        {
            return await client.SpotApi.Trading.GetOrderAsync(symbol, origClientOrderId: orderId);
        }

        public async void getLatestPrice(string symbol)
        {
            var result = await client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync(symbol);
            if (result.Success)
            {
                DataCache.update(symbol, result.Data.Price, DateTime.UtcNow);
            }
        }
        public async void getLastPrice(string symbol)
        {
            var spotTickerData = await client.SpotApi.ExchangeData.GetTickersAsync(new string[] { symbol });
            if (spotTickerData.Success)
            {
                foreach (var x in spotTickerData.Data) { Console.WriteLine("Response: " + x.Symbol + " : " + x.LastPrice); } // Gets last price
            }
            else
            {
                Console.WriteLine("Unsuccessful.");
            }
        }

        public async void ViewData(string[] symbols)
        {

            // Getting the order book of a symbol
            var spotOrderBookData = await client.SpotApi.ExchangeData.GetOrderBookAsync(symbols[0]);
            if (spotOrderBookData.Success)
            {
                var asks = spotOrderBookData.Data.Asks.ToArray();
                var bids = spotOrderBookData.Data.Bids.ToArray();

                for (int i = 0; i < asks.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(asks[i].Price + " @ " + asks[i].Quantity);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" : ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(bids[i].Price + " @ " + bids[i].Quantity);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            /* var spotSymbolData = await client.SpotApi.ExchangeData.GetExchangeInfoAsync(symbols);
             if (spotSymbolData.Success)
             {
                 foreach (var x in spotSymbolData.Data.Symbols)
                 {
                     Console.WriteLine("Response: " + x. + " : " + x.PriceChangePercent);

                 }
             }*/
        }
    }
}
