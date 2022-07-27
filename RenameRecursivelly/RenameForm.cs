using RenameRecursivelly.Utils;

namespace RenameRecursivelly
{
    public partial class RenameForm : Form
    {
        public string newName = "";

        public RenameForm()
        {
            InitializeComponent();
        }

        public DialogResult OpenDialog(ItemInfo item)
        {
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

            this.ActiveControl = this.btnRename;
            this.CenterToScreen();
            return this.ShowDialog();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            string newName = this.tbNewName.Text.Trim();

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
            
            this.newName = tbNewName.Text.Trim()+lblExtension.Text;
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
    }
}
