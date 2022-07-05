using System.Collections.Generic;
using Spire.Pdf.Security;

namespace VerifySignature
{
    interface IFileProcessor
    {
        Dictionary<PdfSignature, string> GetSignature();
    }
}