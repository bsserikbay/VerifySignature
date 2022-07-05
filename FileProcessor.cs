using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Spire.Pdf;
using Spire.Pdf.Security;
using Spire.Pdf.Widget;
using System.IO;

namespace VerifySignature
{
    class FileProcessor : IFileProcessor
    {
        readonly IDirProcessor _processor;


        public FileProcessor(IDirProcessor dirProccessor)
        {
            _processor = dirProccessor;
        }

        public Dictionary<PdfSignature, string> GetSignature()
        {
            var files = _processor.GetPdfFiles();
            Dictionary<PdfSignature, string> signatures = new Dictionary<PdfSignature, string>();

            foreach (var filename in files)
            {
                PdfDocument doc = new PdfDocument(filename);

                PdfFormWidget form = (PdfFormWidget)doc.Form;

                for (int i = 0; i < form.FieldsWidget.Count; ++i)
                {
                    if (form.FieldsWidget[i] is PdfSignatureFieldWidget field && field.Signature != null)
                    {
                        PdfSignature signature = field.Signature;
                        signatures.Add(signature, Path.GetFileName(filename));

                    }
                }
            }

            return signatures;
        }

    }
}
