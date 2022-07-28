using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Threading;

namespace RenameRecursivelly
{
    public partial class MainForm : Form
    {
        private BackgroundWorker bwScan = new BackgroundWorker();

        static string ReadSetting(string key, string defaultValue)
        {
            string result = "";
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? defaultValue;
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return result;
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void bwScan_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(i);
            }
            e.Result = "Hovno";
        }

        private void bwScan_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            label1.Text = e.ProgressPercentage.ToString();
        }

        private void bwScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = (string)e.Result;
        }

        public MainForm()
        {
            InitializeComponent();
            bwScan.ProgressChanged += bwScan_ProgressChanged;
            bwScan.DoWork += bwScan_DoWork;
            bwScan.RunWorkerCompleted += bwScan_RunWorkerCompleted;
            bwScan.WorkerReportsProgress = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dir = folderBrowserDialog1.SelectedPath;
            if (!Directory.Exists(dir))
            {
                logMessage("Adresář neexistuje!", true);
                return;
            }

            if (!(cbRenameFiles.Checked || cbRenameFolders.Checked))
            {
                logMessage("Nejsou vybrané ani soubory ani adresáře!", true);
                return;
            }

            Queue<Utils.ItemInfo> list = new Queue<Utils.ItemInfo>();
            Utils.Utils.DirSearch(dir, list, cbRenameFiles.Checked, cbRenameFolders.Checked, Int32.Parse(cmbMaxItems.Text));
            if (list.Count == 0)
            {
                logMessage("Nic k přejmenování!", true);
                return;
            }

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            int itemsTotal = list.Count;

            using (var writer = new StreamWriter(File.Open(Utils.Utils.getLogFilename(), FileMode.Append)))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                RenameForm frmDialogRename = new RenameForm();
                int currentItem = 0;
                while (list.Count > 0)
                {
                    currentItem++;
                    Utils.ItemInfo item = list.Dequeue();

                    DialogResult result = frmDialogRename.OpenDialog(item, itemsTotal, currentItem);
                    if (result == DialogResult.Abort)
                    {
                        return;
                    }
                    if (result == DialogResult.Yes)
                    {
                        logMessage(String.Format("({0})", item.path), true);
                        logMessage(String.Format("{2} \"{0}\" --> \"{1}\"", item.name, item.normalizedName, (item.isDir ? "Adresář" : "Soubor")));


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
                            logMessage(String.Format("Došlo k chybě: {0}{1}{2}", exc.Message, Environment.NewLine, exc.StackTrace));
                        }
                    }
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string text = folderBrowserDialog1.SelectedPath;
            if (text.Length == 0)
            {
                lblFolder.Text = "-- nevybráno";
                lblFolder.ForeColor = Color.Red;
                return;
            }

            lblFolder.Text = text;
            lblFolder.ForeColor = Color.Black;
            AddUpdateAppSettings("WorkFolder", text);
            this.ActiveControl = this.btnRename;
        }

         private void button3_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbMaxItems.SelectedIndex = 0;
            string folderPath = ReadSetting("WorkFolder", "");
            if (folderPath != "")
            {
                lblFolder.Text = folderPath;
                lblFolder.ForeColor = Color.Black;
                folderBrowserDialog1.SelectedPath = folderPath;
                this.ActiveControl = btnRename;
            }
        }

        private void logMessage(string message, bool separator = false)
        {
            if (separator) logSeparator();
            this.tbLog.AppendText(Environment.NewLine + message);
            this.tbLog.SelectionStart = this.tbLog.Text.Length;
            this.tbLog.ScrollToCaret();
        }

        private void logSeparator()
        {
            this.tbLog.AppendText(Environment.NewLine + "---");
            this.tbLog.SelectionStart = this.tbLog.Text.Length;
            this.tbLog.ScrollToCaret();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bwScan.RunWorkerAsync();
        }
    }
}