using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boletos_CASD
{
	public static class ExtensionMethods
	{
		public static string RemoveAccents(this string text)
		{
			StringBuilder sbReturn = new StringBuilder();
			var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
			foreach (char letter in arrayText)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
					sbReturn.Append(letter);
			}
			return sbReturn.ToString();
		}

		public static string ReplaceSpecialSequences(this string defaultBody, string name)
		{
			return defaultBody.Replace(MainWindow.replacerWord, name);
		}
	}
}
