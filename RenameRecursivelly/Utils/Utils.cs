using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameRecursivelly.Utils
{
    public class Utils
    {
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            bool lastcharWhitespace = false;
            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
           
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    if (c.Equals(' ') || c.Equals('_'))
                    {
                        if (!lastcharWhitespace) stringBuilder.Append('_');
                        lastcharWhitespace = true;
                    }
                    else
                    {
                        lastcharWhitespace = false;
                        stringBuilder.Append(c);
                    }
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }


        public static void DirSearch(string parentDir, Queue<ItemInfo> output, bool doFiles, bool doFolders)
        {
            if (doFiles)
            {
                foreach (string f in Directory.GetFiles(parentDir))
                {
                    string filename = Path.GetFileNameWithoutExtension(f);
                    string normalizedFilename = RemoveDiacritics(filename);
                    if (filename != normalizedFilename)
                    {
                        ItemInfo item = new ItemInfo(parentDir, filename + Path.GetExtension(f), normalizedFilename, false);
                        output.Enqueue(item);
                    }
                }
            }

            foreach (string d in Directory.GetDirectories(parentDir))
            {
                DirSearch(d, output, doFiles, doFolders);
                if (doFolders) 
                {
                    string dirname = Path.GetFileName(d);
                    string normalizedDirname = RemoveDiacritics(dirname);
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
