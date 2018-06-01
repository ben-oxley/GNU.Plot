using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GNU.Plot
{
    public class GNUPlotExecutor
    {
        private static readonly string defaultTerminal = "set term png";
        private string terminal = defaultTerminal;
        public GNUPlotExecutor() { }
        public GNUPlotExecutor(int xSize, int ySize)
        {
            terminal += $"size {xSize},{ySize}";
        }

        private async Task<int> Execute(string args)
        {
            string assembly = Assembly.GetExecutingAssembly().Location;
            FileInfo assemblyInfo = new FileInfo(assembly);
            string GNUPlotPath = Path.Combine(assemblyInfo.Directory.FullName, "gnuplot", "gnuplot.exe");
            FileInfo GNUPlotFilePath = new FileInfo(GNUPlotPath);
            if (!GNUPlotFilePath.Exists) throw new Exception("Cannot find GNU Plot exe: " + GNUPlotFilePath.FullName);
            Process p = CreateProcess(args, GNUPlotPath);
            p.Start();
            string stdOut = await p.StandardOutput.ReadToEndAsync();
            string stdErr = await p.StandardError.ReadToEndAsync();
            await Task.Run(() => p.WaitForExit());
            if (p.ExitCode != 0) new ApplicationException($"GNU Plot exited with an error ({p.ExitCode}): {stdErr}");
            return p.ExitCode;
        }

        private static Process CreateProcess(string args, string GNUPlotPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = GNUPlotPath;
            p.StartInfo.Arguments = args;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            return p;
        }

        public async Task<PlotRender> ExecuteGNUPlot(string args, double[][] rows)
        {
            FileInfo scriptFile, dataFile, outputFile;
            WriteScript(args, rows, out outputFile, out scriptFile, out dataFile);
            string arguments = $"-c {scriptFile}";
            try
            {
                await Execute(arguments);
                return new PlotRender(dataFile, scriptFile, outputFile);
            }
            catch (Exception e)
            {
                return new PlotRender(dataFile, scriptFile, outputFile, e);
            }
        }

        private void WriteScript(string args, double[][] rows, out FileInfo outputFile, out FileInfo scriptFile, out FileInfo dataFile)
        {
            outputFile = new FileInfo(Path.Combine(Path.GetTempFileName() + ".png"));
            scriptFile = new FileInfo(Path.GetTempFileName() + ".script");
            string script = $@"{terminal}
                set output ""{outputFile.FullName.Replace("\\", "/")}""
                ";

            script += "plot ";
            dataFile = WriteData(rows);
            script += $"\"{dataFile.FullName.Replace("\\", "\\\\")}\"";
            script += args;

            File.WriteAllText(scriptFile.FullName, script);
        }

        private FileInfo WriteData(double[][] rows)
        {
            string data = String.Join("\n", rows.Select(r => string.Join("\t", r)));

            FileInfo dataFile = new FileInfo(Path.GetTempFileName() + ".dat");
            File.WriteAllText(dataFile.FullName, data);

            return dataFile;
        }
    }
}
