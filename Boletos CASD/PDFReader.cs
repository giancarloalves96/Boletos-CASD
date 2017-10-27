using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using System.IO;

namespace Boletos_CASD
{
    public class PDFReader
    {
        private PDFReader() { }

        public static Dictionary<string, string> GeneratePages(string inputPdfFileName, string outputDirectoryName)
        {
            using (PdfReader reader = new PdfReader(inputPdfFileName))
            {
                Dictionary<int, string> pageMap = new Dictionary<int, string>();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    pageMap.Add(i, GetNameFromPage(PdfTextExtractor.GetTextFromPage(reader, i)));
                }
                return GeneratePagesFromDictionary(reader, pageMap, outputDirectoryName);
            }
        }

        private static Dictionary<string, string> GeneratePagesFromDictionary(PdfReader reader, 
            Dictionary<int, string> pageMap, string outputDirectoryName)
        {
            Dictionary<string, string> fileMap = new Dictionary<string, string>();
            foreach (int pageNumber in pageMap.Keys)
            {
                string path = outputDirectoryName + "\\" + pageMap[pageNumber] + ".pdf";
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(path, FileMode.Create));
                document.Open();
                copy.AddPage(copy.GetImportedPage(reader, pageNumber));
                document.Close();
                fileMap.Add(pageMap[pageNumber], path);
            }
            return fileMap;
        }

        private static string GetNameFromPage(string pageText)
        {
            string[] lines = pageText.Split('\n');
            bool prox = false;
            string nameLine = null;
            foreach (string line in lines)
            {
                if (prox)
                {
                    nameLine = line;
                    break;
                }
                if (Regex.IsMatch(line, "^(\\s)*Pagador (.)*$"))
                    prox = true;
            }
            if (prox)
            {
                string[] words = nameLine.Split(' ');
                int numberOfNames = 0;
                for (int j = 0; j < words.Length; j++)
                {
                    int number;
                    if (int.TryParse(words[j], out number))
                    {
                        numberOfNames = j;
                        break;
                    }
                }

                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < numberOfNames; j++)
                {
                    builder.Append(words[j]);
                    builder.Append(" ");
                }

                return builder.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
