

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Spire.Pdf.Security;
using Spire.Xls;

namespace VerifySignature
{
    class ExportManager : IExportManager
    {
        readonly IFileProcessor _fileProcessor;
        public ExportManager(IFileProcessor fileProcessor)
        {
            _fileProcessor = fileProcessor;

        }

        public void CreateExcel()
        {

            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];

            sheet.Range["A1"].Text = $"File name";
            sheet.Range["B1"].Text = $"Signed date";
            sheet.Range["C1"].Text = $"Signature verify";
            sheet.Range["D1"].Text = $"Valid from";
            sheet.Range["E1"].Text = $"Valid to";
            sheet.Range["F1"].Text = $"Version";
            sheet.Range["G1"].Text = $"Issuer";
            sheet.Range["H1"].Text = $"Subject";
            sheet.Range["I1"].Text = $"Signature algorythm";

            var signatures = _fileProcessor.GetSignature();
            if (signatures == null) return;

            try
            {
                int count = 2;

                foreach (var item in signatures)
                {
                    string fileName = item.Value;
                    PdfSignature signature = item.Key;
                    bool isValid = signature.VerifySignature();
                    DateTime signedDate = signature.Date;

                    X509Certificate2 certificate = signature.Certificate as X509Certificate2;
                    DateTime validStart = certificate.NotBefore;
                    DateTime validEnd = certificate.NotAfter;
                    Oid algorythm = certificate.SignatureAlgorithm;

                    int version = certificate.Version;
                    string subject = certificate.Subject;
                    string issuer = certificate.Issuer;


                    sheet.Range[$"A{count}"].Text = $"{fileName}";
                    sheet.Range[$"B{count}"].Text = $"{signedDate}";
                    sheet.Range[$"C{count}"].Text = $"{isValid}";
                    sheet.Range[$"D{count}"].Text = $"{validStart}";
                    sheet.Range[$"E{count}"].Text = $"{validEnd}";
                    sheet.Range[$"F{count}"].Text = $"{version}";
                    sheet.Range[$"G{count}"].Text = $"{issuer}";
                    sheet.Range[$"H{count}"].Text = $"{subject}";
                    sheet.Range[$"I{count}"].Text = $"{algorythm}";

                    count++;
                }
                sheet.AllocatedRange.AutoFitColumns();
                sheet.AllocatedRange.AutoFitRows();
                workbook.SaveToFile("Sample.xls");

                var p = new Process
                {
                    StartInfo = new ProcessStartInfo(workbook.FileName)
                    {
                        UseShellExecute = true
                    }
                };
                p.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
