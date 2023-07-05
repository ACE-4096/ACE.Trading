using ACE.Trading.Analytics.Slopes;
using ACE.Trading.OpenAi.Formatting;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Analytics.Shapes
{
    enum Pattern
    {
        None,
        ascending_line,
        head_n_shoulders,
        inverse_head_n_shoulders,
        double_top,
        double_bottom,
        triple_top,
        triple_bottom,
        falling_wedge,
        diamond,
        broadening_triangle,
        simmetrical_triangle,
        consolidation,
        flag,
        pennant,
        ascending_triangle,
        decending_triangle,
        cup_n_handle,
        rising_wedge,
        rectangle
    }

    enum Phase
    {
        start,
        middle,
        end,
        unknown,
    }

    internal class Shape
    {
        public Pattern pattern;
        public Phase phase;
        public PricePointSlope slopes;
        
        public Shape(Pattern pattern, Phase phase, PricePointSlope slopes) 
        {
            this.phase = phase; this.pattern = pattern; this.slopes = slopes;
        }
    }
}
