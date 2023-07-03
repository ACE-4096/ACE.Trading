using CryptoExchange.Net.CommonObjects;
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
        public static int AutoSaveDelay = 60;
        private class Cache
        {
            [JsonProperty("DataCache")]
            internal List<SymbolData> data = new List<SymbolData>();

            [JsonIgnore]
            internal const string DATACACHE_FILENAME = "C:\\Users\\Toby\\.ace\\ACE-DATACACHE.x3";

        }
        public static void Load()
        {
            if (File.Exists(Cache.DATACACHE_FILENAME) && cache?.data?.Count == 0)
            {
                // read
                string json = File.ReadAllText(Cache.DATACACHE_FILENAME);

                // decrypt - Not Yet Implemented


                // deserialize
                cache.data = new List<SymbolData>();
                cache.data.AddRange(JsonConvert.DeserializeObject<SymbolData[]>(json));
            }
            else if (File.Exists("C:\\Users\\bellramt\\ace\\ACE-DATACACHE.x3"))
            {
                string json = File.ReadAllText("C:\\Users\\bellramt\\ace\\ACE-DATACACHE.x3");
                cache.data = new List<SymbolData>();
                cache.data.AddRange(JsonConvert.DeserializeObject<SymbolData[]>(json));
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
            //Cache tmpCache = new() { data =  };
            string jsonString = JsonConvert.SerializeObject(cache.data.ToArray());
            Thread.Sleep(50);
            // write
            if (Directory.Exists(Cache.DATACACHE_FILENAME))
            {
                File.WriteAllText(Path.GetDirectoryName(Cache.DATACACHE_FILENAME), jsonString);
            }
            else
            {
                File.WriteAllText("C:\\Users\\bellramt\\ace\\ACE-DATACACHE.x3", jsonString);
            }
            // encrypt - Not Yet Implemented


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
            var data = cache.data.Find(sd => sd.getSymbol == symbol);
            if (data == null)
            {
                data = new SymbolData(symbol);
                PricePoint p = new PricePoint { lastKnownPrice = price, timeUtc = time };
                data.AddPricePoint(p);
                cache.data.Add(data);
            }
            else
            {
                var x = data.getPriceHistory.Find(point => compareTimeToMin(point.timeUtc, time));
                if (x != null)
                {
                    x.lastKnownPrice = price;
                }
                else
                {
                    PricePoint p = new PricePoint { lastKnownPrice = price, timeUtc = time };
                    data.AddPricePoint(p);
                }
            }
            Thread.Sleep(50);
            Analytics.Predictions.UpdatePredictions();
        }
        public static bool compareTimeToMin(DateTime time1, DateTime time2)
        {
            return (time1.Minute == time2.Minute && time1.Hour == time2.Hour && time1.Date == time2.Date);
        }
        public static void add(string symbol)
        {
            if (GetSymbolData(symbol) == null)
            {
                SymbolData sd = new SymbolData(symbol);
                cache.data.Add(sd);
            }
        }
        public static List<PricePoint> findAllByTime(string symbol, DateTime time)
        {
            var sd = GetSymbolData(symbol);
            return sd.getPriceHistory.FindAll(pd => DataHandling.findByTime(pd, time));

        }
        public static List<PricePoint> getAllPointsBetween(string symbol, DateTime openTime, DateTime closeTime)
        {
            var sd = GetSymbolData(symbol);
            return sd.getPriceHistory.FindAll(pd => DataHandling.AllBetween(pd, openTime, closeTime));

        }
    }
}
