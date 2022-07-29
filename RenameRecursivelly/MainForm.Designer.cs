using System.Windows.Forms;

namespace RenameRecursivelly
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblFolder = new System.Windows.Forms.Label();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.cbRenameFiles = new System.Windows.Forms.CheckBox();
            this.cbRenameFolders = new System.Windows.Forms.CheckBox();
            this.cmbMaxItems = new System.Windows.Forms.ComboBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.pbScan = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblFolderCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbOperations = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lblFolder.ForeColor = System.Drawing.Color.Red;
            this.lblFolder.Location = new System.Drawing.Point(130, 16);
            this.lblFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(78, 15);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "-- nevybráno";
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(10, 11);
            this.btnChooseFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(109, 25);
            this.btnChooseFolder.TabIndex = 1;
            this.btnChooseFolder.Text = "Vyber adresář:";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRename
            // 
            this.btnRename.Enabled = false;
            this.btnRename.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRename.ForeColor = System.Drawing.Color.Red;
            this.btnRename.Location = new System.Drawing.Point(10, 91);
            this.btnRename.Margin = new System.Windows.Forms.Padding(2);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(109, 25);
            this.btnRename.TabIndex = 2;
            this.btnRename.Text = "Přejmenovat";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(2, 0);
            this.tbLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(679, 424);
            this.tbLog.TabIndex = 3;
            // 
            // btnFinish
            // 
            this.btnFinish.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFinish.Location = new System.Drawing.Point(20, 497);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(2);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(109, 25);
            this.btnFinish.TabIndex = 4;
            this.btnFinish.Text = "Konec";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbRenameFiles
            // 
            this.cbRenameFiles.AutoSize = true;
            this.cbRenameFiles.Checked = true;
            this.cbRenameFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRenameFiles.Location = new System.Drawing.Point(10, 12);
            this.cbRenameFiles.Margin = new System.Windows.Forms.Padding(2);
            this.cbRenameFiles.Name = "cbRenameFiles";
            this.cbRenameFiles.Size = new System.Drawing.Size(69, 19);
            this.cbRenameFiles.TabIndex = 5;
            this.cbRenameFiles.Text = "soubory";
            this.cbRenameFiles.UseVisualStyleBackColor = true;
            // 
            // cbRenameFolders
            // 
            this.cbRenameFolders.AutoSize = true;
            this.cbRenameFolders.Checked = true;
            this.cbRenameFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRenameFolders.Location = new System.Drawing.Point(10, 35);
            this.cbRenameFolders.Margin = new System.Windows.Forms.Padding(2);
            this.cbRenameFolders.Name = "cbRenameFolders";
            this.cbRenameFolders.Size = new System.Drawing.Size(70, 19);
            this.cbRenameFolders.TabIndex = 6;
            this.cbRenameFolders.Text = "adresáře";
            this.cbRenameFolders.UseVisualStyleBackColor = true;
            // 
            // cmbMaxItems
            // 
            this.cmbMaxItems.DisplayMember = "0";
            this.cmbMaxItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaxItems.FormattingEnabled = true;
            this.cmbMaxItems.Items.AddRange(new object[] {
            "100",
            "200",
            "500",
            "1000"});
            this.cmbMaxItems.Location = new System.Drawing.Point(10, 59);
            this.cmbMaxItems.Name = "cmbMaxItems";
            this.cmbMaxItems.Size = new System.Drawing.Size(110, 23);
            this.cmbMaxItems.TabIndex = 7;
            this.cmbMaxItems.ValueMember = "100";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(10, 14);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(110, 23);
            this.btnScan.TabIndex = 8;
            this.btnScan.Text = "Skenovat";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // pbScan
            // 
            this.pbScan.Location = new System.Drawing.Point(10, 43);
            this.pbScan.Name = "pbScan";
            this.pbScan.Size = new System.Drawing.Size(110, 23);
            this.pbScan.TabIndex = 9;
            this.pbScan.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnChooseFolder);
            this.panel1.Controls.Add(this.lblFolder);
            this.panel1.Location = new System.Drawing.Point(8, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(829, 50);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnScan);
            this.panel2.Controls.Add(this.pbScan);
            this.panel2.Location = new System.Drawing.Point(8, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(132, 80);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pbLoad);
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Controls.Add(this.cbRenameFiles);
            this.panel3.Controls.Add(this.cbRenameFolders);
            this.panel3.Controls.Add(this.cmbMaxItems);
            this.panel3.Location = new System.Drawing.Point(8, 154);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(132, 154);
            this.panel3.TabIndex = 12;
            // 
            // pbLoad
            // 
            this.pbLoad.Location = new System.Drawing.Point(10, 117);
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(110, 23);
            this.pbLoad.TabIndex = 14;
            this.pbLoad.Visible = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoad.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnLoad.Location = new System.Drawing.Point(10, 88);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(110, 23);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "Načíst";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblFileCount);
            this.panel4.Controls.Add(this.lblFolderCount);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.btnRename);
            this.panel4.Location = new System.Drawing.Point(8, 314);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(132, 128);
            this.panel4.TabIndex = 13;
            // 
            // lblFileCount
            // 
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(15, 61);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(60, 15);
            this.lblFileCount.TabIndex = 5;
            this.lblFileCount.Text = "0 souborů";
            // 
            // lblFolderCount
            // 
            this.lblFolderCount.AutoSize = true;
            this.lblFolderCount.Location = new System.Drawing.Point(15, 37);
            this.lblFolderCount.Name = "lblFolderCount";
            this.lblFolderCount.Size = new System.Drawing.Size(61, 15);
            this.lblFolderCount.TabIndex = 4;
            this.lblFolderCount.Text = "0 adresářů";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "K přejmenování:";
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.tabPage1);
            this.tabLog.Controls.Add(this.tabPage2);
            this.tabLog.Location = new System.Drawing.Point(146, 68);
            this.tabLog.Name = "tabLog";
            this.tabLog.SelectedIndex = 0;
            this.tabLog.Size = new System.Drawing.Size(691, 454);
            this.tabLog.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(683, 426);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Přejmenování";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbOperations);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(683, 426);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Operace";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbOperations
            // 
            this.tbOperations.Location = new System.Drawing.Point(2, 1);
            this.tbOperations.Margin = new System.Windows.Forms.Padding(2);
            this.tbOperations.Multiline = true;
            this.tbOperations.Name = "tbOperations";
            this.tbOperations.ReadOnly = true;
            this.tbOperations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOperations.Size = new System.Drawing.Size(679, 424);
            this.tbOperations.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 533);
            this.Controls.Add(this.tabLog);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnFinish);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Přejmenovávač";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog1;
        private Label lblFolder;
        private Button btnChooseFolder;
        private Button btnRename;
        private TextBox tbLog;
        private Button btnFinish;
        private CheckBox cbRenameFiles;
        private CheckBox cbRenameFolders;
        private ComboBox cmbMaxItems;
        private Button btnScan;
        private Label label1;
        private ProgressBar pbScan;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private ProgressBar pbLoad;
        private Button btnLoad;
        private Panel panel4;
        private Label lblFileCount;
        private Label lblFolderCount;
        private Label label2;
        private TabControl tabLog;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox tbOperations;
    }
}