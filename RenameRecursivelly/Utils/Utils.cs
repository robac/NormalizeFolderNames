﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace RenameRecursivelly.Utils
{
    public static class Utils
    {
        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
           
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                        stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

        private static string RemoveWhitespace(this string text)
        {
            string pattern = @"[\s, '\-\[\].()_+']+";

            string replacement = "_";
            return Regex.Replace(text, pattern, replacement);
        }

        public static string NormalizeString(this string text)
        {
            return RemoveDiacritics(text.Trim()).RemoveWhitespace();
        }

        public static string getCsvResultFilename()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = "FolderRenameRenames.csv";

            return Path.Combine(path, filename);
        }

        public static string getLogFilename()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = "FolderRenameLog.txt";

            return Path.Combine(path, filename);
        }

    }
}
