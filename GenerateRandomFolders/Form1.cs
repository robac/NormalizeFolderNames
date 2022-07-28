using System.Text;

namespace GenerateBordel
{
    public class DirInfo
    {
        public int depth;
        public string path;

        public DirInfo(int depth, string path)
        {
            this.depth = depth;
            this.path = path;
        }
    }

    public partial class Form1 : Form
    {
        string[] dirNames = new string[] { "Jedna", "Pecka", "Tøi", "Žižka", "Major", "Àíkanka", "Pìt" };
        Queue<DirInfo> queue = new Queue<DirInfo>();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            label1.Text = folderBrowserDialog1.SelectedPath.Trim().Length > 0 ? folderBrowserDialog1.SelectedPath : "-- nevybráno";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "-- nevybráno";
        }

        private void CreateSubDirs(string dir, int maxDepth)
        {
            queue.Clear();
            foreach (string subdir in dirNames)
            {
                string newName = Path.Combine(dir, subdir);
                Directory.CreateDirectory(newName);
                queue.Enqueue(new DirInfo(1, newName));
            }

            Random rnd = new Random();

            while (queue.Count > 0)
            {
                DirInfo item = queue.Dequeue();
                if (item.depth > maxDepth) continue;

                int numSubdirs = rnd.Next(6);
                for (int i = 1; i <= numSubdirs; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    int numWords = rnd.Next(2) + 2;
                    for (int j = 0; j <= numWords; j++)
                    {
                        sb.Append(dirNames[rnd.Next(dirNames.Length)]);
                        sb.Append((rnd.Next(3) == 0) ? " " : "");
                    }
                    string newName = Path.Combine(item.path, sb.ToString());
                    queue.Enqueue(new DirInfo(item.depth+1, newName));
                    Directory.CreateDirectory(newName);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dir = folderBrowserDialog1.SelectedPath.Trim();
            if ((dir.Length == 0) || (!Directory.Exists(dir)))
            {
                MessageBox.Show("Neplatné údaje!");
                return;
            }

            CreateSubDirs(dir, 7);
        }
    }
}