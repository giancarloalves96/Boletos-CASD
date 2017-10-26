using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using System.IO;

namespace Test
{
    public class PDFReader
    {
        private PDFReader() { }

        public static void GeneratePages(string inputPdfFileName, string outputDirectoryName)
        {
            using (PdfReader reader = new PdfReader(inputPdfFileName))
            {
                Dictionary<int, string> pageMap = new Dictionary<int, string>();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    pageMap.Add(i, GetNameFromPage(PdfTextExtractor.GetTextFromPage(reader, i)));
                }
                GeneratePagesFromDictionary(reader, pageMap, outputDirectoryName);
            }
        }

        private static void GeneratePagesFromDictionary(PdfReader reader, Dictionary<int, string> pageMap, 
            string outputDirectoryName)
        {
            foreach (int pageNumber in pageMap.Keys)
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(outputDirectoryName + "\\" + pageMap[pageNumber] +
                    ".pdf", FileMode.Create));
                document.Open();
                copy.AddPage(copy.GetImportedPage(reader, pageNumber));
                document.Close();
            }
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
