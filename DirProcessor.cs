using System.Collections.Generic;
using System.IO;
namespace VerifySignature
{
    class DirProcessor : IDirProcessor
    {
        readonly IStartManager _startManager;
        public DirProcessor(IStartManager startManager)
        {
            _startManager = startManager;
        }

        public List<string> GetPdfFiles()
        {
            string directory = _startManager.DirName;
            string[] fileEntries = Directory.GetFiles(directory);

            var fileNames = new List<string>();

            foreach (var fileName in fileEntries)
            {
                if (Path.GetExtension(fileName).ToUpper().Contains("PDF"))
                {
                    fileNames.Add(fileName);

                }
            }
            return fileNames;
        }

    }
}
