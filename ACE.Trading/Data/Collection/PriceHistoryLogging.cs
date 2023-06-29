using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Data.Collection
{
    public class PriceHistoryLogging
    {
        // List of threads that the class is currently using to collect data.
        private List<logLoop> loggers = new List<logLoop>();

        internal void stopLogging(string symbol = "")
        {
            if (symbol == "")
            {
                foreach (var logger in loggers)
                {
                    logger.Enabled = false;
                }
            }
            else
            {
                var logger = loggers.Find(ll => ll.getSymbol == symbol);
                if (logger != null)
                {
                    logger.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Starts logging price data for the symbol(/s) specified
        /// </summary>
        /// <param name="symbol">Symbol to log, Leave empty to log all symbols stored in the DataCache</param>
        public void startLogging(string symbol = "")
        {
            if (symbol == "")
            {
                foreach (var str in DataCache.getAllSymbols())
                {
                    startSingleSymbolLoggingLoop(str);
                }
            }
            else
            {
                startSingleSymbolLoggingLoop(symbol);
            }
        }
        private void startSingleSymbolLoggingLoop(string symbol)
        {
            logLoop ll = new logLoop();
            ll.setSymbol(symbol);

            if (loggers.Find(logger => logger.getSymbol == symbol) == null)
            {

                new Thread(new ThreadStart(ll.startLoop)).Start();

                loggers.Add(ll);
            }
        }

        private class logLoop
        {
            private string _symbol = "";
            private bool _enabled = false;

            public string getSymbol { get { return _symbol; } }

            public bool Enabled { get { return _enabled; } set { _enabled = value; } }
            public void setSymbol(string symbol)
            {
                if (_enabled == false)
                    _symbol = symbol;
            }
            public void startLoop()
            {
                if (_symbol == "")
                    return;
                Enabled = true;
                BinanceHandler BH = new BinanceHandler();
                while (Enabled)
                {
                    BH.getLatestPrice(_symbol);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
