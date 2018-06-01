using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNU.Plot
{
    public class PlotBuilder
    {

        private double[][] data;
        private PlotType? type;
        private string title;
        private int[] columns;
        private int? width;
        private int? height;


        public PlotBuilder Data(double[][] data)
        {
            this.data = data;
            return this;
        }

        public PlotBuilder With(PlotType type)
        {
            this.type = type;
            return this;
        }

        public PlotBuilder Title(string title)
        {
            this.title = $"\"{title}\"";
            return this;
        }

        public PlotBuilder Size(int width, int height)
        {
            this.width = width;
            this.height = height;
            return this;
        }

        public PlotBuilder Using(int[] columns)
        {
            if (columns.Where(v => v > data.Count()).Any()) throw new ArgumentException("Column not contained in the dataset");
            this.columns = columns;
            return this;
        }

        public PlotRender Plot()
        {
            if (data == null) throw new ArgumentNullException("Data cannot be null");
            string withString = !type.HasValue ? "" : $"with {type.Value.AsGNUPlotArg()}";
            string usingString = columns == null ? "" : $"using {string.Join(":", columns)} ";
            string titleString = title == null ? "" : $"title {title} ";
            GNUPlotExecutor script = width.HasValue && height.HasValue ? new GNUPlotExecutor(width.Value,height.Value):new GNUPlotExecutor();
            PlotRender results = script.ExecuteGNUPlot($" {withString} {usingString} {titleString}", data).Result;
            return results;
        }

        
    }
}
