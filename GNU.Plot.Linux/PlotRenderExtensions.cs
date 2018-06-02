using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNU.Plot
{
    public static class PlotRenderExtensions
    {
        public static Image AsImage(this PlotRender d)
        {
            return Image.FromFile(d.Output.FullName);
        }
    }
}
