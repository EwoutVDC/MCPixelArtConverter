using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCPixelArtConverter
{
    public partial class MCPACMainForm : Form
    {
        public MCPACMainForm()
        {
            InitializeComponent();
        }

        private void LoadBlockInfoButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (!String.IsNullOrEmpty(Program.BaseFolderName))
                folderBrowser.SelectedPath = Program.BaseFolderName;
            folderBrowser.ShowDialog();

            Program.BaseFolderName = folderBrowser.SelectedPath + "\\";

            Program.resourcePack = new MCResourcePack(Program.BaseFolderName);

            comboBoxAvailableBlocks.Items.AddRange(Program.resourcePack.getBlockNames().ToArray());
        }

        private void btnShowTexture_Click(object sender, EventArgs e)
        {
            Form textureForm = new Form();
            textureForm.Text = "Texture viewer";
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Program.resourcePack.getState(comboBoxAvailableBlocks.SelectedItem.ToString()).GetTopView();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            textureForm.Controls.Add(pictureBox);
            textureForm.ShowDialog();
        }
    }
}
