using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenameRecursivelly
{
    public partial class RenameForm : Form
    {
        public string newName = "";
        public RenameForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public DialogResult OpenDialog(Utils.ItemInfo item)
        {
            this.label1.Text = Path.GetDirectoryName(item.path);
            this.label2.Text = Path.GetFileName(item.name);
            
            this.textBox1.Select(0, 0);
            if (item.isDir)
            {
                label4.Text = "";
                this.textBox1.Text = Path.GetFileName(item.normalizedName);
            } else
            {
                label4.Text = Path.GetExtension(item.name);
                this.textBox1.Text = Path.GetFileNameWithoutExtension(item.normalizedName);
            }

            this.ActiveControl = this.button1;
            this.CenterToScreen();
            return this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newName = this.textBox1.Text.Trim();

            if (newName.Length == 0)
            {
                MessageBox.Show("Nelze!");
                return;
            }

            string normalizedName = Utils.Utils.RemoveDiacritics(newName);
            if (newName != normalizedName)
            {
                MessageBox.Show("Nový název nesplňuje podmínky!");
                return;
            }
            this.DialogResult = DialogResult.Yes;
            
            this.newName = textBox1.Text.Trim()+label4.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
    }
}
