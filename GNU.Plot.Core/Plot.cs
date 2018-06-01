using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNU.Plot
{
    public class Plot
    {
        public static PlotBuilder Data(double[][] data)
        {
            PlotBuilder plot = new PlotBuilder();
            plot.Data(data);
            return plot;
        }

        public static PlotBuilder With(PlotType type)
        {
            PlotBuilder plot = new PlotBuilder();
            plot.With(type);
            return plot;
        }

    }
}
