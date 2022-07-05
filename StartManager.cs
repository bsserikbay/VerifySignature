using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VerifySignature
{
    class StartManager : IStartManager
    {
        public string DirName
        {
            get
            {
                string targetDirectory;

                Console.Write("Enter path to your folder: ");
                targetDirectory = Console.ReadLine().Trim('"');

                if (Directory.Exists(targetDirectory))
                {
                    return targetDirectory;
                }
                else
                {
                    Console.WriteLine($"Check the path {targetDirectory}");
                    return null;
                }
            }
        }
    }
}
