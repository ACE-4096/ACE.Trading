﻿using CryptoExchange.Net.CommonObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace ACE.Trading.Data
{
    public static class DataCache
    {
        private static Cache cache = new Cache();
        private static bool _blockChanges = false;

        public static int AutoSaveDelay = 5 * 60;

        private class Cache
        {
            [JsonProperty("DataCache")]
            internal List<SymbolData> data = new List<SymbolData>();

            [JsonIgnore]
            internal const string DATACACHE_FILENAME = "C:\\Users\\Toby\\.ace\\ACE-DATACACHE.x3";

        }

        public static void Load()
        {
            if (File.Exists(Cache.DATACACHE_FILENAME) && cache.data.Count == 0)
            {
                // read
                string json = File.ReadAllText(Cache.DATACACHE_FILENAME);

                // decrypt - Not Yet Implemented


                // deserialize
                cache = JsonConvert.DeserializeObject<Cache>(json);
            }
            else
            {
                if (cache == null)
                {
                    cache = new Cache();
                }
            }

            // Start auto saver
            //new Thread(delayedSave).Start();
        }

        public static void Save()
        {
            // serialize
            _blockChanges = true;
            Thread.Sleep(50);
            Cache tmpCache = new() { data = cache.data.ToArray().ToList() };
            string jsonString = JsonConvert.SerializeObject(tmpCache);
            _blockChanges = false;
            // encrypt - Not Yet Implemented


            // write
            File.WriteAllText(Cache.DATACACHE_FILENAME, jsonString);
        }

        private static void delayedSave()
        {
            while (true)
            {
                Thread.Sleep(AutoSaveDelay * 1000);
                Save();
            }
        }
        /// <summary>
        /// Fetches a list of all the different symbols with data stored in the dataCache
        /// </summary>
        /// <returns>List of synbols</returns>
        public static string[] getAllSymbols()
        {
            if (cache == null) return new string[] { };
            List<string> symbolList = new List<string>();
            foreach (SymbolData data in cache.data)
            {
                symbolList.Add(data.getSymbol);
            }
            return symbolList.ToArray();
        }

        /// <summary>
        /// Gets the all price data associated with that symbol
        /// </summary>
        /// <param name="symbol">Symbol to search for</param>
        /// <returns>Price history of the symbol specified</returns>
        public static SymbolData GetSymbolData(string symbol)
        {
            if (cache == null) return null;
            var sd1 = cache.data.Find(sd => sd.getSymbol == symbol);
            if (sd1 != null) sd1.getPriceHistory.Sort(DataHandling.sortTime_earliestFirst); 
            return sd1;
        }

        /// <summary>
        /// Updates price data in the dataCache
        /// </summary>
        /// <param name="symbol">Symbol to store the data for</param>
        /// <param name="price">Symbol price</param>
        /// <param name="time">Time of symbol price</param>
        public static void update(string symbol, decimal price, DateTime time)
        {
            while (_blockChanges) ;
            var data = cache.data.Find(sd => sd.getSymbol == symbol);
            if (data == null)
            {
                data = new SymbolData();
                data.getSymbol = symbol;
                PricePoint p = new PricePoint { avgPrice = price, timeUtc = time };
                data.getPriceHistory = new List<PricePoint>();
                data.priceHistory.Add(p);
                cache.data.Add(data);
            }
            else
            {
                Price p = new Price { averagePrice = price, timeUtc = time };
                data.priceHistory.Add(p);
            }
        }

        public static void add(string symbol)
        {
            while (_blockChanges) ;
            if (GetSymbolData(symbol) == null)
            {
                SymbolData sd = new SymbolData() { symbol = symbol, priceHistory = new List<PricePoint>() };
                cache.data.Add(sd);
            }
        }

        public static List<PricePoint> findAllByTime(string symbol, DateTime time)
        {
            var sd = GetSymbolData(symbol);
            return sd.priceHistory.FindAll(pd => DataHandling.findByTime(pd, time));

        }
    }
}
