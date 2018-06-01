using System;
using System.Drawing;
using GNU.Plot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GNU.Plot.Tests
{
    [TestClass]
    public class SinglePlotTests
    {

        private static readonly double[][] sampleData2D = new double[][] { new double[] { 1, 1 }, new double[] { 2, 2 } };
        [TestMethod]
        public void TestSimplePlot()
        {
            double[][] data = sampleData2D;
            PlotBuilder builder = Plot.Data(data);
            builder.With(PlotType.lines);
            PlotRender render = builder.Plot();
            Assert.IsNotNull(render);
            Assert.IsNull(render.Exception);
            Assert.IsTrue(render.Output.Exists);
        }

        [TestMethod]
        public void TestVectorPlot()
        {
            double[][] data = new double[][] { new double[] { 1, 1, 0.5, 0.5 }, new double[] { 3, 3, -0.5, -0.5 } };
            PlotBuilder builder = Plot.Data(data);
            builder.With(PlotType.vectors);
            PlotRender render = builder.Plot();
            Assert.IsNotNull(render);
            Assert.IsNull(render.Exception);
            Assert.IsTrue(render.Output.Exists);
        }

        [TestMethod]
        public void TestCastingResultsToImage()
        {
            double[][] data = new double[][] { new double[] { 1, 1, 0.5, 0.5 }, new double[] { 3, 3, -0.5, -0.5 } };

            Image plotResult = Plot.Data(data).With(PlotType.vectors).Plot().AsImage();

            Assert.IsNotNull(plotResult);
        }

        [TestMethod]
        public void TestWithSetting()
        {
            double[][] data = sampleData2D;
            PlotBuilder builder = Plot.Data(data);
            builder.With(PlotType.lines);
            PlotRender render = builder.Plot();
            Assert.IsNotNull(render);
            Assert.IsNull(render.Exception);
            Assert.IsTrue(render.Output.Exists);
        }

        [TestMethod]
        public void TestTitleSetting()
        {
            double[][] data = sampleData2D;
            PlotBuilder builder = Plot.Data(data);
            builder.Title("Plot lines");
            PlotRender render = builder.Plot();
            Assert.IsNotNull(render);
            Assert.IsNull(render.Exception);
            Assert.IsTrue(render.Output.Exists);
        }

        [TestMethod]
        public void TestUsingSetting()
        {
            double[][] data = sampleData2D;
            PlotBuilder builder = Plot.Data(data);
            builder.Using(new int[] { 1, 2 });
            PlotRender render = builder.Plot();
            Assert.IsNotNull(render);
            Assert.IsNull(render.Exception);
            Assert.IsTrue(render.Output.Exists);
        }
    }
}
