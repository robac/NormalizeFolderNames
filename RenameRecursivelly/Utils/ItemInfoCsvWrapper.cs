using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameRecursivelly.Utils
{
    internal class ItemInfoCsvWrapper
    {
        private ItemInfo item;

        public void setItem(ItemInfo item)
        {
            this.item = item;
        }

        public string Path { get => this.item.path; }
        public string Name { get => this.item.name; }
        public string NormalizedName { get => this.item.normalizedName; }
        public bool IsDir { get => this.item.isDir; }

        public string Time
        {
            get
            {
                return DateTime.Now.ToString();
            }
        }
    }
}
