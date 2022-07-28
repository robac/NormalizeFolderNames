using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RenameRecursivelly.Utils
{
    public class BackgroundScanFolderArgs 
    {
        public string path;
        public int timeToProgress;

        public BackgroundScanFolderArgs(string path, int timeToProgress)
        {
            this.path = path;
            this.timeToProgress = timeToProgress;
        }
    }

    public class BackgroundScanFolderRes
    {
        public int dirCount = 0;
        public int fileCount = 0;
        public int wrongDirCount = 0;
        public int wrongFileCount = 0;
    }

    internal class BackgroundScanFolder
    {
        public BackgroundScanFolderRes DoWork(BackgroundWorker worker, BackgroundScanFolderArgs args) 
        {
            Queue<ItemInfo> folders = new Queue<ItemInfo>();
            BackgroundScanFolderRes result = new BackgroundScanFolderRes();
            folders.Enqueue(new ItemInfo(args.path, "", "", true));

            DateTime startOperation = DateTime.Now;
            while (folders.Count > 0)
            {
                //get next item
                ItemInfo currentDir = folders.Dequeue();

                //check current dir
                result.dirCount++;
                string folderName = Path.GetFileName(currentDir.path);
                string normalizedFolderName = folderName.NormalizeString();
                if (folderName != normalizedFolderName)
                { 
                    result.wrongDirCount++;
                }

                //progress
                if ((DateTime.Now - startOperation).TotalMilliseconds > args.timeToProgress)
                {
                    worker.ReportProgress(0, result);
                    startOperation = DateTime.Now;
                }

                //files
                foreach (string f in Directory.GetFiles(currentDir.path))
                {
                    result.fileCount++;
                    string filename = Path.GetFileNameWithoutExtension(f);
                    string normalizedFilename = filename.NormalizeString();
                    if (filename != normalizedFilename)
                        result.wrongFileCount++;
                }

                //folders
                foreach (string folder in Directory.GetDirectories(currentDir.path))
                {
                    folders.Enqueue(new ItemInfo(Path.Combine(currentDir.path, folder), "", "", true));
                }
#if DEBUG
                Thread.Sleep(50);
#endif
            }
            return result;
        }
    }
}
