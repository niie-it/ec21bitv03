using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace MyEStore.Models
{
	public static class SlugHelper
	{
		public static string ToSlug(this string title)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				return string.Empty;
			}

			// Remove any unwanted characters and normalize the string
			title = title.ToLowerInvariant();

			title = title.Replace("đ", "d");
			title = Regex.Replace(title, @"[ôốồổỗộơớởờỡợ]", "o");

			title = title.Normalize(NormalizationForm.FormD)  // Decompose characters (like é -> e)
						 .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)  // Remove diacritic marks
						 .Aggregate(string.Empty, (current, c) => current + c);


			// Replace spaces with hyphens and remove non-alphanumeric characters
			title = Regex.Replace(title, @"[^a-z0-9\s-]", "")
						 .Trim()
						 .Replace(' ', '-');


			// Remove duplicate hyphens
			title = Regex.Replace(title, @"-+", "-");

			return title;
		}

	}
}
