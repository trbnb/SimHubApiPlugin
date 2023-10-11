using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy
{
    internal static class Program
    {
        private const string SimHubDir = @"C:\Program Files (x86)\SimHub\";
        private const string SimHubExe = "SimHubWPF";

        public static async Task Main()
        {
            StopSimHub();
            await Task.Delay(500);
            const string srcDirFromRider = @"C:\Users\thorb\Documents\source\SimHubApiPlugin\SimHubApiPlugin\bin\Debug\";
            //var sourceDir = @"C:\Users\thorb\source\repos\SimHubApiPlugin\SimHubApiPlugin\obj\Debug\";
            const string filename = "SimHubApiPlugin.dll";
            File.Copy(
                destFileName: SimHubDir + filename,
                sourceFileName: srcDirFromRider + filename,
                overwrite: true
            );
            StartSimHub();
            await Task.Delay(1000);
            StartSimHub();
        }

        private static void StopSimHub()
        {
            var processes = Process.GetProcesses();
            var simHubProcess = processes.FirstOrDefault(it => it.ProcessName == SimHubExe);
            simHubProcess?.Kill();
        }

        private static void StartSimHub()
        {
            Process.Start(SimHubDir + SimHubExe + ".exe");
        }
    }
}