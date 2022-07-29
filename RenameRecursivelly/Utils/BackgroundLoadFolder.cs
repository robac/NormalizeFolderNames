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
    public class BackgroundLoadFolderArgs
    {
        public string path;
        public int timeToProgress;
        public int maxItems;
        public bool loadFiles;
        public bool loadFolders;

        public BackgroundLoadFolderArgs(string path, int timeToProgress, bool loadFiles, bool loadFolders, int maxItems)
        {
            this.path = path;
            this.timeToProgress = timeToProgress;
            this.loadFiles = loadFiles;
            this.loadFolders = loadFolders;
            this.maxItems = maxItems;
        }
    }

    public class BackgroundLoadFolderRes
    {
        public Queue<ItemInfo> itemsToRename;

        public BackgroundLoadFolderRes(Queue<ItemInfo> itemsToRename)
        {
            this.itemsToRename = itemsToRename;
        }
    }

    internal class BackgroundLoadFolder
    {
        DateTime startOperation;
        BackgroundWorker worker;
        BackgroundLoadFolderArgs args;

        public BackgroundLoadFolderRes DoWork(BackgroundWorker worker, BackgroundLoadFolderArgs args)
        {
            Queue<ItemInfo> output = new Queue<ItemInfo>();
            this.startOperation = DateTime.Now;
            this.worker = worker;
            this.args = args;

            DirSearch(args.path, output);

            return new BackgroundLoadFolderRes(output);
        }

        private void DirSearch(string parentDir, Queue<ItemInfo> output)
        {

#if DEBUG
            Thread.Sleep(50);
#endif
            if ((DateTime.Now - startOperation).TotalMilliseconds > args.timeToProgress)
            {
                worker.ReportProgress(0);
                startOperation = DateTime.Now;
            }

            if (args.loadFiles)
            {
                foreach (string f in Directory.GetFiles(parentDir))
                {
                    if (args.maxItems <= output.Count) return;

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
                if (args.maxItems <= output.Count) return;

                DirSearch(d, output);
                
                if (args.maxItems <= output.Count) return;

                if (args.loadFolders)
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


    }
}
