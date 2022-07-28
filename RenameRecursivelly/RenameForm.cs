using RenameRecursivelly.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace RenameRecursivelly
{
    public partial class RenameForm : Form
    {
        private ItemInfo item;

        public RenameForm()
        {
            InitializeComponent();
        }

        public DialogResult OpenDialog(ItemInfo item, int itemsTotal, int currentItem)
        {
            this.item = item;

            this.lblFolder.Text = item.path;
            this.tbOriginalName.Text = item.name;

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
            hideMessage();
            DialogResult res = this.ShowDialog();
            this.item = null;
            return res;
        }

        private void showMessage(string message) 
        {
            this.tbMessage.Text = message;
            this.tbMessage.Visible = true;
        }

        private void hideMessage()
        {
            this.tbMessage.Text = "";
            this.tbMessage.Visible = false;
        }


        private void btnRename_Click(object sender, EventArgs e)
        {
            string newName = this.tbNewName.Text.Trim();
            string path = Path.Combine(this.item.path, newName);

            if (newName.Length == 0)
            {
                showMessage("Nelze použít prázdný název!");
                return;
            }

            if ((this.item.IsDir && Directory.Exists(path)) ||
                (!this.item.IsDir && File.Exists(path))) 
            {
                string type = (this.item.IsDir) ? "Adresář" : "Soubor";
                showMessage(String.Format("{0} s názvem {1} již existuje!", type, path));
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
    }
}
