using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;

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

        private static string RemoveWhitespace(string text)
        {
            string pattern = @"[\s,'_']+";
            string replacement = "_";
            return Regex.Replace(text, pattern, replacement);
        }

        public static string NormalizeString(this string text)
        {
            return RemoveWhitespace(RemoveDiacritics(text.Trim()));
        }

        public static void DirSearch(string parentDir, Queue<ItemInfo> output, bool doFiles, bool doFolders, int maxItems)
        {
            if (doFiles)
            {
                foreach (string f in Directory.GetFiles(parentDir))
                {
                    if (maxItems <= output.Count) return;

                    string filename = Path.GetFileNameWithoutExtension(f);
                    string normalizedFilename = filename.NormalizeString();
                    if (filename != normalizedFilename)
                    {
                        ItemInfo item = new ItemInfo(parentDir, filename + Path.GetExtension(f), normalizedFilename, false);
                        output.Enqueue(item);
                    }
                }
            }

            foreach (string d in Directory.GetDirectories(parentDir))
            {
                if (maxItems <= output.Count) return;

                DirSearch(d, output, doFiles, doFolders, maxItems);
                if (maxItems <= output.Count) return;
                if (doFolders) 
                {
                    string dirname = Path.GetFileName(d);
                    string normalizedDirname = dirname.NormalizeString();
                    if (dirname != normalizedDirname)
                    {
                        ItemInfo item = new ItemInfo(parentDir, dirname, normalizedDirname, true);
                        output.Enqueue(item);
                    }
                }
            }

        }

        public static string getLogFilename()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = String.Format("FolderRenameLog.csv");

            return Path.Combine(path, filename);
        }
    }
}
