using System;
using System.Collections.Generic;
using System.Linq;
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
            using (PdfReader reader = new PdfReader(filename))
            {
                for(int i = 1; i<=reader.NumberOfPages; i++)
                {
                    string text = PdfTextExtractor.GetTextFromPage(reader, i);
                    string[] lines = text.Split('\n');
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

                        Console.WriteLine("Página " + i.ToString() + ": " + builder.ToString());
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
