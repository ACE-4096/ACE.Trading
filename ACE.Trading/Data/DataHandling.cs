
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Data
{
    internal static class DataHandling
    {
        internal static int sortTime_earliestFirst(PricePoint x, PricePoint y)
        {
            if (x.timeUtc > y.timeUtc)
                return -1;
            if (x.timeUtc < y.timeUtc)
                return +1;
            return 0;
        }
        internal static int sortTime_latestFirst(PricePoint x, PricePoint y)
        {
            if (x.timeUtc > y.timeUtc)
                return -1;
            if (x.timeUtc < y.timeUtc)
                return +1;
            return 0;
        }
        internal static bool AllBetween(PricePoint x, DateTime openTime, DateTime closeTime)
        {
            return (x.timeUtc > openTime && x.timeUtc < closeTime);
        }
        internal static bool findByTime(PricePoint x, DateTime time)
        {
            return (x.timeUtc.Date == time.Date && x.timeUtc.Hour == time.Hour && x.timeUtc.Minute == time.Minute);
        }

    }
}
