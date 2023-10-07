using Device.Net;
using Hid.Net.Windows;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usb.Net;
using Usb.Net.Windows;

namespace ConsoleStart
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //SimHubApiPlugin.Program.Start(9999);
            var destDir = @"C:\Program Files (x86)\SimHub\";
            var srcDirFromRider = @"C:\Users\thorb\Documents\source\SimHubApiPlugin\SimHubApiPlugin\bin\Debug\";
            var sourceDir = @"C:\Users\thorb\source\repos\SimHubApiPlugin\SimHubApiPlugin\obj\Debug\";
            var filename = "SimHubApiPlugin.dll";
            File.Copy(
                destFileName: destDir + filename,
                sourceFileName: srcDirFromRider + filename,
                overwrite: true
            );
        }
    }
}
