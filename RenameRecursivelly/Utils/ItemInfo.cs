namespace RenameRecursivelly.Utils
{
    public class ItemInfo
    {
        public string path;
        public string name;
        public string normalizedName;
        public bool isDir = false;

        //CSV columns
        public string Path { get => path; }
        public string Name { get => name; }
        public string NormalizedName { get => normalizedName; }
        public bool IsDir { get => isDir; }

        public string Time 
        { 
                get 
                {
                    return DateTime.Now.ToString();
                }      
        }


        public ItemInfo(string path, string name, string normalizedName, bool isDir)
        {
            this.name = name;
            this.normalizedName = normalizedName;
            this.isDir = isDir;
            this.path = path;
        }
    }
}
