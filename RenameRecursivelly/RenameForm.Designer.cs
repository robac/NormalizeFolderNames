namespace RenameRecursivelly
{
    partial class RenameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFolder = new System.Windows.Forms.Label();
            this.tbNewName = new System.Windows.Forms.TextBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.lblExtension = new System.Windows.Forms.Label();
            this.tbOriginalName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblFolder.Location = new System.Drawing.Point(24, 14);
            this.lblFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 15);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "label1";
            // 
            // tbNewName
            // 
            this.tbNewName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbNewName.ForeColor = System.Drawing.Color.Red;
            this.tbNewName.Location = new System.Drawing.Point(24, 91);
            this.tbNewName.Margin = new System.Windows.Forms.Padding(2);
            this.tbNewName.Name = "tbNewName";
            this.tbNewName.Size = new System.Drawing.Size(799, 34);
            this.tbNewName.TabIndex = 2;
            // 
            // btnRename
            // 
            this.btnRename.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRename.ForeColor = System.Drawing.Color.Red;
            this.btnRename.Location = new System.Drawing.Point(24, 142);
            this.btnRename.Margin = new System.Windows.Forms.Padding(2);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(90, 25);
            this.btnRename.TabIndex = 3;
            this.btnRename.Text = "Přejmenovat";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(133, 142);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(2);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(78, 25);
            this.btnSkip.TabIndex = 4;
            this.btnSkip.Text = "Přeskočit";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFinish.Location = new System.Drawing.Point(745, 142);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(2);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(78, 25);
            this.btnFinish.TabIndex = 5;
            this.btnFinish.Text = "Konec";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblExtension.ForeColor = System.Drawing.Color.Red;
            this.lblExtension.Location = new System.Drawing.Point(839, 97);
            this.lblExtension.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(65, 28);
            this.lblExtension.TabIndex = 7;
            this.lblExtension.Text = "label4";
            // 
            // tbOriginalName
            // 
            this.tbOriginalName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbOriginalName.Location = new System.Drawing.Point(24, 47);
            this.tbOriginalName.Name = "tbOriginalName";
            this.tbOriginalName.ReadOnly = true;
            this.tbOriginalName.Size = new System.Drawing.Size(799, 34);
            this.tbOriginalName.TabIndex = 8;
            // 
            // RenameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 176);
            this.Controls.Add(this.tbOriginalName);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.tbNewName);
            this.Controls.Add(this.lblFolder);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RenameForm";
            this.Text = "Přejmenovat soubor";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblFolder;
        private TextBox tbNewName;
        private Button btnRename;
        private Button btnSkip;
        private Button btnFinish;
        private Label lblExtension;
        private TextBox tbOriginalName;
    }
}