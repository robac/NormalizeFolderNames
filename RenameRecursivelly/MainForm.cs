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
using RenameRecursivelly.Utils;

namespace RenameRecursivelly
{
    public partial class MainForm : Form
    {
        private BackgroundWorker bwScan = new BackgroundWorker();
        private BackgroundWorker bwLoad = new BackgroundWorker();
        private Queue<ItemInfo>? itemsToRename;

        private void bwScan_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;
            BackgroundScanFolder bwScanFolder = new BackgroundScanFolder();

            e.Result = bwScanFolder.DoWork(worker, new BackgroundScanFolderArgs(folderBrowserDialog1.SelectedPath, 1000));
        }

        private void bwScan_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            BackgroundScanFolderRes progress = e.UserState as BackgroundScanFolderRes;
            logMessage("-- Scan průběžné info");
            logMessage(String.Format(" Složek: {0}", progress.dirCount));
            logMessage(String.Format(" Souborů: {0}", progress.fileCount));
            logMessage(String.Format(" Složek špatně: {0}", progress.wrongDirCount));
            logMessage(String.Format(" Souborů špatně: {0}", progress.wrongFileCount));
            pbScan.Value = (pbScan.Value == 100) ? 10 : (pbScan.Value + 10);
        }

        private void bwScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundScanFolderRes progress = e.Result as BackgroundScanFolderRes;
            logMessage("-- Scan VÝSLEDEK ---", true);
            logMessage(String.Format(" Složek: {0}", progress.dirCount));
            logMessage(String.Format(" Souborů: {0}", progress.fileCount));
            logMessage(String.Format(" Složek špatně: {0}", progress.wrongDirCount));
            logMessage(String.Format(" Souborů špatně: {0}", progress.wrongFileCount));
            logMessage("");
            logMessage("Scan skončil.");
            pbScan.Visible = false;
            btnScan.Enabled = true;
        }

        private void bwLoad_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;
            BackgroundLoadFolder bwLoadFolder = new BackgroundLoadFolder();

            e.Result = bwLoadFolder.DoWork(worker, (BackgroundLoadFolderArgs)e.Argument);
        }

        private void bwLoad_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            pbLoad.Value = (pbLoad.Value == 100) ? 10 : (pbLoad.Value + 10);
        }

        private void refreshRenameStatus()
        {
            btnRename.Enabled = itemsToRename.Count > 0;

            int totalFolders = 0;
            int totalFiles = 0;
            foreach (ItemInfo item in itemsToRename)
            {
                if (item.isDir)
                    totalFolders++;
                else
                    totalFiles++;
            }

            lblFolderCount.Text = String.Format("{0} adresářů", totalFolders.ToString());
            lblFileCount.Text = String.Format("{0} souborů", totalFiles.ToString());
        }


        private void bwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.itemsToRename = ((BackgroundLoadFolderRes)e.Result).itemsToRename;
            logMessage(String.Format("Načteno {0} položek...", this.itemsToRename.Count));
            pbLoad.Visible = false;
            btnLoad.Enabled = true;
            refreshRenameStatus();
        }

        public MainForm()
        {
            InitializeComponent();

            bwScan.ProgressChanged += bwScan_ProgressChanged;
            bwScan.DoWork += bwScan_DoWork;
            bwScan.RunWorkerCompleted += bwScan_RunWorkerCompleted;
            bwScan.WorkerReportsProgress = true;

            bwLoad.ProgressChanged += bwLoad_ProgressChanged;
            bwLoad.DoWork += bwLoad_DoWork;
            bwLoad.RunWorkerCompleted += bwLoad_RunWorkerCompleted;
            bwLoad.WorkerReportsProgress = true;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            int itemsTotal = itemsToRename.Count;

            using (var writer = new StreamWriter(File.Open(Utils.Utils.getLogFilename(), FileMode.Append)))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                ItemInfoCsvWrapper itemInfoCsvWrapper = new ItemInfoCsvWrapper();
                RenameForm frmDialogRename = new RenameForm();
                int currentItem = 0;
                while (itemsToRename.Count > 0)
                {
                    currentItem++;
                    Utils.ItemInfo item = itemsToRename.Dequeue();

                    DialogResult result = frmDialogRename.OpenDialog(item, itemsTotal, currentItem);
                    if (result == DialogResult.Abort)
                    {
                        refreshRenameStatus();
                        return;
                    }
                    if (result == DialogResult.Yes)
                    {
                        logMessage(String.Format("({0})", item.path), true);
                        logMessage(String.Format("{2} \"{0}\" --> \"{1}\"", item.name, item.normalizedName, (item.isDir ? "Adresář" : "Soubor")));


                        itemInfoCsvWrapper.setItem(item);
                        csv.WriteRecord(itemInfoCsvWrapper);
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
            refreshRenameStatus();
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
            AppSettings.AddUpdateValue("WorkFolder", text);
            this.ActiveControl = this.btnRename;
        }

         private void button3_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbMaxItems.SelectedIndex = 0;
            string folderPath = AppSettings.ReadValue("WorkFolder", "");
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
            if (bwScan.IsBusy)
            {
                logMessage("Operace probíhá...");
            }
            logMessage("Scan začíná...", true);
            pbScan.Visible = true;
            pbScan.Value = 0;
            btnScan.Enabled = false;
            bwScan.RunWorkerAsync();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (bwLoad.IsBusy)
            {
                logMessage("Operace probíhá...");
            }

            if (!(cbRenameFiles.Checked || cbRenameFolders.Checked))
            {
                logMessage("Nejsou vybrané ani soubory ani adresáře!", true);
                return;
            }

            logMessage("Načítání začíná...", true);
            pbLoad.Visible = true;
            pbLoad.Value = 0;
            btnLoad.Enabled = false;
            btnRename.Enabled = false;

            BackgroundLoadFolderArgs args = new BackgroundLoadFolderArgs(
                folderBrowserDialog1.SelectedPath,
                1000,
                cbRenameFiles.Checked,
                cbRenameFolders.Checked,
                Int32.Parse(cmbMaxItems.Text)
                );

            bwLoad.RunWorkerAsync(args);
        }
    }
}