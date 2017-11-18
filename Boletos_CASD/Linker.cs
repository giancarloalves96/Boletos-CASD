using System.Collections.Generic;
using System.Linq;

namespace Boletos_CASD
{
    public static class Linker
    {
        // Do PDF and excel data integration by its output from other functions
        public static Dictionary<string, KeyValuePair<string, string>> linkPdfAndExcel(Dictionary<string, string>
            pdfOutput, Dictionary<string, string> excelOutput, out List<string> notFound, out List<string> notPresent)
        {
            Dictionary<string, KeyValuePair<string, string>> nameMap = new Dictionary<string, KeyValuePair<string, string>>();

            string[] pdfNames = pdfOutput.Keys.ToArray();
            string[] excelNames = excelOutput.Keys.ToArray();

            Dictionary<string, bool> hasPdfPage = new Dictionary<string, bool>();
            foreach (string excelName in excelNames)
            {
                hasPdfPage.Add(excelName, false);
            }

            notFound = new List<string>();
            notPresent = new List<string>();
            foreach (string pdfName in pdfNames)
            {

                bool achou = false;
                foreach (string excelName in excelNames)
                {
                    if (excelName.RemoveAccents().Contains(pdfName.Trim(' ').RemoveAccents()))
                    {
                        achou = true;
                        nameMap.Add(excelName, new KeyValuePair<string, string>(pdfOutput[pdfName], excelOutput[excelName]));
                        hasPdfPage[excelName] = true;
                        break;
                    }
                }
                if (!achou)
                {
                    notFound.Add(pdfName);
                }
            }

            foreach (string key in hasPdfPage.Keys)
            {
                if (!hasPdfPage[key])
                {
                    notPresent.Add(key);
                }
            }

            return nameMap;
        }
    }
}
