using System.Text;

namespace RenameRecursivelly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dir = folderBrowserDialog1.SelectedPath;
            if (!Directory.Exists(dir))
            {
                MessageBox.Show("Adresář neexistuje!");
                return;
            }

            if (!(checkBox1.Checked || checkBox2.Checked))
            {
                MessageBox.Show("Nejsou vybrané ani soubory ani adresáře!");
                return;
            }

            DialogResult dialogResult = MessageBox.Show(String.Format("Přejmenovat ve složce {0}?", dir), "Potvrďte", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Queue<Utils.ItemInfo> list = new Queue<Utils.ItemInfo>();
                Utils.Utils.DirSearch(dir, list, checkBox1.Checked, checkBox2.Checked);

                if (list.Count == 0)
                {
                    MessageBox.Show("Nic k přejmenování!");
                    return;
                }


                Form2 frmDialogRename = new Form2();
                while (list.Count > 0)
                {
                    Utils.ItemInfo item = list.Dequeue();
                    
                    DialogResult result = frmDialogRename.OpenDialog(item);
                    if (result == DialogResult.Abort)
                    {
                        return;
                    }
                    if (result == DialogResult.Yes)
                    {
                        item.normalizedName = frmDialogRename.newName;
                        textBox1.Text += Environment.NewLine + String.Format("({0}) {1} prejmenovano na {2}", item.path, item.name, item.normalizedName);
                        if (item.isDir)
                        {
                            System.IO.Directory.Move(Path.Combine(item.path, item.name), Path.Combine(item.path, item.normalizedName));
                        }
                        else
                        {
                            System.IO.File.Move(Path.Combine(item.path, item.name), Path.Combine(item.path, item.normalizedName));
                        }
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string text = folderBrowserDialog1.SelectedPath;
            label1.Text = (text.Length > 0) ? text : "-- nevybráno";
            this.ActiveControl = this.button2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}