using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OfficeOpenXml;
using System.Globalization;

namespace Test
{
    public static class Program
	{
        static void Main(string[] args)
        {

			//            //String pdfName = @"C:\Users\Giancarlo\Desktop\gerados para outubro.pdf";
			//            String outputDir = @"C:\Users\Giancarlo\Desktop\teste";

			//            //PDFReader.GeneratePages(filename, outputDir);

			//            //Console.WriteLine("OK");

			//            //Console.ReadLine();

			//            string excelName = @"C:\Users\Giancarlo\Desktop\teste.xlsx";
			//            Dictionary<string, string> mailMap = ExcelParser.ParseMails(excelName);

			//            string[] filenames = Directory.GetFiles(outputDir);
			//            string[] names = new string[filenames.Length];
			//            for(int i = 0; i<names.Length; i++)
			//         {
			//             string[] split = filenames[i].Split('\\');
			//             string[] split2 = split[split.Length - 1].Split('.');
			//             names[i] = split2[0].Trim(' ');
			//             names[i] = RemoveAccents(names[i]);
			//         }

			//         //mailMap.Clear();
			//         //mailMap.Add("Adam Aristeus Matos de Sá Silveira", "oi@oi.com");

			//         foreach(string name in names)
			//         {
			//             bool achou = false;
			//             foreach(string key in mailMap.Keys)
			//             {
			//                 if (RemoveAccents(key).Contains(name))
			//                 {
			//                     // Console.WriteLine("ENCONTRADO: " + name + " - " + mailMap[key]);
			//                     achou = true;
			//                     break;
			//                 }
			//             }
			//             if(!achou)
			//                 Console.WriteLine("NÃO ENCONTRADO: " + name);
			//         }

			//Console.ReadLine();
			//     }

			//     static string RemoveAccents(this string text)
			//     {
			//         StringBuilder sbReturn = new StringBuilder();
			//         var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
			//         foreach (char letter in arrayText)
			//         {
			//             if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
			//                 sbReturn.Append(letter);
			//         }
			//         return sbReturn.ToString();

		}
    }
}
