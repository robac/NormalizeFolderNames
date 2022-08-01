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
using Serilog;


namespace RenameRecursivelly
{
    enum LogType
    {
        Rename,
        Operation,
    }

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
            logMessage("-- Scan průběžné info", LogType.Operation);
            logMessage($" Složek: {progress.dirCount}", LogType.Operation);
            logMessage($" Souborů: {progress.fileCount}", LogType.Operation);
            logMessage($" Složek špatně: {progress.wrongDirCount}", LogType.Operation);
            logMessage($" Souborů špatně: {progress.wrongFileCount}", LogType.Operation);
            pbScan.Value = (pbScan.Value == 100) ? 10 : (pbScan.Value + 10);
        }

        private void bwScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundScanFolderRes progress = e.Result as BackgroundScanFolderRes;
            logMessage("-- Scan VÝSLEDEK ---", LogType.Operation, true);
            logMessage($" Složek: {progress.dirCount}", LogType.Operation);
            logMessage($" Souborů: {progress.fileCount}", LogType.Operation);
            logMessage($" Složek špatně: {progress.wrongDirCount}", LogType.Operation);
            logMessage($" Souborů špatně: {progress.wrongFileCount}", LogType.Operation);
            logMessage("", LogType.Operation);
            logMessage("Scan skončil.", LogType.Operation);
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

            lblFolderCount.Text = $"{totalFolders.ToString()} adresářů";
            lblFileCount.Text = $"{totalFiles.ToString()} souborů";
        }


        private void bwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.itemsToRename = ((BackgroundLoadFolderRes)e.Result).itemsToRename;
            logMessage($"Načteno {itemsToRename.Count} položek...", LogType.Operation);
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

            timer1.Enabled = true;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            int itemsTotal = itemsToRename.Count;

            using (var writer = new StreamWriter(File.Open(Utils.Utils.getCsvResultFilename(), FileMode.Append)))
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
                        logMessage("Přejmenování ukončeno uživatelem.", LogType.Rename, true);
                        return;
                    }

                    if (result == DialogResult.Cancel)
                    {
                        logMessage($"Položka {Path.Combine(item.path, item.name)} přeskočena.", LogType.Rename, true);
                        continue;
                    }

                    if (result == DialogResult.Yes)
                    {
                        logMessage($"({item.path})", LogType.Rename, true);
                        if (item.isDir)
                            logMessage($"Adresář \"{item.name}\" --> \"{item.normalizedName}\"", LogType.Rename);
                        else
                            logMessage($"Soubor \"{item.name}\" --> \"{item.normalizedName}\"", LogType.Rename);

                        try
                        {
                            if (item.isDir)
                                System.IO.Directory.Move(Path.Combine(item.path, item.name), Path.Combine(item.path, item.normalizedName));
                            else
                                System.IO.File.Move(Path.Combine(item.path, item.name), Path.Combine(item.path, item.normalizedName));

                            itemInfoCsvWrapper.setItem(item);
                            csv.WriteRecord(itemInfoCsvWrapper);
                            csv.NextRecord();
                        } catch (Exception exc)
                        {
                            logMessage($"Došlo k chybě: {exc.Message}{Environment.NewLine}{exc.StackTrace}", LogType.Rename);
                        }
                    }
                }
            }
            refreshRenameStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldPath = folderBrowserDialog1.SelectedPath;
            folderBrowserDialog1.ShowDialog();

            if (oldPath == folderBrowserDialog1.SelectedPath)
            {
                return;
            }

            itemsToRename = new Queue<ItemInfo>();
            refreshRenameStatus();

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

        private void logMessage(string message, LogType type, bool separator = false)
        {
            TextBox destination = (type == LogType.Rename) ? tbLog : tbOperations;
            tabLog.SelectedTab = (type == LogType.Rename) ? tabPage1 : tabPage2;

            if (separator) logSeparator(destination);
            destination.AppendText(Environment.NewLine + message);
            destination.SelectionStart = destination.Text.Length;
            destination.ScrollToCaret();

            Log.Information($"({type}): {message}", type, message);
        }

        private void logSeparator(TextBox destination)
        {
            destination.AppendText(Environment.NewLine + "---");
            destination.SelectionStart = this.tbLog.Text.Length;
            destination.ScrollToCaret();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                logMessage($"Složka {folderBrowserDialog1.SelectedPath} neexistuje!", LogType.Operation);
                return;
            }
            
            if (bwScan.IsBusy)
            {
                logMessage($"Operace probíhá...", LogType.Operation);
                return;
            }
            logMessage("Scan začíná...", LogType.Operation, true);
            pbScan.Visible = true;
            pbScan.Value = 0;
            btnScan.Enabled = false;
            bwScan.RunWorkerAsync();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                logMessage($"Složka {folderBrowserDialog1.SelectedPath} neexistuje!", LogType.Operation);
                return;
            }

            if (bwLoad.IsBusy)
            {
                logMessage($"Operace probíhá...", LogType.Operation);
                return;
            }

            if (!(cbRenameFiles.Checked || cbRenameFolders.Checked))
            {
                logMessage("Nejsou vybrané ani soubory ani adresáře!", LogType.Operation, true);
                return;
            }

            logMessage("Načítání začíná...", LogType.Operation, true);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnRename.Enabled = !((itemsToRename is null) || (itemsToRename.Count == 0));

            if (bwScan.IsBusy)
            {
                btnChooseFolder.Enabled = false;
                btnLoad.Enabled = false;
                btnScan.Enabled = false;
                return;
            }

            if (bwLoad.IsBusy)
            {
                btnChooseFolder.Enabled = false;
                btnLoad.Enabled = false;
                btnScan.Enabled = false;
                return;
            }

            btnChooseFolder.Enabled = true;

            bool dirExistis = Directory.Exists(folderBrowserDialog1.SelectedPath);
            btnLoad.Enabled = dirExistis;
            btnScan.Enabled = dirExistis;
        }
    }
}