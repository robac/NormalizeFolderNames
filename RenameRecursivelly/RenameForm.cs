using RenameRecursivelly.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RenameRecursivelly
{
    public partial class RenameForm : Form
    {
        private ItemInfo? item;
        private string fileName;
        private string fileExt;

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

            fileName = (item.isDir) ? item.normalizedName : Path.GetFileNameWithoutExtension(item.normalizedName);
            fileExt = (item.isDir) ? "" : Path.GetExtension(item.normalizedName);
            
            this.tbNewName.Select(0, 0);
            lblExtension.Text = fileExt;
            tbNewName.Text = fileName;
            

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
            string path = Path.Combine(item.path, newName);

            if (newName.Length == 0)
            {
                showMessage("Nelze použít prázdný název!");
                return;
            } 

            if ((this.item.isDir && Directory.Exists(path)) ||
                ((!this.item.isDir) && File.Exists(path + lblExtension.Text.Trim()))) 
            {
                string type = (this.item.isDir) ? "Adresář" : "Soubor";
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

        private void OpenFolder(string folderPath)
        {
            Process.Start("explorer.exe", folderPath);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(item.path))
            {
                showMessage(String.Format("Složka {0} neexistuje!", item.path));
                return;
            }

            OpenFolder(item.path);
        }
    }
}
