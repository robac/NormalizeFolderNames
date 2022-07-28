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
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lblFolder.ForeColor = System.Drawing.Color.Red;
            this.lblFolder.Location = new System.Drawing.Point(8, 5);
            this.lblFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(78, 15);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "-- nevybráno";
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(8, 30);
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
            this.btnRename.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRename.Location = new System.Drawing.Point(8, 130);
            this.btnRename.Margin = new System.Windows.Forms.Padding(2);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(109, 25);
            this.btnRename.TabIndex = 2;
            this.btnRename.Text = "Přejmenovat";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(147, 30);
            this.tbLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(695, 291);
            this.tbLog.TabIndex = 3;
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(8, 296);
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
            this.cbRenameFiles.Location = new System.Drawing.Point(8, 67);
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
            this.cbRenameFolders.Location = new System.Drawing.Point(8, 94);
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
            this.cmbMaxItems.Location = new System.Drawing.Point(8, 175);
            this.cmbMaxItems.Name = "cmbMaxItems";
            this.cmbMaxItems.Size = new System.Drawing.Size(121, 23);
            this.cmbMaxItems.TabIndex = 7;
            this.cmbMaxItems.ValueMember = "100";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 333);
            this.Controls.Add(this.cmbMaxItems);
            this.Controls.Add(this.cbRenameFolders);
            this.Controls.Add(this.cbRenameFiles);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.lblFolder);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Přejmenovávač";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}