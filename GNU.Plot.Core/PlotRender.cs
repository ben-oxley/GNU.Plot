using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNU.Plot
{
    public class PlotRender
    {
        public FileInfo Output { get; private set; }
        public FileInfo Data { get; private set; }
        public FileInfo Script { get; private set; }
        public Exception Exception { get; private set; }

        public PlotRender(FileInfo data, FileInfo script, FileInfo output, Exception exception = null)
        {
            this.Data = data;
            this.Script = script;
            this.Output = output;
            this.Exception = exception;
        }
    }
}
