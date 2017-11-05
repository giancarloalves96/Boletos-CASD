using System.Collections.Generic;
using System.Linq;

namespace Boletos_CASD
{
	public static class Linker
	{
		// Do PDF and excel data integration by its output from other functions
		public static Dictionary<string, KeyValuePair<string, string>> linkPdfAndExcel(Dictionary<string, string>
			pdfOutput, Dictionary<string, string> excelOutput, out List<string> notFound)
		{
			Dictionary<string, KeyValuePair<string, string>> nameMap = new Dictionary<string, KeyValuePair<string, string>>();

			string[] pdfNames = pdfOutput.Keys.ToArray();
			string[] excelNames = excelOutput.Keys.ToArray();
			
			notFound = new List<string>();
			foreach (string pdfName in pdfNames)
			{
				
				bool achou = false;
				foreach (string excelName in excelNames)
				{
					if (excelName.RemoveAccents().Contains(pdfName.Trim(' ').RemoveAccents()))
					{
						
						achou = true;
						nameMap.Add(excelName, new KeyValuePair<string, string>(pdfOutput[pdfName], excelOutput[excelName]));
						break;
					}
				}
				if (!achou)
				{
					notFound.Add(pdfName);
				}
			}

			return nameMap;
		}
	}
}
