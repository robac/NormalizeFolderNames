using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace RenameRecursivelly
{
    public partial class MainForm : Form
    {
        public MainForm()
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

            if (!(cbRenameFiles.Checked || cbRenameFolders.Checked))
            {
                MessageBox.Show("Nejsou vybrané ani soubory ani adresáře!");
                return;
            }

            Queue<Utils.ItemInfo> list = new Queue<Utils.ItemInfo>();
                Utils.Utils.DirSearch(dir, list, cbRenameFiles.Checked, cbRenameFolders.Checked, 100);

                if (list.Count == 0)
                {
                    MessageBox.Show("Nic k přejmenování!");
                    return;
                }

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var writer = new StreamWriter(File.Open(Utils.Utils.getLogFilename(), FileMode.Append)))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.WriteHeader<Utils.ItemInfo>();
                csv.NextRecord();
                RenameForm frmDialogRename = new RenameForm();
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
                        tbLog.Text += ((tbLog.Text.Length > 0) ? Environment.NewLine : "") + String.Format("{3} \"{1}\" --> \"{2}\" ({0})", item.path, item.name, item.normalizedName, (item.isDir ? "Adresář" : "Soubor"));

                        csv.WriteRecord(item);
                        csv.NextRecord();
                        try
                        {
                            if (item.isDir)
                            {
                                System.IO.Directory.Move(Path.Combine(item.path, item.name), Path.Combine(item.path, item.normalizedName));
                            }
                            else
                            {
                                System.IO.File.Move(Path.Combine(item.path, item.name), Path.Combine(item.path, item.normalizedName));
                            }
                        } catch (Exception exc)
                        {
                            MessageBox.Show(String.Format("Došlo k chybě: {0}{1}{2}", exc.Message, Environment.NewLine, exc.StackTrace));
                        }
                    }
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string text = folderBrowserDialog1.SelectedPath;
            lblFolder.Text = (text.Length > 0) ? text : "-- nevybráno";
            lblFolder.ForeColor = (text.Length > 0) ? Color.Black : Color.Red;
            this.ActiveControl = this.btnRename;
        }

         private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbMaxItems.SelectedIndex = 0;
        }
    }
}