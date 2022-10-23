using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStart
{
    class Program
    {
        static void Main(string[] args)
        {
            //SimHubApiPlugin.Program.Start(9999);
            var destDir = @"C:\Program Files (x86)\SimHub\";
            var sourceDir = @"C:\Users\thorb\source\repos\SimHubApiPlugin\SimHubApiPlugin\obj\Debug\";
            var filename = "SimHubApiPlugin.dll";
            File.Copy(destDir + filename, sourceDir + filename, true);
        }
    }
}
