using System;

namespace RenameRecursivelly.Utils
{
    public class ItemInfo
    {
        public string path;
        public string name;
        public string normalizedName;
        public bool isDir = false;

        public ItemInfo(string path, string name, string normalizedName, bool isDir)
        {
            this.name = name;
            this.normalizedName = normalizedName;
            this.isDir = isDir;
            this.path = path;
        }
    }
}
