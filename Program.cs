using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace VerifySignature
{
    class Program
    {


        static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDirProcessor, DirProcessor>()
                .AddSingleton<IFileProcessor, FileProcessor>()
                .AddSingleton<IExportManager, ExportManager>()
                .AddSingleton<IStartManager, StartManager>()
                .BuildServiceProvider();

            var manager = serviceProvider.GetService<IExportManager>();
            manager.CreateExcel();
        }
    }
}
