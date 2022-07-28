using RenameRecursivelly.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace RenameRecursivelly
{
    public partial class RenameForm : Form
    {
        private ItemInfo item;
        private bool AltPressed;

        public RenameForm()
        {
            InitializeComponent();
        }

        public DialogResult OpenDialog(ItemInfo item, int itemsTotal, int currentItem)
        {
            this.item = item;

            this.lblFolder.Text = Path.GetDirectoryName(item.path);
            this.tbOriginalName.Text = Path.GetFileName(item.name);

            this.Text = item.isDir ? "Přejmenovat adresář" : "Přejmenovat soubor";
            
            this.tbNewName.Select(0, 0);
            if (item.isDir)
            {
                lblExtension.Text = "";
                this.tbNewName.Text = Path.GetFileName(item.normalizedName);
            } else
            {
                lblExtension.Text = Path.GetExtension(item.name);
                this.tbNewName.Text = Path.GetFileNameWithoutExtension(item.normalizedName);
            }

            this.lblCounter.Text = String.Format("{0}/{1}", currentItem, itemsTotal);

            this.ActiveControl = this.btnRename;
            this.CenterToScreen();
            DialogResult res = this.ShowDialog();
            this.item = null;
            return res;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            string newName = this.tbNewName.Text.Trim();

            if (newName.Length == 0)
            {
                MessageBox.Show("Nelze!");
                return;
            }

            string normalizedName = newName.NormalizeString();
            if (newName != normalizedName)
            {
                MessageBox.Show("Nový název nesplňuje podmínky!");
                return;
            }
            this.DialogResult = DialogResult.Yes;
            
            this.item.normalizedName = tbNewName.Text.Trim()+lblExtension.Text;
            this.Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void RenameForm_Load(object sender, EventArgs e)
        {

        }

        private void tbNewName_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar == (char)Keys.Return)
            { 
                this.btnRename_Click(sender, e);
            }*/
        }

        private void RenameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Return) && (this.ActiveControl != btnSkip) && (this.ActiveControl != btnFinish))
            {
                e.Handled = true;
                this.btnRename_Click(sender, e);
                return;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.btnSkip_Click(sender, e);
                return;
            }

            e.Handled = false;
        }

        private void RenameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("close");
        }
    }
}
