using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            String filename = @"C:\Users\Giancarlo\Desktop\gerados para outubro.pdf";
            String outputDir = @"C:\Users\Giancarlo\Desktop\teste";

            PDFReader.GeneratePages(filename, outputDir);

            Console.WriteLine("OK");

            Console.ReadLine();
        }
    }
}
