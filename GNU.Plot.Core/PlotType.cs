using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNU.Plot
{
    public enum PlotType
    {
        #region line point and text properties
        lines,
        dots,
        steps,
        errorbars,
        xerrorbar,
        xyerrorlines,
        points,
        impulses,
        fsteps,
        errorlines,
        xerrorlines,
        yerrorbars,
        linespoints,
        labels,
        histeps,
        financebars,
        xyerrorbars,
        yerrorlines,
        surface,
        vectors,
        parallelaxes,
        #endregion
        #region additional fill properties
        boxes,
        boxplot,
        ellipses,
        histograms,
        rgbalpha,
        boxerrorbars,
        candlesticks,
        filledcurves,
        image,
        rgbimage,
        boxxyerrorbars,
        circles,
        fillsteps,
        pm3d,
        #endregion
        #region tabular
        table
        #endregion
    }

    public static class PlotTypeExtensions{
        public static string AsGNUPlotArg(this PlotType type)
        {
            return type.ToString().ToLowerInvariant();
        }
    }
}
