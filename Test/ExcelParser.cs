using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Configuration;

namespace Test
{
    public class ExcelParser
    {
        public static Dictionary<string, string> ParseMails(string fileName)
        {
            using(ExcelPackage package = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["2017"];
                int initialRow = 2;
                int nameColumn = 3;
                int emailColumn = 4;
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
                    mailMap.Add(nameObj.ToString(), mailString);
                    row++;
                }
                return mailMap;               
            }
        }
    }
}
