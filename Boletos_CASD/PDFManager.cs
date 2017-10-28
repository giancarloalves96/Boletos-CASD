using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Boletos_CASD
{
	class PDFManager
	{
		public static string LerArquivo(String filename)
		{
			string _return = "";

			using (PdfReader reader = new PdfReader(filename))
			{
				for (int i = 1; i <= reader.NumberOfPages; i++)
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

						_return += "Página " + i.ToString() + ": " + builder.ToString() + "\n";
						// Temos a página e a pessoa de cada página
						// Agora:
						// - Achar um jeito de separar as folhas
						// - Arquivar as informações em um banco de dados
					}
				}
			}
			return _return;
		}

		public static void SplitPages(string pdfFilePath, string outPath)
		{
			int interval = 100;
			int pageNameSuffix = 0;

			// Intialize a new PdfReader instance with the contents of the source Pdf file:  
			PdfReader reader = new PdfReader(pdfFilePath);

			FileInfo file = new FileInfo(pdfFilePath);
			string pdfFileName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + "-";

			PDFManager obj = new PDFManager();

			for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber += interval)
			{
				pageNameSuffix++;
				string newPdfFileName = string.Format(pdfFileName + "{0}", pageNameSuffix);
				obj.SplitAndSaveInterval(pdfFilePath, outPath, pageNumber, interval, newPdfFileName);
			}
		}

		private void SplitAndSaveInterval(string pdfFilePath, string outPath, int startPage, int interval, string pdfFileName)
		{
			using (PdfReader reader = new PdfReader(pdfFilePath))
			{
				Document document = new Document();
				PdfCopy copy = new PdfCopy(document, new FileStream(outPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
				document.Open();

				for (int pagenumber = startPage; pagenumber < (startPage + interval); pagenumber++)
				{
					if (reader.NumberOfPages >= pagenumber)
					{
						copy.AddPage(copy.GetImportedPage(reader, pagenumber));
					}
					else
					{
						break;
					}

				}

				document.Close();
			}
		}

	}
}
