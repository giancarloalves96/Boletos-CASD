using System;
using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;
using System.Configuration;

namespace Boletos_CASD
{
	public static class ExcelParser
	{
		public static Dictionary<string, string> ParseMails(string fileName)
		{
			using (ExcelPackage package = new ExcelPackage(new FileInfo(fileName)))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets[ConfigurationManager.AppSettings["sheetName"]];
				int initialRow = int.Parse(ConfigurationManager.AppSettings["initialRow"]);
				int nameColumn = int.Parse(ConfigurationManager.AppSettings["nameColumn"]);
				int emailColumn = int.Parse(ConfigurationManager.AppSettings["emailColumn"]);
				Dictionary<string, string> mailMap = new Dictionary<string, string>();

				int row = initialRow;
				object nameObj = null;
				object emailObj = null;
				while (true)
				{
					nameObj = worksheet.Cells[row, nameColumn].Value;
					emailObj = worksheet.Cells[row, emailColumn].Value;
					String mailString;
					if (nameObj == null)
						break;
					if (emailObj == null)
						mailString = null;
					else mailString = emailObj.ToString();

                    if(mailString != null)
					    mailMap.Add(nameObj.ToString(), mailString);
					row++;
				}
				return mailMap;
			}
		}
	}
}
